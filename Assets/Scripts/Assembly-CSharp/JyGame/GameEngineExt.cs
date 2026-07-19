using System;
using UnityEngine;

namespace JyGame
{
    /// <summary>
    /// 游戏引擎扩展 — 支持 Mod DLL 覆写
    /// </summary>
    public static class GameEngineExt
    {
        public static bool ExtendConsole(GameEngine gameEngine, string statusType, string value)
        {
            if (ModScriptManager.TryCall<bool>("GameEngine_extendConsole", out bool modResult, gameEngine, statusType, value))
                return modResult;

            switch (statusType)
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

        public static bool ExtendStoryAction(MapUI mapUI, StoryAction action, string[] paras, CommonSettings.VoidCallBack callback)
        {
            if (ModScriptManager.TryCall<bool>("GameEngine_extendStoryAction", out bool modResult, mapUI, action, paras, callback))
                return modResult;

            if (action.type == "TEST_PRINT")
            {
                Debug.Log(action.value);
                mapUI.ExecuteNextStoryAction(callback);
                return true;
            }
            return false;
        }

        public static string GetJianghuContent()
        {
            if (ModScriptManager.TryCall<string>("GameEngine_jianghuContent", out string modResult))
                return modResult;

            string result = string.Empty;
            result += "道德：<color='red'>" + RuntimeData.Instance.Daode + "</color>\n";
            result += "女主角好感：<color='red'>" + RuntimeData.Instance.getHaogan() + "</color>\n";
            result += "本周目生命、内力上限：<color='red'>" + CommonSettings.MAX_HPMP + "</color>\n";
            result += "松鼠旅馆箱子存储量：<color='red'>" + RuntimeData.Instance.XiangziCount + "/" + RuntimeData.Instance.MaxXiangziItemCount + "</color>\n";
            result += "\n";
            result += "亲爱的大侠，您在金X的世界中一共：\n";
            result += "  死亡：<color='red'>" + ModData.GetParam(ModData.DEAD_KEY) + "次</color>\n";
            result += "  存档：<color='red'>" + ModData.GetParam(ModData.SAVE_KEY) + "次</color>\n";
            result += "  击杀：<color='red'>" + ModData.GetParam(ModData.TOTALKILL_KEY) + "个</color>\n";
            result += "  通关次数：<color='red'>" + ModData.GetParam(ModData.END_COUNT_KEY) + "</color>\n";
            result += "  最高周目：<color='red'>" + Math.Max(RuntimeData.Instance.Round, ModData.GetParam(ModData.MAX_ROUND_KEY)) + "</color>\n";

            return result;
        }
    }
}
