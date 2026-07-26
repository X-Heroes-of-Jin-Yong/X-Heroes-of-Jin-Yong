using System;
using Model.Base;
using Newtonsoft.Json;

namespace Model
{
    /// <summary>
    /// 角色成长模板 — 纯数据模型，只负责 JSON 序列化。
    /// 编辑器显示由 GrowTemplateWrapper 处理。
    /// </summary>
    [Serializable]
    public class RoleGrowTemplate : DataModelBase
    {
        [JsonProperty("name")]  public string Name { get; set; }
        [JsonProperty("hp")]    public int Hp { get; set; }
        [JsonProperty("mp")]    public int Mp { get; set; }
        [JsonProperty("wuxue")] public int Wuxue { get; set; }
        [JsonProperty("wuxing")]public int Wuxing { get; set; }
        [JsonProperty("shenfa")]public int Shenfa { get; set; }
        [JsonProperty("bili")]  public int Bili { get; set; }
        [JsonProperty("gengu")] public int Gengu { get; set; }
        [JsonProperty("fuyuan")]public int Fuyuan { get; set; }
        [JsonProperty("dingli")]public int Dingli { get; set; }
        [JsonProperty("quanzhang")] public int Quanzhang { get; set; }
        [JsonProperty("jianfa")]   public int Jianfa { get; set; }
        [JsonProperty("daofa")]    public int Daofa { get; set; }
        [JsonProperty("qimen")]    public int Qimen { get; set; }

        [JsonIgnore]
        public override string Id => Name;
    }
}
