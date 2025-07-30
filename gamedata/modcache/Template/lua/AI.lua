--[[
金庸群侠传X 战斗AI扩展

]]--

local Tools = CS.JyGame.Tools
local Debug = CS.UnityEngine.Debug
local LuaTool = CS.JyGame.LuaTool
local CommonSettings = CS.JyGame.CommonSettings
local RuntimeData = CS.JyGame.RuntimeData

--如果使用自定义AI，则return修改过的rst
function AI_GetAIResult(battleLogic,rst)
	--print("getting AI from lua..")
	
	
	--不使用扩展AI
	return nil
end
