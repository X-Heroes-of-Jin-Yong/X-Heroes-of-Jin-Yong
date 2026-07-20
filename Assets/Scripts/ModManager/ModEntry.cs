using System;
using UnityEngine;

/// <summary>
/// Mod DLL 入口类模板
///
/// 将此类编译为 DLL，放入 gamedata/modcache/{modKey}/Mod.dll
/// 游戏加载 Mod 时会自动查找 ModEntry 类型并通过反射调用其静态方法。
///
/// 每个扩展方法对应原来的 Lua 全局函数。
/// 只需实现你需要的覆写方法，其他使用内置默认逻辑。
/// 方法签名必须与内置 Ext 类中的方法匹配。
///
/// 扩展点列表（按分类）：
///
/// === 战斗回调 ===
/// BATTLE_BeforeRoleAction(BattleField, BattleSprite)
/// BATTLE_AfterSkillAnimation(BattleField, BattleSprite, SkillBox, int hitNumber)
/// BATTLE_BeforeSkillAnimation(BattleField, BattleSprite, SkillBox, int hitNumber)
/// BATTLE_Die(BattleField, BattleSprite, AttackResult) → bool
/// BATTLE_Rest(BattleField, BattleSprite, int hpRecover, int mpRecover) → string "hp,mp"
///
/// === 攻击扩展 ===
/// AttackLogic_extendSpecialSkill(AttackResult, SkillInstance, BattleSprite, BattleSprite, BattleField) → AttackResult
/// AttackLogic_extendTalents(BattleSprite, BattleSprite, SkillInstance, BattleField, AttackResult)
/// AttackLogic_extendTalents2(BattleSprite, BattleSprite, SkillInstance, BattleField, AttackResult)
/// AttackLogic_extendTalents3(BattleSprite, BattleSprite, SkillInstance, BattleField, AttackResult, AttackFormula)
///
/// === 引擎扩展 ===
/// GameEngine_extendConsole(GameEngine, string type, string value) → bool
/// GameEngine_extendStoryAction(MapUI, StoryAction, string[], Action) → bool
/// GameEngine_jianghuContent() → string
///
/// === Buff/Item 扩展 ===
/// BUFF_OnRoundBuff(BuffInstance, RoundBuffResult)
///
/// 编译命令 (需要引用游戏程序集):
///   mcs -target:library -out:Mod.dll ModEntry.cs -r:Assembly-CSharp.dll -r:UnityEngine.dll
/// </summary>
public static class ModEntry
{
    // ===== 示例：覆写战斗开始前的回调 =====
    // 取消注释并修改即可生效

    // public static void BATTLE_BeforeRoleAction(JyGame.BattleField battlefield, JyGame.BattleSprite currentSprite)
    // {
    //     // 你的自定义逻辑
    //     Debug.Log("Mod: BeforeRoleAction for " + currentSprite.Role.Name);
    // }

    // ===== 示例：添加新的控制台指令 =====

    // public static bool GameEngine_extendConsole(JyGame.GameEngine gameEngine, string type, string value)
    // {
    //     if (type == "my_custom_cmd")
    //     {
    //         Debug.Log("Mod custom command: " + value);
    //         return true;
    //     }
    //     return false; // 返回 false 让内置逻辑处理
    // }

    // ===== 示例：添加新的天赋效果 =====

    // public static void AttackLogic_extendTalents(JyGame.BattleSprite source, JyGame.BattleSprite target,
    //     JyGame.SkillInstance skill, JyGame.BattleField bf, JyGame.AttackResult result)
    // {
    //     // 先调用内置逻辑
    //     // （如果需要完全覆写，不调用内置即可）
    //
    //     if (source.Role.HasTalent("我的自定义天赋"))
    //     {
    //         result.Hp += 100;
    //     }
    // }
}
