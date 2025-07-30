--[[
金庸群侠传X 战斗逻辑扩展

]]--

local Tools = luanet.import_type('JyGame.Tools')
local Debug = luanet.import_type('UnityEngine.Debug')
local LuaTool = luanet.import_type('JyGame.LuaTool')
local CommonSettings = luanet.import_type('JyGame.CommonSettings')
local RuntimeData = luanet.import_type('JyGame.RuntimeData')
local Color = luanet.import_type('UnityEngine.Color')

--在某个角色行动之前调用
function BATTLE_BeforeRoleAction(battlefield, currentSprite)
	--currentSprite:AttackInfo("轮到我行动啦!", Color.black)
	
	--例子天赋：战神，每回合一定概率自动叠加攻击强化BUFF
	if(currentSprite.Role:HasTalent("战神") and Tools.ProbabilityTest(0.3)) then
		currentSprite:AddBuff("攻击强化",5,5)
	end
end


--角色施展技能之后(播放完技能攻击动画）调用
function BATTLE_AfterSkillAnimation(battlefield, currentSprite, skill, hitnumber)
	--currentSprite:AttackInfo("我打中了"..hitnumber.."个人！", Color.red)
	
	--例子天赋：赚便宜，如果同时打中4个人以上，恢复最大生命的10%
	if(currentSprite.Role:HasTalent("赚便宜") and hitnumber>=4) then
		local hpadd = math.ceil(currentSprite.MaxHp * 0.1)
		currentSprite.Hp = currentSprite.Hp + hpadd
		currentSprite:AttackInfo("+" .. hpadd, Color.green)
		currentSprite:RandomSay(LuaTool.MakeStringArray({"哈哈，这便宜赚得爽!", "有便宜就要赚！"}))
	end
	
end

--角色技能命中目标后（播放技能攻击动画之前）调用
--hitnumber : 命中目标个数
function BATTLE_BeforeSkillAnimation(battlefield, currentSprite, skill, hitnumber)
	--currentSprite:AttackInfo("我打中了"..hitnumber.."个人！", Color.white)
	
	--例子天赋：空挥狂魔，没有打中人的情况下，自动攻击最近的一个人敌人
	if(currentSprite.Role:HasTalent("空挥狂魔") and hitnumber == 0) then
		--寻找最近的敌人
		mindist = 9999
		findSprite = nil
		for _,sprite in pairs(battlefield.SpritesTable) do
			if sprite.Team ~= currentSprite.Team then
				local distx = math.abs(currentSprite.X - sprite.X)
				local disty = math.abs(currentSprite.Y - sprite.Y)
				local dist = distx + disty
				if(dist < mindist) then
					mindist = dist
					findSprite = sprite
				end
			end
		end
		
		--如果找到了，攻击他
		if(findSprite ~= nil) then
			battlefield:Attack(currentSprite, skill, findSprite.X, findSprite.Y)
		end
	end
end

--角色濒死时调用
function BATTLE_Die(battlefield, sprite, attackResult)

	--天赋“不死”， 80%概率不死，剩1滴血
	if(sprite.Role:HasTalent("不死") and Tools.ProbabilityTest(0.8)) then
		sprite:Say("就是死不了，你打我啊")
		sprite.Hp = 1
		return true
	end

	return false --继续执行死亡逻辑, 返回true的话就不死
end

--角色休息时调用
function BATTLE_Rest(battlefield, sprite, hprecover, mprecover)
	--sprite:Say("我在休息...")
	
	--例子天赋:懒虫，休息时比别人获得多100点生命回复，但有一定概率晕眩
	if(sprite.Role:HasTalent("懒虫")) then
		hprecover = hprecover + 100
		if(Tools.ProbabilityTest(0.1)) then
			sprite:AddBuff("晕眩",2,5)
			sprite:Say("啊。。好好睡一觉")
		end
	end
	
	--固定返回格式
	return string.format("%s,%s", hprecover, mprecover)
end
