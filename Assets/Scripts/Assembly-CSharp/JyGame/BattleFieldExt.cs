using System;
using UnityEngine;

namespace JyGame
{
    /// <summary>
    /// 战斗扩展逻辑 — 支持 Mod DLL 覆写
    /// 每个方法先检查 Mod 是否提供了覆写，否则使用默认实现
    /// Mod DLL 中的 ModEntry 类需提供同名静态方法
    /// </summary>
    public static class BattleFieldExt
    {
        public static void BeforeRoleAction(BattleField battlefield, BattleSprite currentSprite)
        {
            if (ModScriptManager.TryCallVoid("BATTLE_BeforeRoleAction", battlefield, currentSprite))
                return;

            // 内置默认：天赋「战神」，30% 概率自动叠加攻击强化
            if (currentSprite.Role.HasTalent("战神") && Tools.ProbabilityTest(0.3))
            {
                currentSprite.Addbuff("攻击强化", 5, 5);
            }
        }

        public static void AfterSkillAnimation(BattleField battlefield, BattleSprite currentSprite, SkillBox skill, int hitNumber)
        {
            if (ModScriptManager.TryCallVoid("BATTLE_AfterSkillAnimation", battlefield, currentSprite, skill, hitNumber))
                return;

            // 内置默认：天赋「赚便宜」，命中 4+ 人恢复最大生命 10%
            if (currentSprite.Role.HasTalent("赚便宜") && hitNumber >= 4)
            {
                int hpAdd = (int)Math.Ceiling(currentSprite.MaxHp * 0.1);
                currentSprite.Hp += hpAdd;
                currentSprite.AttackInfo("+" + hpAdd, Color.green);
                currentSprite.RandomSay(new string[] { "哈哈，这便宜赚得爽!", "有便宜就要赚！" });
            }
        }

        public static void BeforeSkillAnimation(BattleField battlefield, BattleSprite currentSprite, SkillBox skill, int hitNumber)
        {
            if (ModScriptManager.TryCallVoid("BATTLE_BeforeSkillAnimation", battlefield, currentSprite, skill, hitNumber))
                return;

            // 内置默认：天赋「空挥狂魔」，未命中时自动攻击最近敌人
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
                {
                    battlefield.Attack(currentSprite, skill, findSprite.X, findSprite.Y);
                }
            }
        }

        /// <returns>true = 不死（取消死亡）</returns>
        public static bool Die(BattleField battlefield, BattleSprite sprite, AttackResult attackResult)
        {
            if (ModScriptManager.TryCall<bool>("BATTLE_Die", out bool modResult, battlefield, sprite, attackResult))
                return modResult;

            // 内置默认：天赋「不死」，80% 概率剩 1 血
            if (sprite.Role.HasTalent("不死") && Tools.ProbabilityTest(0.8))
            {
                sprite.Say("就是死不了，你打我啊");
                sprite.Hp = 1;
                return true;
            }
            return false;
        }

        /// <returns>"hp,mp" 格式字符串</returns>
        public static string Rest(BattleField battlefield, BattleSprite sprite, int hpRecover, int mpRecover)
        {
            if (ModScriptManager.TryCall<string>("BATTLE_Rest", out string modResult, battlefield, sprite, hpRecover, mpRecover))
                return modResult;

            // 内置默认：天赋「懒虫」，额外 +100 HP 恢复，10% 晕眩
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
    }
}
