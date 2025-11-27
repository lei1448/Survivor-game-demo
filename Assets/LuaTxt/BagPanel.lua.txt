BagPanel = {}

BagPanel.panelObj = nil
BagPanel.togEquip = nil
BagPanel.togItem = nil
BagPanel.togGem = nil
BagPanel.btnClose = nil
BagPanel.svBag = nil
BagPanel.Content = nil
BagPanel.nowType = -1
BagPanel.items = {}
function BagPanel:Init(onComplete)
    if self.panelObj then
        return
    end
    AAMgr:InstantiateAsync("BagPanel",
        function(bagPanel)
            if bagPanel == nil then
                print("错误: BagPanel 实例化失败!")
                return
            end
            print("BagPanel 实例化成功!")
            self.panelObj = bagPanel     
            --2.查找关联控件
            --关闭按钮
            self.btnClose = self.panelObj.transform:Find("btnClose"):GetComponent(typeof(Button))
            self.btnClose.onClick:AddListener(
                function()
                    self:OnCloseBtnClick()
                end
            )
            --Toggle
            local togGroup = self.panelObj.transform:Find("togGroup")
            self.togEquip = togGroup.transform:Find("togEquip"):GetComponent(typeof(Toggle))
            self.togItem = togGroup.transform:Find("togItem"):GetComponent(typeof(Toggle))
            self.togGem = togGroup.transform:Find("togGem"):GetComponent(typeof(Toggle))
            --ScrollView
            self.svBag = self.panelObj.transform:Find("svBag"):GetComponent(typeof(ScrollRect))
            self.Content = self.svBag.transform:Find("Viewport"):Find("Content")

            --单选框事件
            self.togEquip.onValueChanged:AddListener
            (
                function(value)
                    if value then self:ChangeType(1) end
                end
            )
            self.togItem.onValueChanged:AddListener
            (
                function(value)
                    if value then self:ChangeType(2) end
                end
            )
            self.togGem.onValueChanged:AddListener
            (
                function(value)
                    if value then self:ChangeType(3) end
                end
            )

            if onComplete then
                onComplete()
            end
        end,
        Canvas.transform
    )
end

function BagPanel:ShowBagPanel()
    if self.panelObj == nil then
        self:Init(
            function()    
            self:ChangeType(1)
            self.panelObj:SetActive(true)
            end
        )
        else 
            self.panelObj:SetActive(true)
            self:ChangeType(1)
            self.togEquip.isOn = true
        end
end
function BagPanel:HideBagPanel()
    if self.panelObj then
        self.panelObj:SetActive(false)
    end
end
function BagPanel:OnCloseBtnClick()
    self:HideBagPanel()
end

function BagPanel:ChangeType(type)
    if self.nowType == type then
        return
    end
    self.nowType = type -- 立即更新，防止并发

    -- 1. 隐藏所有当前显示的物品
    self:HideItems()

    -- 2. 获取目标物品数据
    local nowItems = nil
    if type == 1 then
        nowItems = PlayerData.equips
    elseif type == 2 then
        nowItems = PlayerData.items
    else
        nowItems = PlayerData.gems
    end

    -- 3. 遍历数据，更新或创建格子
    for i = 1, #nowItems do
        -- 捕获循环变量，解决异步闭包问题
        local index = i
        local currentItemData = nowItems[index]

        if self.items[currentItemData.id] then
             -- 如果物品已在池中，直接激活
             self.items[currentItemData.id]:SetActive(true)
        else
            local grid = ItemGrid:new()
            grid:Init(self.Content,(index - 1) % 4 * 135, math.floor((index - 1) / 4) * 135,
                function()
                    grid:InitData(currentItemData)
                    self.items[currentItemData.id] = grid.obj
                end
            )
        end
    end
end

function BagPanel:HideItems()
    for _, itemObj in pairs(self.items) do
        if itemObj then
            itemObj:SetActive(false)
        end
    end
end