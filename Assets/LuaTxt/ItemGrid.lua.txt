Object:subClass("ItemGrid")

ItemGrid.obj = nil
ItemGrid.numText = nil
ItemGrid.iconImg = nil

--单个格子初始化自己,通过AA包加载模板填充自己的内存,并根据外部传入的数据设置自己在局内的表现
function ItemGrid:Init(father,posX,poY,callBack)
     AAMgr:InstantiateAsync("ItemGrid",
        function(item)
            if not item then return end
            self.obj = item
            item.transform.localPosition = Vector3(posX, -poY, 0)                    
            self.iconImg = item.transform:Find("Icon"):GetComponent(typeof(Image))
            self.numText = item.transform:Find("Text"):GetComponent("TMPro.TextMeshProUGUI")
            if callBack then
                callBack()
            end
        end,
        father
    )
end

--通过外部数据初始化自己的信息
function ItemGrid:InitData(data)
    self.numText.text = data.num
    --BagPanel.items[data.id] = ItemGrid.obj 
    local itemInfo = ItemData[data.id]
    local iconParts = string.split(itemInfo.icon, "_")
    AAMgr:LoadAssetAsync(iconParts[1],
        function(atlas)
            if atlas and self.iconImg then
                self.iconImg.sprite = atlas:GetSprite(iconParts[2])
            end
        end
    )
end