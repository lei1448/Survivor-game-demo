BasePanel:subClass("BagPanel")

-- 使用一个列表作为对象池来管理所有格子实例
BagPanel.gridPool = {} 
-- 记录当前正在显示的格子数量
BagPanel.activeGridCount = 0 

function BagPanel:Init()
    self.base.Init(self,"BagPanel",
        function()
            self.controls["togEquip"]["Toggle"].onValueChanged:AddListener
            (
                function(value)
                    if value then self:ChangeType(1) end
                end
            )
            self.controls["togItem"]["Toggle"].onValueChanged:AddListener
            (
                function(value)
                    if value then self:ChangeType(2) end
                end
            )
            self.controls["togGem"]["Toggle"].onValueChanged:AddListener
            (
                function(value)
                    if value then self:ChangeType(3) end
                end
            )
            self.controls["btnClose"]["Button"].onClick:AddListener
            (
                function()
                    self:OnCloseBtnClick()
                end
            )
            -- 初始化时默认不显示任何类型，或根据需求显示第一个
            self.nowType = -1
        end
    )
end

function BagPanel:ShowBagPanel()
    self.base.ShowPanel(self)
    if self.panelObj then 
        -- 每次打开背包时，都强制选中并刷新装备页
        self.controls["togEquip"]["Toggle"].isOn = true
        -- 设置 isOn 会触发 onValueChanged 事件，自动调用 ChangeType(1)
        self:ChangeType(1)
    end
end

function BagPanel:HidePanel()
    self.base.HidePanel(self)
end

function BagPanel:OnCloseBtnClick()
    self:HidePanel()
end

-- 核心修改：使用对象池逻辑来更新格子
function BagPanel:ChangeType(type)
    -- 如果页签没有变化，则无需刷新
    if self.nowType == type and self.panelObj and self.panelObj.activeSelf then
        return
    end
    self.nowType = type

    -- 1. 获取目标物品数据
    local nowItems = nil
    if type == 1 then
        nowItems = PlayerData.equips
    elseif type == 2 then
        nowItems = PlayerData.items
    else
        nowItems = PlayerData.gems
    end

    -- 2. 隐藏当前所有活动的格子，准备复用
    for i = 1, self.activeGridCount do
        if self.gridPool[i] and self.gridPool[i].obj then
            self.gridPool[i].obj:SetActive(false)
        end
    end

    -- 3. 遍历新数据，更新或创建格子
    for i = 1, #nowItems do
        local currentItemData = nowItems[i]
        local grid = nil

        if i <= #self.gridPool then
            -- A. 对象池中有足够的格子，直接复用
            grid = self.gridPool[i]
            grid:InitData(currentItemData)
            -- 最佳实践: 建议在Unity Editor中为Content节点添加GridLayoutGroup组件来自动布局，这样就不需要手动计算坐标了。
            grid.obj.transform.localPosition = Vector3((i - 1) % 4 * 135, -math.floor((i - 1) / 4) * 135, 0)
            grid.obj:SetActive(true)
        else
            -- B. 对象池格子不足，创建新的
            grid = ItemGrid:new()
            local index = i -- 捕获循环变量
            grid:Init(self.controls["svBag"]["ScrollRect"].transform:Find("Viewport"):Find("Content"),
                (index - 1) % 4 * 135, math.floor((index - 1) / 4) * 135,
                function()
                    -- 异步加载完成后，初始化数据并将其加入对象池
                    grid:InitData(currentItemData)
                    table.insert(self.gridPool, grid)
                end
            )
        end
    end
    
    -- 4. 更新活动格子的数量
    self.activeGridCount = #nowItems
end

-- 旧的 HideItems 函数不再需要，其逻辑已被整合到 ChangeType 中
-- function BagPanel:HideItems() ... end