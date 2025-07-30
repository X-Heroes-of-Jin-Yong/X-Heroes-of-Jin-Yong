--[[
金庸群侠传X 游戏引擎扩展

]]--

local Tools = luanet.import_type('JyGame.Tools')
local Debug = luanet.import_type('UnityEngine.Debug')
local LuaTool = luanet.import_type('JyGame.LuaTool')
local CommonSettings = luanet.import_type('JyGame.CommonSettings')
local RuntimeData = luanet.import_type('JyGame.RuntimeData')
local ModData = luanet.import_type('JyGame.ModData')
local AudioManager = luanet.import_type('JyGame.AudioManager')
local LuaManager = luanet.import_type('JyGame.LuaManager')

--控制台指令+游戏内的跳转指令
function GameEngine_extendConsole(gameEngine,statusType,value)
	
	if(statusType == "test") then
		print(value)
		--扩展的指令，必须返回true
		return true
	elseif(statusType == "get_money") then
		RuntimeData.Instance.Money = RuntimeData.Instance.Money + tonumber(value)
		AudioManager.Instance:PlayEffect("音效.升级")
		--部分参数生效时，需要刷新一次状态栏
		RuntimeData.Instance.MapUI:RefreshRoleState()
		return true 
	elseif(statusType == "play_music") then
		AudioManager.Instance:Play(value)
		return true
	end
	
	return false
end

--扩展STORY ACTION
function GameEngine_extendStoryAction(this,action,paras,callback)
	
	if(action.type == "TEST_PRINT") then
		print(action.value)
		--继续执行下一条ACTION
		this:ExecuteNextStoryAction(callback)
		return true
	end
	
	return false
end

--扩展江湖历练UI面板
function GameEngine_jianghuContent(this)

	local result
	
	result = "道德：<color='red'>" .. RuntimeData.Instance.Daode .. "</color>\n"
	result = result .. "女主角好感：<color='red'>" .. RuntimeData.Instance:getHaogan() .. "</color>\n"
	--添加其他角色好感需要RuntimeData.Instance:getHaogan("角色名字")，以下是例子：
	--result = result .. "阿青好感：<color='red'>" .. RuntimeData.Instance:getHaogan("阿青") .. "</color>\n"
	result = result .. "本周目生命、内力上限：<color='red'>" .. CommonSettings.MAX_HPMP .. "</color>\n"
	result = result .. "松鼠旅馆箱子存储量：<color='red'>" .. RuntimeData.Instance.XiangziCount .. "/" .. RuntimeData.Instance.MaxXiangziItemCount .. "</color>\n"
	result = result .. "\n"
	result = result .. "亲爱的大侠，您在金X的世界中一共：\n"
	result = result .. "  死亡：<color='red'>" .. ModData.GetParam(ModData.DEAD_KEY) .. "次</color>\n"
	result = result .. "  存档：<color='red'>" .. ModData.GetParam(ModData.SAVE_KEY) .. "次</color>\n"
	result = result .. "  击杀：<color='red'>" .. ModData.GetParam(ModData.TOTALKILL_KEY) .. "个</color>\n"
	result = result .. "  通关次数：<color='red'>" .. ModData.GetParam(ModData.END_COUNT_KEY) .. "</color>\n"
	result = result .. "  最高周目：<color='red'>" .. math.max(unpack({RuntimeData.Instance.Round, ModData.GetParam(ModData.MAX_ROUND_KEY)})) .. "</color>\n"

	return result

end