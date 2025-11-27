BasePanel:subClass("MainPanel")
function MainPanel:Init()
    self.base.Init(self,"MainPanel",
        function()
            self.controls["btnRole"]["Button"].onClick:AddListener(
                function()
                    self:OnBtnRoleClick()
                end
            )
        end
    )
end

function MainPanel:ShowPanel()
    self.base.ShowPanel(self)
end

function MainPanel:HidePanel()
    self.base.HidePanel(self)
end

function MainPanel:OnBtnRoleClick()
    BagPanel:ShowBagPanel()
end