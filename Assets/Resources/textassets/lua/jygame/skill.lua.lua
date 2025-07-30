--[[
金庸群侠传X 技能逻辑扩展

]]--

local Tools = luanet.import_type('JyGame.Tools')
local Debug = luanet.import_type('UnityEngine.Debug')
local LuaTool = luanet.import_type('JyGame.LuaTool')
local CommonSettings = luanet.import_type('JyGame.CommonSettings')
local RuntimeData = luanet.import_type('JyGame.RuntimeData')
local Color = luanet.import_type('UnityEngine.Color')

--技能覆盖范围
function SKILL_getSize(skill ,sprite, size)
	return size
end

--技能施展范围
function SKILL_getCastSize(skill ,sprite, size)
	return size
end

