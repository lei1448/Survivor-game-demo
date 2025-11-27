local itemdata = nil
local itemList = nil
AAMgr:LoadAssetAsync("ItemData", function(loadedAsset)
    if loadedAsset then
        print("物品资源加载成功！")
        itemdata = loadedAsset.text
        itemList = Json.decode(itemdata)
        ItemData = {}
        for k, v in pairs(itemList) do
            ItemData[v.id] = v
        end
        --[[
        for k, v in pairs(ItemData) do
            print(k,v)
        end
        ]]--
    else
        print("物品资源加载失败！")
    end
end)