--[[
金庸群侠传X 逻辑语法扩展脚本

]]--

local Tools = CS.JyGame.Tools
local Debug = CS.UnityEngine.Debug
local LuaTool = CS.JyGame.LuaTool
local CommonSettings = CS.JyGame.CommonSettings
local RuntimeData = CS.JyGame.RuntimeData

--需要扩展的判断条件写在这里
TriggerLogic_pluginConditions = {
	"is_zhujue_name", --判断主角名字是否等于
}

--逻辑实现写在这里
function TriggerLogic_judge(condition)
	
	if(condition.type=="is_zhujue_name") then
		return RuntimeData.Instance.Team[0].Name == condition.value
	end
	
end

--不要改本函数
function TriggerLogic_getExtensionConditions()
	return LuaTool.MakeStringArray(TriggerLogic_pluginConditions)
end