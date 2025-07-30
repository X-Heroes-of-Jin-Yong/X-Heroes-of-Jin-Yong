--[[
金庸群侠传X BUFF逻辑扩展

]]--

local Tools = luanet.import_type('JyGame.Tools')
local Debug = luanet.import_type('UnityEngine.Debug')
local LuaTool = luanet.import_type('JyGame.LuaTool')
local CommonSettings = luanet.import_type('JyGame.CommonSettings')
local RuntimeData = luanet.import_type('JyGame.RuntimeData')
local Color = luanet.import_type('UnityEngine.Color')

--战斗中每回合执行的BUFF逻辑
--如中毒、恢复、内伤等
function BUFF_OnRoundBuff(buff, result)
	local level = buff.Level
	local owner = buff.Owner
	--print("正在执行"..buff.Owner.Name.."的BUFF逻辑:"..buff.Name)
	
	--DEBUFF例子：诅咒，每回合掉当前生命的1% * level
	if(buff.Name == "诅咒") then
		local deschp = math.ceil(owner.Hp * 0.01 * level)
		owner.Hp = owner.Hp - deschp
		owner:AttackInfo("诅咒 -"..deschp, Color.red)
		owner:Refresh()
	end
	
end

