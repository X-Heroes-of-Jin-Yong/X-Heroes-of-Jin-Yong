using System;
using Model.Base;
using Newtonsoft.Json;

namespace Model
{
    /// <summary>
    /// 技能等级信息 — 按等级覆盖技能的威力/范围/动画/冷却。
    /// </summary>
    [Serializable]
    public class SkillLevelInfo : DataModelBase
    {
        [JsonProperty("skill")]      public string Skill;
        [JsonProperty("level")]      public int Level;
        [JsonProperty("power")]      public float Power;
        [JsonProperty("covertype")]  public int CoverType;
        [JsonProperty("coversize")]  public int CoverSize;
        [JsonProperty("castsize")]   public int CastSize;
        [JsonProperty("animation")]  public string Animation;
        [JsonProperty("cd")]         public int Cd;

        [JsonIgnore]
        public override string Id => $"{Skill}_Lv{Level}";
    }
}
