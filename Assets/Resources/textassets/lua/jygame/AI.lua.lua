--[[
金庸群侠传X 战斗AI扩展

]]--

local Tools = luanet.import_type('JyGame.Tools')
local Debug = luanet.import_type('UnityEngine.Debug')
local LuaTool = luanet.import_type('JyGame.LuaTool')
local CommonSettings = luanet.import_type('JyGame.CommonSettings')
local RuntimeData = luanet.import_type('JyGame.RuntimeData')

--如果使用自定义AI，则return修改过的rst
function AI_GetAIResult(battleLogic,rst)
	--print("getting AI from lua..")
	
	
	--不使用扩展AI
	return nil
end
