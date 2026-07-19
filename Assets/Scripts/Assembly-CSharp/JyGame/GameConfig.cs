namespace JyGame
{
    /// <summary>
    /// 游戏配置常量类
    /// 替代原来从 Lua (ROOT_getConfigList) 读取的配置值
    /// 默认值来自 main.lua 中的 ROOT_getConfigList 函数
    /// </summary>
    public static class GameConfig
    {
        // ===== 游戏基础配置 =====

        /// <summary>主菜单音乐</summary>
        public const string MAINMENU_MUSIC = "音乐.武侠回忆";

        /// <summary>主菜单背景图</summary>
        public const string MAINMENU_BG = "地图.桃花岛";

        /// <summary>开场剧本</summary>
        public const string gamestart_story = "新手村_出生";

        /// <summary>开场地点</summary>
        public const string gamestart_location = "南贤居";

        /// <summary>是否在游戏中开启控制台</summary>
        public const bool CONSOLE = true;

        /// <summary>默认最大战斗时间</summary>
        public const int DEFAULT_MAX_GAME_SPTIME = 3000;

        // ===== 周目成长系数 =====

        /// <summary>每周目敌人增加攻击力比例</summary>
        public const double ZHOUMU_ATTACK_ADD = 0.1;

        /// <summary>每周目敌人增加的防御力比例</summary>
        public const double ZHOUMU_DEFENCE_ADD = 0.08;

        /// <summary>每周目敌人增加血量比例</summary>
        public const double ZHOUMU_HP_ADD = 0.15;

        /// <summary>每周目敌人增加内力比例</summary>
        public const double ZHOUMU_MP_ADD = 0.15;

        /// <summary>多少个周目提高一级武功等级上限</summary>
        public const int PER_MAXLEVEL_ADD_BY_ZHOUMU = 2;

        /// <summary>NPC多少个周目提高所有武功等级1级</summary>
        public const int NPC_SKILL_LEVEL_ADD_BY_ZHOUMU = 2;

        /// <summary>每周目增长血内上限</summary>
        public const int MAX_HPMP_PER_ROUND = 1000;

        // ===== 数值上限 =====

        /// <summary>最大内功数量</summary>
        public const int MAX_INTERNALSKILL_COUNT = 5;

        /// <summary>最大外功数量</summary>
        public const int MAX_SKILL_COUNT = 10;

        /// <summary>最大可分点属性值</summary>
        public const int MAX_ATTRIBUTE = 200;

        /// <summary>最大外功等级</summary>
        public const int MAX_SKILL_LEVEL = 20;

        /// <summary>最大内功等级</summary>
        public const int MAX_INTERNALSKILL_LEVEL = 20;

        /// <summary>最大血内上限</summary>
        public const int MAX_HPMP = 10000;

        /// <summary>人物等级上限</summary>
        public const int MAX_LEVEL = 30;

        /// <summary>同福客栈小游戏最大提升到的属性值</summary>
        public const int SMALLGAME_MAX_ATTRIBUTE = 70;

        // ===== 掉落率 =====

        /// <summary>战斗元宝掉率 (0.5%)</summary>
        public const double YUANBAO_DROP_RATE = 0.005;

        /// <summary>困难残章掉率 (1.5%)</summary>
        public const double HARD_MODE_CANZHANG_DROPRATE = 0.015;

        /// <summary>炼狱以上难度残章基础掉率 (3%)</summary>
        public const double CRAZY_MODE_CANZHANG_DROPRATE = 0.03;

        /// <summary>炼狱以上难度残章基础掉率每周目递增 (0.5%)</summary>
        public const double CRAZY_MODE_CANZHANG_DROPRATE_PER_ROUND = 0.005;

        /// <summary>外功最大掉落残章的武学难度值(不含)</summary>
        public const int CANZHANG_MAX_HARD_SKILL = 8;

        /// <summary>内功最大掉落残章的武学难度值(不含)</summary>
        public const int CANZHANG_MAX_HARD_INTERNALSKILL = 8;

        /// <summary>外功残章掉率是内功的多少倍</summary>
        public const double CANZHANG_DROP_RATE_INTERNAL_RATE = 2.0;

        // ===== 战斗场景配置 =====

        /// <summary>战斗加速倍速</summary>
        public const double BATTLE_SPEEDUP_RATE = 1.5;

        /// <summary>战斗场景X坐标格数</summary>
        public const int BATTLE_MOVEBLOCK_MAX_X = 11;

        /// <summary>战斗场景Y坐标格数</summary>
        public const int BATTLE_MOVEBLOCK_MAX_Y = 4;

        /// <summary>战斗场景格子长度</summary>
        public const int BATTLE_MOVEBLOCK_LENGTH = 80;

        /// <summary>战斗场景格子宽度</summary>
        public const int BATTLE_MOVEBLOCK_WIDTH = 80;

        /// <summary>战斗场景右边距（单位：格子数）</summary>
        public const int BATTLE_MOVEBLOCK_MARGIN_RIGHT = 5;

        /// <summary>战斗场景上边距（单位：格子数）</summary>
        public const int BATTLE_MOVEBLOCK_MARGIN_TOP = 2;

        /// <summary>战斗场景格子大小修正（默认：1.25倍，1.25 * 64 = 80）</summary>
        public const double BATTLE_MOVEBLOCK_SCALE = 1.25;

        // ===== 数组配置 =====

        /// <summary>随机战斗音乐列表</summary>
        public static readonly string[] randomBattleMusics = {
            "战斗音乐.云狐之战", "战斗音乐.暮云出击", "战斗音乐.山谷行进",
            "战斗音乐.山谷行进2", "战斗音乐.2", "战斗音乐.3",
            "战斗音乐.4", "战斗音乐.5",
            "音乐.天龙八部.紧张感3", "音乐.天龙八部.紧张感4",
        };

        /// <summary>困难难度 NPC 随机天赋池</summary>
        public static readonly string[] EnemyRandomTalentsList = {
            "飘然", "斗魂", "哀歌", "奋战", "百足之虫", "真气护体", "暴躁",
            "金钟罩", "诸般封印", "刀封印", "剑封印", "奇门封印", "拳掌封印",
            "自我主义", "大小姐", "破甲", "好色", "瘸子", "白内障", "左手剑",
            "右臂有伤", "拳掌增益", "剑法增益", "刀法增益", "奇门增益", "锐眼"
        };

        /// <summary>炼狱/无悔难度 NPC 随机天赋池1</summary>
        public static readonly string[] EnemyRandomTalentListCrazy1 = {
            "百足之虫", "真气护体", "金钟罩", "苦命儿", "老江湖", "暴躁",
            "灵心慧质", "精打细算", "白内障", "右臂有伤", "神经病", "鲁莽",
        };

        /// <summary>炼狱/无悔难度 NPC 随机天赋池2</summary>
        public static readonly string[] EnemyRandomTalentListCrazy2 = {
            "斗魂", "奋战", "暴躁", "自我主义", "破甲", "铁拳无双",
            "素心神剑", "左右互搏", "博览群书", "阴谋家", "琴胆剑心",
            "追魂", "铁口直断", "左手剑", "拳掌增益", "剑法增益",
            "刀法增益", "奇门增益", "锐眼"
        };

        /// <summary>炼狱/无悔难度 NPC 随机天赋池3</summary>
        public static readonly string[] EnemyRandomTalentListCrazy3 = {
            "刀封印", "剑封印", "奇门封印", "拳掌封印", "清心",
            "哀歌", "幽居", "金刚", "嗜血狂魔", "清风", "御风",
            "轻功高超", "瘸子",
        };

        /// <summary>奥义呐喊（女声）</summary>
        public static readonly string[] AOYI_SOUND_FEMALE = {
            "音效.女", "音效.女2", "音效.女3", "音效.女4"
        };

        /// <summary>奥义呐喊（男声）</summary>
        public static readonly string[] AOYI_SOUND_MALE = {
            "音效.男", "音效.男2", "音效.男3", "音效.男4", "音效.男5", "音效.男-哼"
        };

        /// <summary>奥义特效音效</summary>
        public static readonly string[] AOYI_EFFECT = {
            "音效.内功攻击4", "音效.打雷", "音效.奥义1", "音效.奥义2",
            "音效.奥义3", "音效.奥义4", "音效.奥义5", "音效.奥义6"
        };

        /// <summary>珍珑棋局随机掉落武器</summary>
        public static readonly string[] ZHENLONG_WUQI = {
            "真.天龙宝剑", "玄铁剑", "冷月宝刀", "被诅咒的木刀",
            "真.倚天剑", "真.屠龙刀", "真.打狗棒", "真.灭仙爪", "打狗棒"
        };

        /// <summary>珍珑棋局随机掉落防具</summary>
        public static readonly string[] ZHENLONG_FANGJU = {
            "黄金重甲", "幽梦衣", "霓裳羽衣", "岳飞的重铠",
            "千变魔女的披风", "华裳", "乌蚕衣", "三清袍"
        };

        /// <summary>珍珑棋局随机掉落饰品</summary>
        public static readonly string[] ZHENLONG_SHIPIN = {
            "橙色灯戒", "铂金戒指", "蓝宝戒指", "水晶护符", "魔神信物", "神奇戒指"
        };

        /// <summary>Buff 列表</summary>
        public static readonly string[] BUFF_LIST = {
            "恢复", "集气", "攻击强化", "飘渺", "左右互搏", "神速攻击",
            "醉酒", "溜须拍马", "易容", "狂战", "坚守", "沾衣十八跌",
            "圣战", "轻身", "防御强化", "魔神降临", "神行"
        };

        /// <summary>Debuff 列表</summary>
        public static readonly string[] DEBUFF_LIST = {
            "中毒", "内伤", "致盲", "缓速", "晕眩", "攻击弱化",
            "诸般封印", "剑封印", "刀封印", "拳掌封印", "奇门封印",
            "伤害加深", "重伤", "定身", "封穴", "点穴", "诅咒"
        };

        // ===== RollRole 奖励队友 =====

        /// <summary>1周目奖励队友池</summary>
        public static readonly string[] ROLLROLE_BonusRole_1 = {};

        /// <summary>2周目奖励队友池</summary>
        public static readonly string[] ROLLROLE_BonusRole_2 = {
            "鲁连荣", "冲虚道长", "方证大师", "灭绝师太", "张翠山", "宋远桥",
            "韦一笑", "仪清", "何太冲", "哑仆", "温方达", "温方义", "温方山",
            "温方施", "温方悟", "安小慧", "阿九"
        };

        /// <summary>3周目奖励队友池</summary>
        public static readonly string[] ROLLROLE_BonusRole_3 = {
            "紫衫龙王", "白眉鹰王", "商剑鸣", "杨逍", "范遥", "霍都",
            "孙不二", "龙岛主", "木岛主", "善勇"
        };

        /// <summary>4周目奖励队友池</summary>
        public static readonly string[] ROLLROLE_BonusRole_4 = {
            "白自在", "向问天", "丁春秋", "成昆", "段延庆", "丘处机", "欧阳锋"
        };

        /// <summary>5+周目奖励队友池</summary>
        public static readonly string[] ROLLROLE_BonusRole_5 = {
            "任我行", "王重阳", "林朝英", "归辛树", "玉真子", "慕容博",
            "卓一航", "谢逊", "虚竹"
        };

        /// <summary>根据周目数获取奖励队友列表</summary>
        public static string[] GetBonusRoles(int round)
        {
            switch (round)
            {
                case 1: return ROLLROLE_BonusRole_1;
                case 2: return ROLLROLE_BonusRole_2;
                case 3: return ROLLROLE_BonusRole_3;
                case 4: return ROLLROLE_BonusRole_4;
                default: return ROLLROLE_BonusRole_5;
            }
        }
    }
}
