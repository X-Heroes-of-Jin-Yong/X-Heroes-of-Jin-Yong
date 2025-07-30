--[[
金庸群侠传X 物品逻辑扩展

]]--

local Tools = luanet.import_type('JyGame.Tools')
local Debug = luanet.import_type('UnityEngine.Debug')
local LuaTool = luanet.import_type('JyGame.LuaTool')
local CommonSettings = luanet.import_type('JyGame.CommonSettings')
local RuntimeData = luanet.import_type('JyGame.RuntimeData')
local Color = luanet.import_type('UnityEngine.Color')


--尝试使用物品(分解词条)
--说明：实现一个物品TRIGGER分两部分，一部分是在本函数定义，将结果存在itemResult里，另外一部分是
--在以下两个函数中实现。
function ITEM_OnTryUse(sourceRole, targetRole, itemTrigger, itemResult)
	--print("item trigger = "..itemTrigger)
	--itemResult.data["test"] = "1"
	
	if itemTrigger.Name == "提升定力" then
		itemResult.data["dingli"] = tonumber(itemTrigger.Argvs[0])
	elseif itemTrigger.Name == "清除所有BUFF" then
		itemResult.data["clearbuffs"] = true
	end
end


--执行战斗中使用物品结果(如加血、加蓝)
function ITEM_OnItemResultRun(result, targetSprite, battleField)

	if(result.data["clearbuffs"] ~= nil) then
		targetSprite.Buffs:Clear()
		targetSprite:AttackInfo("清除所有BUFF", Color.red)
		targetSprite:Refresh()
	end
end

--执行永久提升性物品（如提升血内上限）
function ITEM_OnUseUpgradeItem(result, role)

	if(result.data["dingli"] ~= nil) then
		role.dingli = role.dingli + result.data["dingli"]
	end
end