Object:subClass("BasePanel")

BasePanel.panelObj = nil
BasePanel.panelName = nil
BasePanel.controls = {}

function BasePanel:Init(preName,onComplete)
    if self.panelObj then
        return
    end
    AAMgr:InstantiateAsync(preName,
        function(panel)
            if panel == nil then
                print("错误: " .. preName .. " 实例化失败!")
                return
            end
            print(preName.." 实例化成功!")
            self.panelObj = panel 
            self.panelName = preName   
            --2.查找关联控件
            local allControls = self.panelObj:GetComponentsInChildren(typeof(UIBehaviour))
            --3.根据物体名字排除不需要的组件,将需要的组件放到成员数组中
            for i = 0, allControls.Length-1 do
                local controlName = allControls[i].name
                if string.find(controlName,"btn") ~= nil or
                string.find(controlName,"tog") ~= nil or
                string.find(controlName,"img") ~= nil or
                string.find(controlName,"sv") ~= nil or
                string.find(controlName,"txt") ~= nil then
                    if self.controls[controlName] then
                        self.controls[controlName][allControls[i]:GetType().Name] = allControls[i]
                    else
                        self.controls[controlName] = {[allControls[i]:GetType().Name] = allControls[i]}
                    end
                end
            end
            self.panelObj:SetActive(true)
            if onComplete then
                onComplete()
            end
        end,
        Canvas.transform
    )
end

function BasePanel:ShowPanel()
    if self.panelObj == nil then
        self:Init(self.panelName)
    else 
        self.panelObj:SetActive(true)
    end
end

function BasePanel:HidePanel()
    if self.panelObj then
        self.panelObj:SetActive(false)
    end
end