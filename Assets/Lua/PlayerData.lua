PlayerData = {}
PlayerData.equips = {}
PlayerData.items = {}
PlayerData.gems = {}

function PlayerData:Init(onComplete)
    --总任务数量
    local sumTask = 3
    --作为回调函数统计完成任务数量
    local function CheckIsComplete()
        sumTask = sumTask - 1
        if sumTask == 0 then
            if onComplete then
                onComplete()
            end
        end
    end
    --开始加载
    LoadPlayerData("equips",self.equips,CheckIsComplete)
    LoadPlayerData("items",self.items,CheckIsComplete)
    LoadPlayerData("gems",self.gems,CheckIsComplete)
end

function LoadPlayerData(str,bag,callback)
    AAMgr:LoadAssetAsync(str,
        function(playerData)
            if playerData then
                print(str.." 资源加载成功！")   
                --解析数据
                local data = Json.decode(playerData.text)
                --插入数据
                for k, v in pairs(data) do
                    table.insert(bag,{id = v.id,num = v.num})
                end    
            end     
            --回调
            if callback then
                callback()
            end
        end
    )
end

function PlayerData:PrintData()
    print("--- 打印当前玩家数据 ---")
    print("Equips:", Json.encode(self.equips))
    print("Items:", Json.encode(self.items))
    print("Gems:", Json.encode(self.gems))
    print("------------------------")
end

function PlayerData:PrintFinished()
    print("玩家数据加载成功")
end