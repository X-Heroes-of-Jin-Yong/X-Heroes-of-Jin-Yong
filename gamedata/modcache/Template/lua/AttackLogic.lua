--[[
金庸群侠传X 攻击逻辑扩展

]]--

local Tools = CS.JyGame.Tools
local Debug = CS.UnityEngine.Debug
local LuaTool = CS.JyGame.LuaTool
local CommonSettings = CS.JyGame.CommonSettings
local RuntimeData = CS.JyGame.RuntimeData

--扩展特殊攻击
function AttackLogic_extendSpecialSkill(result, skill, sourceSprite, targetSprite, bf)
	if(skill.Name == "扳手腕") then
		--施展技能弹的对话
		result:AddCastInfo(sourceSprite, LuaTool.MakeStringArray({"喝啊！","比比谁的力气大！！"}))
		
		--造成攻击者100+ 0~50*臂力差 的伤害
		local deltaBili = math.abs(sourceSprite.Role.AttributesFinal["bili"], targetSprite.Role.AttributesFinal["bili"])
		result.Hp = 100 + Tools.GetRandomInt(0, 50*deltaBili)
	end
	
	--请勿修改
	return result
end

--扩展天赋（包含在AI计算内）
--不要在此函数中直接扣除血量、或者调用sprite的讲话函数，此函数所有的操作应该针对(AttackResult)result变量进行
function AttackLogic_extendTalents(sourceSprite, targetSprite, skill, bf, result)

	--天赋：大力士，每次攻击附加臂力的50%的伤害
	if(sourceSprite.Role:HasTalent("大力士")) then
		result.Hp = result.Hp + tonumber(sourceSprite.Role.AttributesFinal["bili"] * 0.5)
		result:AddCastInfo(sourceSprite, LuaTool.MakeStringArray({"受死吧，小不点！","大力士就是我！"}))
	end
end

--扩展天赋2（不包含在AI计算内）
--与扩展天赋区别是在此的修改是立即结算
function AttackLogic_extendTalents2(sourceSprite, targetSprite, skill, bf, result)
	
	--天赋：神.化功大法，20%概率直接将对手内力打空
	if(sourceSprite.Role:HasTalent("神.化功大法")) then
		if(Tools.ProbabilityTest(0.2)) then
			targetSprite.Mp = 0
			bf:Log(sourceSprite.Role.Name .. "天赋【神.化功大法】发动！直接打空" .. targetSprite.Role.Name .. "的内力!") --记录日志
			sourceSprite:Say("见识见识让人闻风丧胆的化功大法吧！")
		end
	end
	
	--天赋：诅咒，100%叠加诅咒BUFF(等级5，持续5回合)
	if(sourceSprite.Role:HasTalent("诅咒")) then
		targetSprite:AddBuff("诅咒",5,5)
	end
end

--扩展天赋3（包含在AI计算内，并参与计算公式）
--与扩展天赋区别是参与了最小攻击、最大攻击、暴击、防御相关的计算公式，但请不要对result.Hp进行赋值
function AttackLogic_extendTalents3(sourceSprite, targetSprite, skill, bf, result, formula)

	--天赋：不稳定的野球拳（最小攻击降低50%，最大攻击提高50%）
	if(sourceSprite.Role:HasTalent("不稳定的野球拳") and skill.Name == "野球拳") then
		formula.attackLow = formula.attackLow * 0.5
        if (formula.attackLow < 0) then
            formula.attackLow = 0
        end
        formula.attackUp = formula.attackUp * 1.5
	end
end