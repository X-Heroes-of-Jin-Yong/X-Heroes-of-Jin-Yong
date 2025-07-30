--[[
 金庸群侠传X外接脚本公用配置文件
 汉家松鼠工作室(http://www.jy-x.com)
]]--

local Debug = CS.UnityEngine.Debug
local Color = CS.UnityEngine.Color
local Tools = CS.JyGame.Tools
local LuaTool = CS.JyGame.LuaTool
local AudioManager = CS.JyGame.AudioManager
local logger = CS.JyGame.FileLogger.instance
local RuntimeData = CS.JyGame.RuntimeData

--载入的LUA文件列表
function ROOT_getLuaFiles()
	return {
		"rollrole.lua",
		"triggerlogic.lua",
		"AttackLogic.lua",
		"GameEngine.lua",
		"AI.lua",
		"battle.lua",
		"skill.lua",
		"buff.lua",
		"item.lua",
	}
end

--资源载入完成
--这里一般用于做数据调试和测试
function ROOT_onInitedResources()
	logger:Log("game started..")
end

--配置列表
function ROOT_getConfigList()
	return {
		--主菜单音乐（默认："音乐.武侠回忆"）
		MAINMENU_MUSIC = "音乐.武侠回忆",
		
		--主菜单背景图（默认："地图.桃花岛"）
		MAINMENU_BG = "地图.桃花岛",
	
		--开场剧本（默认："新手村_出生"）
		gamestart_story = "新手村_出生",	
		
		--开场地点（默认："南贤居"）
		gamestart_location = "南贤居",
		
		--每周目敌人增加攻击力比例（默认：0.1）
		ZHOUMU_ATTACK_ADD = 0.1,
		
		--每周目敌人增加的防御力比例（默认：0.08）
		ZHOUMU_DEFENCE_ADD = 0.08,
		
		--每周目敌人增加血量比例（默认：0.15）
		ZHOUMU_HP_ADD = 0.15,
		
		--每周目敌人增加内力比例（默认：0.15）
		ZHOUMU_MP_ADD = 0.15,
		
		--多少个周目提高一级武功等级上限（默认：2）
		PER_MAXLEVEL_ADD_BY_ZHOUMU = 2,
		
		--NPC多少个周目提高所有武功等级1级（默认：2）
		NPC_SKILL_LEVEL_ADD_BY_ZHOUMU = 2,
		
		--默认最大战斗时间（默认：3000）
		DEFAULT_MAX_GAME_SPTIME = 3000,
		
		--是否在游戏中开启控制台（默认：true）
		CONSOLE = true,
		
		--最大内功数量（默认：5）
		MAX_INTERNALSKILL_COUNT = 5,
		
		--最大外功数量（默认：10）
		MAX_SKILL_COUNT = 10,
		
		--最大可分点属性值（默认：200）
		MAX_ATTRIBUTE = 200,
		
		--最大外功等级（默认：20）
		MAX_SKILL_LEVEL = 20,
		
		--最大内功等级（默认：20）
		MAX_INTERNALSKILL_LEVEL = 20,
		
		--战斗元宝掉率(0.5%)
		YUANBAO_DROP_RATE = 0.005,
		
		--最大血内上限（默认：10000）
		MAX_HPMP = 10000,
		
		--每周目增长血内上限（默认：1000）
		MAX_HPMP_PER_ROUND = 1000,
		
		--人物等级上限（默认：30）
		MAX_LEVEL = 30,
		
		--箱子物品上限（默认：8 + RuntimeData.Instance.Round * 6）
		MAX_XIANGZI_ITEM_COUNT = 8 + RuntimeData.Instance.Round * 6,

		--困难残章掉率(1.5%，默认：0.015)
		HARD_MODE_CANZHANG_DROPRATE = 0.015,
		
		--炼狱以上难度残章基础掉率(3%，默认：0.03)
		CRAZY_MODE_CANZHANG_DROPRATE = 0.03;
		
		--炼狱以上难度残章基础掉率每周目递增(0.5%，默认：0.005)
		CRAZY_MODE_CANZHANG_DROPRATE_PER_ROUND = 0.005,
		
		--外功最大掉落残章的武学难度值(不含，默认：8)
		CANZHANG_MAX_HARD_SKILL = 8,
		
		--内功最大掉落残章的武学难度值(不含，默认：8)
		CANZHANG_MAX_HARD_INTERNALSKILL = 8,
		
		--外功残章掉率是内功的多少倍（默认：2.0）
		CANZHANG_DROP_RATE_INTERNAL_RATE = 2.0,
		
		--随机战斗音乐
		randomBattleMusics = LuaTool.MakeStringArray({
			"战斗音乐.云狐之战", 
            "战斗音乐.暮云出击", 
            "战斗音乐.山谷行进", 
            "战斗音乐.山谷行进2", 
            "战斗音乐.2",
            "战斗音乐.3",
            "战斗音乐.4",
            "战斗音乐.5",
            "音乐.天龙八部.紧张感3",
            "音乐.天龙八部.紧张感4",
		}),
		
		--困难难度 NPC随机天赋池
		EnemyRandomTalentsList = LuaTool.MakeStringArray({
			"飘然",
            "斗魂",
            "哀歌",
            "奋战",
            "百足之虫",
            "真气护体",
            "暴躁",
            "金钟罩",
            "诸般封印",
            "刀封印",
            "剑封印",
            "奇门封印",
            "拳掌封印",
            "自我主义",
            "大小姐",
            "破甲",
            "好色",
            "瘸子",
            "白内障",
            "左手剑",
            "右臂有伤",
            "拳掌增益",
            "剑法增益",
            "刀法增益",
            "奇门增益",
            "锐眼"
		}),
		
		--说明：炼狱、无悔难度是每次从以下3个天赋池中各取一个
		--炼狱、无悔难度下NPC随机天赋池1
		EnemyRandomTalentListCrazy1 = LuaTool.MakeStringArray({
			"百足之虫",
            "真气护体",
            "金钟罩",
            "苦命儿",
            "老江湖",
            "暴躁",
            "灵心慧质",
            "精打细算",
            "白内障",
            "右臂有伤",
            "神经病",
            "鲁莽",
		}),
		
		--炼狱、无悔难度下NPC随机天赋池2
		EnemyRandomTalentListCrazy2 = LuaTool.MakeStringArray({
			"斗魂",
            "奋战",
            "暴躁",
            "自我主义",
            "破甲",
            "铁拳无双",
            "素心神剑",
            "左右互搏",
            "博览群书",
            "阴谋家",
            "琴胆剑心",
            "追魂",
            "铁口直断",
            "左手剑",
            "拳掌增益",
            "剑法增益",
            "刀法增益",
            "奇门增益",
            "锐眼"
		}),
		
		--炼狱、无悔难度下NPC随机天赋池3
		EnemyRandomTalentListCrazy3 = LuaTool.MakeStringArray({
			"刀封印",
            "剑封印",
            "奇门封印",
            "拳掌封印",
            "清心",
            "哀歌",
            "幽居",
            "金刚",
            "嗜血狂魔",
            "清风",
            "御风",
            "轻功高超",
            "瘸子",
		}),
		
		--同福客栈小游戏最大提升到的属性值（默认：70）
		SMALLGAME_MAX_ATTRIBUTE = 70,
		
		--奥义呐喊（女声）
		AOYI_SOUND_FEMALE = LuaTool.MakeStringArray({
			"音效.女", "音效.女2", "音效.女3", "音效.女4"
		}),
		
		--奥义呐喊（男声）
		AOYI_SOUND_MALE = LuaTool.MakeStringArray({
			"音效.男", "音效.男2", "音效.男3", "音效.男4", "音效.男5", "音效.男-哼"
		}),
		
		--奥义音效（随机选取）
		AOYI_EFFECT = LuaTool.MakeStringArray({
			"音效.内功攻击4", "音效.打雷", "音效.奥义1", "音效.奥义2", "音效.奥义3", "音效.奥义4", "音效.奥义5", "音效.奥义6"
		}),
		
		--珍珑棋局随机掉落武器
		ZHENLONG_WUQI = LuaTool.MakeStringArray({
			 "真.天龙宝剑","玄铁剑","冷月宝刀","被诅咒的木刀","真.倚天剑","真.屠龙刀","真.打狗棒","真.灭仙爪","打狗棒"
		}),
		
		--珍珑棋局防具
		ZHENLONG_FANGJU = LuaTool.MakeStringArray({
			"黄金重甲","幽梦衣","霓裳羽衣","岳飞的重铠","千变魔女的披风","华裳","乌蚕衣","三清袍"
		}),
		
		--珍珑棋局饰品
		ZHENLONG_SHIPIN = LuaTool.MakeStringArray({
			"橙色灯戒","铂金戒指","蓝宝戒指","水晶护符","魔神信物","神奇戒指"
		}),
		
		--BUFF列表
		BUFF_LIST = LuaTool.MakeStringArray({
			"恢复", "集气", "攻击强化", "飘渺", "左右互搏", "神速攻击", "醉酒", "溜须拍马", "易容", 
            "狂战", "坚守", "沾衣十八跌", "圣战", "轻身", "防御强化", "魔神降临", "神行"
		}),
		
		--DEBUFF列表
		DEBUFF_LIST = LuaTool.MakeStringArray({
			"中毒", "内伤", "致盲", "缓速", "晕眩", "攻击弱化", "诸般封印", "剑封印", "刀封印", 
            "拳掌封印", "奇门封印", "伤害加深", "重伤", "定身", "封穴", "点穴", "诅咒"
		}),
		
		--游戏战斗加速倍速（默认：1.5）
		BATTLE_SPEEDUP_RATE = 1.5,
		
		--战斗场景X坐标格数（默认：11）
		BATTLE_MOVEBLOCK_MAX_X = 11,

		--战斗场景Y坐标格数（默认：4）
		BATTLE_MOVEBLOCK_MAX_Y = 4,

		--战斗场景格子长度（默认：80）
		BATTLE_MOVEBLOCK_LENGTH = 80,

		--战斗场景格子宽度（默认：80）
		BATTLE_MOVEBLOCK_WIDTH = 80,

		--战斗场景右边距（默认：5，单位：格子数）
		BATTLE_MOVEBLOCK_MARGIN_RIGHT = 5,

		--战斗场景上边距（默认：2，单位：格子数）
		BATTLE_MOVEBLOCK_MARGIN_TOP = 2,

		--战斗场景格子大小修正（默认：1.25，单位：倍数）
		--注意：格子的实际大小为64*64，但金X默认值为1.25倍（即80*80）
		BATTLE_MOVEBLOCK_SCALE = 1.25,
	}
end