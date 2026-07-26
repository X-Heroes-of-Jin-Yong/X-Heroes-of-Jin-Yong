using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Model;
using Model.Base;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class DataEditorWindow : OdinMenuEditorWindow
    {
        [MenuItem("Big Events/Mod编辑器")]
        private static void Open()
        {
            var window = GetWindow<DataEditorWindow>();
            window.titleContent = new GUIContent("Mod编辑器");
            window.minSize = new Vector2(900, 600);
            window.Show();
        }

        protected override OdinMenuTree BuildMenuTree()
        {
            var tree = new OdinMenuTree();

            foreach (var mod in DataManager.ListMods())
            {
                var modDir = Path.Combine(DataManager.ModsRoot, mod, "设定");

                // 成长模板
                var gtPath = Path.Combine(modDir, "成长模板.json");
                if (File.Exists(gtPath))
                    tree.Add($"{mod}/设定/成长模板", new GrowTemplateHandle(gtPath));

                // 技能等级
                var slPath = Path.Combine(modDir, "技能等级.json");
                if (File.Exists(slPath))
                    tree.Add($"{mod}/设定/技能等级", new SkillLevelInfoHandle(slPath));
            }

            return tree;
        }
    }

    // ============================================================
    // Wrapper — 把 RoleGrowTemplate 的属性展示为 TableList 中文列
    // ============================================================
    public class GrowTemplateWrapper
    {
        private RoleGrowTemplate _data;
        private int _index;
        public RoleGrowTemplate Data => _data;

        public static List<GrowTemplateWrapper> FromList(List<RoleGrowTemplate> data)
            => data.Select((d, i) => new GrowTemplateWrapper(d) { _index = i }).ToList();

        private GrowTemplateWrapper(RoleGrowTemplate d) => _data = d;

        [ShowInInspector, ReadOnly, TableColumnWidth(40)]
        public int 序号 => _index + 1;

        [ShowInInspector, TableColumnWidth(80)] public string 名称 { get => _data.Name;     set => _data.Name     = value; }
        [ShowInInspector, TableColumnWidth(60)] public int    生命 { get => _data.Hp;       set => _data.Hp       = value; }
        [ShowInInspector, TableColumnWidth(60)] public int    内力 { get => _data.Mp;       set => _data.Mp       = value; }
        [ShowInInspector, TableColumnWidth(60)] public int    武学 { get => _data.Wuxue;    set => _data.Wuxue    = value; }
        [ShowInInspector, TableColumnWidth(60)] public int    悟性 { get => _data.Wuxing;   set => _data.Wuxing   = value; }
        [ShowInInspector, TableColumnWidth(60)] public int    身法 { get => _data.Shenfa;   set => _data.Shenfa   = value; }
        [ShowInInspector, TableColumnWidth(60)] public int    臂力 { get => _data.Bili;     set => _data.Bili     = value; }
        [ShowInInspector, TableColumnWidth(60)] public int    根骨 { get => _data.Gengu;    set => _data.Gengu    = value; }
        [ShowInInspector, TableColumnWidth(60)] public int    福缘 { get => _data.Fuyuan;   set => _data.Fuyuan   = value; }
        [ShowInInspector, TableColumnWidth(60)] public int    定力 { get => _data.Dingli;   set => _data.Dingli   = value; }
        [ShowInInspector, TableColumnWidth(60)] public int    拳掌 { get => _data.Quanzhang;set => _data.Quanzhang= value; }
        [ShowInInspector, TableColumnWidth(60)] public int    剑法 { get => _data.Jianfa;   set => _data.Jianfa   = value; }
        [ShowInInspector, TableColumnWidth(60)] public int    刀法 { get => _data.Daofa;    set => _data.Daofa    = value; }
        [ShowInInspector, TableColumnWidth(60)] public int    奇门 { get => _data.Qimen;    set => _data.Qimen    = value; }
    }

    // ============================================================
    // Handle — 具体类型，零反射，零泛型
    // ============================================================
    [Serializable, HideMonoScript]
    public class GrowTemplateHandle
    {
        private readonly string _jsonPath;

        [ShowInInspector, ReadOnly, HorizontalGroup("Top"), PropertyOrder(-2), LabelText("路径")]
        private string PathLabel => _jsonPath;

        [PropertyOrder(-1), HorizontalGroup("Top")]
        [Button("重新加载"), GUIColor(0.3f, 0.8f, 0.3f)]
        private void Reload()
        {
            _wrappers = GrowTemplateWrapper.FromList(
                DataManager.Load<RoleGrowTemplate>(_jsonPath));
        }

        [PropertyOrder(-1), HorizontalGroup("Top")]
        [Button("保存"), GUIColor(0.2f, 0.6f, 1f)]
        [EnableIf("@_wrappers != null && _wrappers.Count > 0")]
        private void Save()
        {
            DataManager.Save(_wrappers.Select(w => w.Data).ToList(), _jsonPath);
        }

        [PropertyOrder(0)]
        [ShowInInspector, TableList(AlwaysExpanded = true, NumberOfItemsPerPage = 10)]
        private List<GrowTemplateWrapper> _wrappers;

        public GrowTemplateHandle(string jsonPath)
        {
            _jsonPath = jsonPath;
            _wrappers = GrowTemplateWrapper.FromList(
                DataManager.Load<RoleGrowTemplate>(jsonPath));
        }
    }

    // ============================================================
    // SkillLevelInfo
    // ============================================================
    public class SkillLevelInfoWrapper
    {
        private SkillLevelInfo _data;
        private int _index;
        public SkillLevelInfo Data => _data;

        public static List<SkillLevelInfoWrapper> FromList(List<SkillLevelInfo> data)
            => data.Select((d, i) => new SkillLevelInfoWrapper(d) { _index = i }).ToList();

        private SkillLevelInfoWrapper(SkillLevelInfo d) => _data = d;

        [ShowInInspector, ReadOnly, TableColumnWidth(40)]
        public int 序号 => _index + 1;

        [ShowInInspector, TableColumnWidth(60)]  public int    等级 { get => _data.Level;    set => _data.Level    = value; }
        [ShowInInspector, TableColumnWidth(60)]  public float  威力 { get => _data.Power;    set => _data.Power    = value; }
        [ShowInInspector, TableColumnWidth(70)]  public int    覆盖类型 { get => _data.CoverType;set => _data.CoverType= value; }
        [ShowInInspector, TableColumnWidth(70)]  public int    覆盖范围 { get => _data.CoverSize;set => _data.CoverSize= value; }
        [ShowInInspector, TableColumnWidth(70)]  public int    施放距离 { get => _data.CastSize; set => _data.CastSize = value; }
        [ShowInInspector, TableColumnWidth(80)]  public string 动画 { get => _data.Animation;set => _data.Animation= value; }
        [ShowInInspector, TableColumnWidth(60)]  public int    冷却 { get => _data.Cd;       set => _data.Cd       = value; }
    }

    [Serializable, HideMonoScript]
    public class SkillLevelInfoHandle
    {
        private readonly string _jsonPath;

        [ShowInInspector, ReadOnly, HorizontalGroup("Top"), PropertyOrder(-2), LabelText("路径")]
        private string PathLabel => _jsonPath;

        [PropertyOrder(-1), HorizontalGroup("Top")]
        [Button("重新加载"), GUIColor(0.3f, 0.8f, 0.3f)]
        private void Reload()
        {
            _wrappers = SkillLevelInfoWrapper.FromList(
                DataManager.Load<SkillLevelInfo>(_jsonPath));
        }

        [PropertyOrder(-1), HorizontalGroup("Top")]
        [Button("保存"), GUIColor(0.2f, 0.6f, 1f)]
        [EnableIf("@_wrappers != null && _wrappers.Count > 0")]
        private void Save()
        {
            DataManager.Save(_wrappers.Select(w => w.Data).ToList(), _jsonPath);
        }

        [PropertyOrder(0)]
        [ShowInInspector, TableList(AlwaysExpanded = true, NumberOfItemsPerPage = 10)]
        private List<SkillLevelInfoWrapper> _wrappers;

        public SkillLevelInfoHandle(string jsonPath)
        {
            _jsonPath = jsonPath;
            _wrappers = SkillLevelInfoWrapper.FromList(
                DataManager.Load<SkillLevelInfo>(jsonPath));
        }
    }
}
