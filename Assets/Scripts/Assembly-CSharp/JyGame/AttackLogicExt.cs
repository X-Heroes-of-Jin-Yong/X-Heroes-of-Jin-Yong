namespace JyGame
{
    /// <summary>
    /// 攻击逻辑扩展 — 支持 Mod DLL 覆写
    /// </summary>
    public static class AttackLogicExt
    {
        public static AttackResult ExtendSpecialSkill(AttackResult result, SkillBox skill, BattleSprite sourceSprite, BattleSprite targetSprite, BattleField bf)
        {
            if (ModScriptManager.TryCall<AttackResult>("AttackLogic_extendSpecialSkill", out var modResult, result, skill, sourceSprite, targetSprite, bf))
                return modResult;

            // 内置默认：技能「扳手腕」
            if (skill.Name == "扳手腕")
            {
                result.AddCastInfo(sourceSprite, new string[] { "喝啊！", "比比谁的力气大！！" });
                int deltaBili = System.Math.Abs(sourceSprite.Role.AttributesFinal["bili"] - targetSprite.Role.AttributesFinal["bili"]);
                result.Hp = 100 + Tools.GetRandomInt(0, 50 * deltaBili);
            }
            return result;
        }

        public static void ExtendTalents(BattleSprite sourceSprite, BattleSprite targetSprite, SkillBox skill, BattleField bf, AttackResult result)
        {
            if (ModScriptManager.TryCallVoid("AttackLogic_extendTalents", sourceSprite, targetSprite, skill, bf, result))
                return;

            if (sourceSprite.Role.HasTalent("大力士"))
            {
                int bonusDamage = (int)(sourceSprite.Role.AttributesFinal["bili"] * 0.5f);
                result.Hp += bonusDamage;
                result.AddCastInfo(sourceSprite, new string[] { "受死吧，小不点！", "大力士就是我！" });
            }
        }

        public static void ExtendTalents2(BattleSprite sourceSprite, BattleSprite targetSprite, SkillBox skill, BattleField bf, AttackResult result)
        {
            if (ModScriptManager.TryCallVoid("AttackLogic_extendTalents2", sourceSprite, targetSprite, skill, bf, result))
                return;

            if (sourceSprite.Role.HasTalent("神.化功大法") && Tools.ProbabilityTest(0.2))
            {
                targetSprite.Mp = 0;
                bf.Log(sourceSprite.Role.Name + "天赋【神.化功大法】发动！直接打空" + targetSprite.Role.Name + "的内力!");
                sourceSprite.Say("见识见识让人闻风丧胆的化功大法吧！");
            }

            if (sourceSprite.Role.HasTalent("诅咒"))
            {
                targetSprite.Addbuff("诅咒", 5, 5);
            }
        }

        public static void ExtendTalents3(BattleSprite sourceSprite, BattleSprite targetSprite, SkillBox skill, BattleField bf, AttackResult result, AttackFormula formula)
        {
            if (ModScriptManager.TryCallVoid("AttackLogic_extendTalents3", sourceSprite, targetSprite, skill, bf, result, formula))
                return;

            if (sourceSprite.Role.HasTalent("不稳定的野球拳") && skill.Name == "野球拳")
            {
                formula.attackLow = (int)(formula.attackLow * 0.5f);
                if (formula.attackLow < 0) formula.attackLow = 0;
                formula.attackUp = (int)(formula.attackUp * 1.5f);
            }
        }
    }
}
