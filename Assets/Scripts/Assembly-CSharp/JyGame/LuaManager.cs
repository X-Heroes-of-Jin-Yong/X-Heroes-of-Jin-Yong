namespace JyGame
{
    /// <summary>
    /// LuaManager — 兼容层，委托给 ModScriptManager
    /// </summary>
    public static class LuaManager
    {
        public static void Init(bool forceReset = false)
        {
            if (forceReset && GlobalData.CurrentMod != null)
            {
                ModScriptManager.LoadMod(GlobalData.CurrentMod.key);
            }
        }

        public static void Reload()
        {
            if (GlobalData.CurrentMod != null)
            {
                ModScriptManager.LoadMod(GlobalData.CurrentMod.key);
            }
        }
    }
}
