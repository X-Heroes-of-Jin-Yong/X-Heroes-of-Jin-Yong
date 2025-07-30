--[[
金庸群侠传X 技能逻辑扩展

]]--

local Tools = CS.JyGame.Tools
local Debug = CS.UnityEngine.Debug
local LuaTool = CS.JyGame.LuaTool
local CommonSettings = CS.JyGame.CommonSettings
local RuntimeData = CS.JyGame.RuntimeData
local Color = CS.UnityEngine.Color

--技能覆盖范围
function SKILL_getSize(skill ,sprite, size)
	return size
end

--技能施展范围
function SKILL_getCastSize(skill ,sprite, size)
	return size
end

