using System;
using JyGame;
using UnityEngine;

/// <summary>
/// Template Mod 入口 — 原 Lua 文件的 C# 等效实现
/// 所有方法签名必须与内置 Ext 类一致，ModManager 加载后自动通过反射调用
/// 不需要覆写的方法可以删掉，游戏会使用内置默认逻辑
/// </summary>
public static class ModEntry
{
    // =====================================================
    // 战斗回调 (原 battle.lua)
    // =====================================================

    /// <summary>角色行动前</summary>
    public static void BATTLE_BeforeRoleAction(BattleField battlefield, BattleSprite currentSprite)
    {
        // 天赋：战神 — 30% 概率自动叠加攻击强化
        if (currentSprite.Role.HasTalent("战神") && Tools.ProbabilityTest(0.3))
        {
            currentSprite.Addbuff("攻击强化", 5, 5);
        }
    }

    /// <summary>技能命中后、动画播放前</summary>
    public static void BATTLE_BeforeSkillAnimation(BattleField battlefield, BattleSprite currentSprite, SkillBox skill, int hitNumber)
    {
        // 天赋：空挥狂魔 — 未命中时自动攻击最近敌人
        if (currentSprite.Role.HasTalent("空挥狂魔") && hitNumber == 0)
        {
            float minDist = 9999f;
            BattleSprite findSprite = null;
            foreach (BattleSprite sprite in battlefield.Sprites)
            {
                if (sprite.Team != currentSprite.Team)
                {
                    float dist = Mathf.Abs(currentSprite.X - sprite.X) + Mathf.Abs(currentSprite.Y - sprite.Y);
                    if (dist < minDist)
                    {
                        minDist = dist;
                        findSprite = sprite;
                    }
                }
            }
            if (findSprite != null)
                battlefield.Attack(currentSprite, skill, findSprite.X, findSprite.Y);
        }
    }

    /// <summary>技能动画播放后</summary>
    public static void BATTLE_AfterSkillAnimation(BattleField battlefield, BattleSprite currentSprite, SkillBox skill, int hitNumber)
    {
        // 天赋：赚便宜 — 命中 4+ 人恢复最大生命 10%
        if (currentSprite.Role.HasTalent("赚便宜") && hitNumber >= 4)
        {
            int hpAdd = (int)Math.Ceiling(currentSprite.MaxHp * 0.1);
            currentSprite.Hp += hpAdd;
            currentSprite.AttackInfo("+" + hpAdd, Color.green);
            currentSprite.RandomSay(new string[] { "哈哈，这便宜赚得爽!", "有便宜就要赚！" });
        }
    }

    /// <summary>角色濒死时，返回 true 阻止死亡</summary>
    public static bool BATTLE_Die(BattleField battlefield, BattleSprite sprite, AttackResult attackResult)
    {
        // 天赋：不死 — 80% 概率剩 1 血
        if (sprite.Role.HasTalent("不死") && Tools.ProbabilityTest(0.8))
        {
            sprite.Say("就是死不了，你打我啊");
            sprite.Hp = 1;
            return true;
        }
        return false;
    }

    /// <summary>角色休息，返回 "hp,mp" 字符串</summary>
    public static string BATTLE_Rest(BattleField battlefield, BattleSprite sprite, int hpRecover, int mpRecover)
    {
        // 天赋：懒虫 — 额外 +100 HP，10% 晕眩
        if (sprite.Role.HasTalent("懒虫"))
        {
            hpRecover += 100;
            if (Tools.ProbabilityTest(0.1))
            {
                sprite.Addbuff("晕眩", 2, 5);
                sprite.Say("啊。。好好睡一觉");
            }
        }
        return string.Format("{0},{1}", hpRecover, mpRecover);
    }

    // =====================================================
    // 攻击扩展 (原 AttackLogic.lua)
    // =====================================================

    /// <summary>扩展特殊技能</summary>
    public static AttackResult AttackLogic_extendSpecialSkill(AttackResult result, SkillBox skill, BattleSprite sourceSprite, BattleSprite targetSprite, BattleField bf)
    {
        if (skill.Name == "扳手腕")
        {
            result.AddCastInfo(sourceSprite, new string[] { "喝啊！", "比比谁的力气大！！" });
            int deltaBili = Math.Abs(sourceSprite.Role.AttributesFinal["bili"] - targetSprite.Role.AttributesFinal["bili"]);
            result.Hp = 100 + Tools.GetRandomInt(0, 50 * deltaBili);
        }
        return result;
    }

    /// <summary>扩展天赋（参与 AI 计算）</summary>
    public static void AttackLogic_extendTalents(BattleSprite sourceSprite, BattleSprite targetSprite, SkillBox skill, BattleField bf, AttackResult result)
    {
        if (sourceSprite.Role.HasTalent("大力士"))
        {
            int bonusDamage = (int)(sourceSprite.Role.AttributesFinal["bili"] * 0.5f);
            result.Hp += bonusDamage;
            result.AddCastInfo(sourceSprite, new string[] { "受死吧，小不点！", "大力士就是我！" });
        }
    }

    /// <summary>扩展天赋2（立即结算，不参与 AI 计算）</summary>
    public static void AttackLogic_extendTalents2(BattleSprite sourceSprite, BattleSprite targetSprite, SkillBox skill, BattleField bf, AttackResult result)
    {
        // 神.化功大法 — 20% 打空内力
        if (sourceSprite.Role.HasTalent("神.化功大法") && Tools.ProbabilityTest(0.2))
        {
            targetSprite.Mp = 0;
            bf.Log(sourceSprite.Role.Name + "天赋【神.化功大法】发动！直接打空" + targetSprite.Role.Name + "的内力!");
            sourceSprite.Say("见识见识让人闻风丧胆的化功大法吧！");
        }
        // 诅咒 — 100% 叠加诅咒
        if (sourceSprite.Role.HasTalent("诅咒"))
        {
            targetSprite.Addbuff("诅咒", 5, 5);
        }
    }

    /// <summary>扩展天赋3（参与攻击公式计算）</summary>
    public static void AttackLogic_extendTalents3(BattleSprite sourceSprite, BattleSprite targetSprite, SkillBox skill, BattleField bf, AttackResult result, AttackFormula formula)
    {
        if (sourceSprite.Role.HasTalent("不稳定的野球拳") && skill.Name == "野球拳")
        {
            formula.attackLow = (int)(formula.attackLow * 0.5f);
            if (formula.attackLow < 0) formula.attackLow = 0;
            formula.attackUp = (int)(formula.attackUp * 1.5f);
        }
    }

    // =====================================================
    // 引擎扩展 (原 GameEngine.lua)
    // =====================================================

    /// <summary>控制台指令</summary>
    public static bool GameEngine_extendConsole(GameEngine gameEngine, string type, string value)
    {
        switch (type)
        {
            case "test":
                Debug.Log(value);
                return true;
            case "get_money":
                RuntimeData.Instance.Money += int.Parse(value);
                AudioManager.Instance.PlayEffect("音效.升级");
                RuntimeData.Instance.mapUI.RefreshRoleState();
                return true;
            case "play_music":
                AudioManager.Instance.Play(value);
                return true;
        }
        return false;
    }

    /// <summary>扩展剧情指令</summary>
    public static bool GameEngine_extendStoryAction(MapUI mapUI, StoryAction action, string[] paras, CommonSettings.VoidCallBack callback)
    {
        if (action.type == "TEST_PRINT")
        {
            Debug.Log(action.value);
            mapUI.ExecuteNextStoryAction(callback);
            return true;
        }
        return false;
    }

    /// <summary>江湖历练面板内容</summary>
    public static string GameEngine_jianghuContent()
    {
        string result = "";
        result += "道德：<color='red'>" + RuntimeData.Instance.Daode + "</color>\n";
        result += "女主角好感：<color='red'>" + RuntimeData.Instance.getHaogan() + "</color>\n";
        result += "本周目生命、内力上限：<color='red'>" + CommonSettings.MAX_HPMP + "</color>\n";
        result += "松鼠旅馆箱子存储量：<color='red'>" + RuntimeData.Instance.XiangziCount + "/" + RuntimeData.Instance.MaxXiangziItemCount + "</color>\n";
        result += "\n亲爱的大侠，您在金X的世界中一共：\n";
        result += "  死亡：<color='red'>" + ModData.GetParam(ModData.DEAD_KEY) + "次</color>\n";
        result += "  存档：<color='red'>" + ModData.GetParam(ModData.SAVE_KEY) + "次</color>\n";
        result += "  击杀：<color='red'>" + ModData.GetParam(ModData.TOTALKILL_KEY) + "个</color>\n";
        result += "  通关次数：<color='red'>" + ModData.GetParam(ModData.END_COUNT_KEY) + "</color>\n";
        result += "  最高周目：<color='red'>" + Math.Max(RuntimeData.Instance.Round, ModData.GetParam(ModData.MAX_ROUND_KEY)) + "</color>\n";
        return result;
    }

    // =====================================================
    // Buff 扩展 (原 buff.lua)
    // =====================================================

    /// <summary>自定义 Buff 回合效果</summary>
    public static void BUFF_OnRoundBuff(BuffInstance buff, RoundBuffResult result)
    {
        if (buff.Name == "诅咒")
        {
            int deschp = (int)Math.Ceiling(buff.Owner.Hp * 0.01 * buff.Level);
            buff.Owner.Hp -= deschp;
            buff.Owner.AttackInfo("诅咒 -" + deschp, Color.red);
            buff.Owner.Refresh();
        }
    }

    // =====================================================
    // 触发器扩展 (原 triggerlogic.lua)
    // =====================================================

    /// <summary>判断主角名字</summary>
    public static bool TriggerLogic_judge(Condition condition)
    {
        if (condition.type == "is_zhujue_name")
        {
            return RuntimeData.Instance.Team[0].Name == condition.value;
        }
        return false;
    }
}
