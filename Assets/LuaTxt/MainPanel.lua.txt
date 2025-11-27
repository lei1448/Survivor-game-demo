MainPanel = {}
--设置需要的成员变量
MainPanel.panelObj = nil
MainPanel.btnRole = nil
MainPanel.btnSkill = nil
--实例化面板
function MainPanel:Init(onComplete)
    --加载AA包资源
    if self.panelObj then
        return
    end
    AAMgr:InstantiateAsync("MainPanel",
        function(mainPanel)
            if mainPanel == nil then
                print("错误: MainPanel 实例化失败!")
                return
            end
            print("MainPanel 实例化成功!")
            self.panelObj = mainPanel     
            --2.查找关联控件
            self.btnRole = self.panelObj.transform:Find("btnRole"):GetComponent(typeof(Button))
            self.btnRole.onClick:AddListener(
                function()
                    self:OnBtnRoleClick()
                end
            )
            if onComplete then
                onComplete()
            end
        end,
        Canvas.transform
    )
end

function MainPanel:ShowMainPanel()
    if self.panelObj == nil then
        self:Init(
            function()    
                self.panelObj:SetActive(true)
            end
        )
    else 
        self.panelObj:SetActive(true)
    end
end

function MainPanel:HideMainPanel()
    if self.panelObj then
        self.panelObj:SetActive(false)
    end
end

function MainPanel:OnBtnRoleClick()
    BagPanel:ShowBagPanel()
end