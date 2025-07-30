--[[
金庸群侠传X 初始角色ROLL点逻辑

]]--

local AudioManager = luanet.import_type('JyGame.AudioManager')
local LuaTool = luanet.import_type('JyGame.LuaTool')
local Debug = luanet.import_type('UnityEngine.Debug')
local Item = luanet.import_type('JyGame.Item')
local CommonSettings = luanet.import_type('JyGame.CommonSettings')
local RuntimeData = luanet.import_type('JyGame.RuntimeData')
local Role = luanet.import_type('JyGame.Role')

--开场问题
local ROLLROLE_QUESTIONS = {
	[0]={title="在来到这个世界之前，请允许询问您几个问题", opts={"继续.."}},
	[1]={title="你希望你在武侠小说中的出身是", opts={"商人的儿子", "大草原上长大的孩子", "名门世家", "市井流浪的汉子", "疯子", "书香门第"}},
	[2]={title="以下你觉得最宝贵的是", opts={"无尽的财宝", "永恒的爱情", "坚强的意志", "自由", "至高无上的权力"}},
	[3]={title="以下你觉得最可恶的行为是", opts={"调戏妇女", "欺软怕硬", "偷奸耍滑", "切JJ练神功", "有美女不泡"}},
	[4]={title="你最喜欢的兵刃是", opts={"拳", "剑", "刀", "暗器"}},
	[5]={title="以下女性角色你最喜欢的是", opts={"黄蓉", "小龙女", "郭襄", "梅超风", "周芷若"}},
	[6]={title="以下男性角色你最喜欢的是", opts={"张无忌", "郭靖", "杨过", "令狐冲", "林平之"}},
	[7]={title="以下你觉得最牛的人是", opts={"乔峰", "韦小宝", "金庸先生", "东方不败", "汉家松鼠", "半瓶神仙醋"}},
	[8]={title="选择你的游戏难度", opts={}},
	[9]={title="请输入你的名字", opts={"继续.."}},
}

--可选头像
local ROLLROLE_HeadList = {
	"头像.主角","头像.主角3","头像.主角4","头像.魔君","头像.全冠清","头像.李白","头像.林平之瞎","头像.侠客2",
	"头像.归辛树","头像.狄云","头像.独孤求败","头像.陈近南","头像.石中玉",
	"头像.商宝震","头像.尹志平","头像.流浪汉","头像.梁发", "头像.卓一航", "头像.烟霞神龙",
	"头像.双手开碑", "头像.流星赶月", "头像.盖七省", "头像.公子1", "头像.主角2",
}

--奖励的队友
local ROLLROLE_BonusRole = {
	[0]={},
	[1]={"鲁连荣","冲虚道长","方证大师","灭绝师太","张翠山","宋远桥","韦一笑",
	"仪清","何太冲","哑仆","温方达","温方义","温方山","温方施","温方悟","安小慧","阿九"},
	[2]={"紫衫龙王","白眉鹰王","商剑鸣","杨逍","范遥","霍都","孙不二","龙岛主","木岛主","善勇"},
	[3]={"白自在","向问天","丁春秋","成昆","段延庆","丘处机","欧阳锋"},
	[4]={"任我行","王重阳","林朝英","归辛树","玉真子","慕容博","卓一航","谢逊","虚竹"}
}

--入口函数
function ROLLROLE_start(this)
	--开始游戏播放音乐
	AudioManager.Instance:Play("音乐.七秀")
	--清楚历史结果
	this.results:Clear()
	
	--初始化难度选择
	
	--以前是无悔，并且大于一周目，这里只有一个选项
	ROLLROLE_QUESTIONS[8].opts = {}
	
	if(RuntimeData.Instance.AutoSaveOnly and RuntimeData.Instance.Round > 1) then
		table.insert(ROLLROLE_QUESTIONS[8].opts, "<color='magenta'>无悔(变态+实时存档)</color>")
	else
		if(RuntimeData.Instance.Round == 1) then
			table.insert(ROLLROLE_QUESTIONS[8].opts, "简单（新手推荐，仅一周目可选）")
		end
		table.insert(ROLLROLE_QUESTIONS[8].opts, "<color='yellow'>进阶（难度较高）</color>")
		table.insert(ROLLROLE_QUESTIONS[8].opts, "<color='red'>炼狱（极度变态狂选这个..请慎重)</color>")
		
		if(RuntimeData.Instance.Round == 1) then
			table.insert(ROLLROLE_QUESTIONS[8].opts, "<color='magenta'>无悔(变态+实时存档，仅周目一可选)</color>")
		end
	end
	
	--开始选项
	ROLLROLE_LoadSelection(this, 0)
end

function ROLLROLE_getBonusRole(index)
	return LuaTool.MakeStringArray(ROLLROLE_BonusRole[index])
end

function ROLLROLE_LoadSelection(this, index)

	local question = ROLLROLE_QUESTIONS[index]
	if(question == nil) then
		ROLLROLE_InputName(this)
	else
		this:LoadSelection(question.title , question.opts, function(selectIndex) 
			this.results:Add(selectIndex)
			ROLLROLE_LoadSelection(this, index + 1)
		end)
	end
end


function ROLLROLE_InputName(this)
	this.nameInputPanel:Show("小虾米", LuaTool.MakeStringCallBack( function(name)
		RuntimeData.Instance.maleName = name
		this.headSelectPanelObj:SetActive(true)
		this.headSelectMenu:Show(LuaTool.MakeStringArray(ROLLROLE_HeadList), LuaTool.MakeStringCallBack( function(selectKey)
			this.selectHeadKey = selectKey
			this.headSelectPanelObj:SetActive(false)
			this:LoadSelection("欢迎来到金庸群侠传的世界", {"继续.."}, function(selectIndex)	
				ROLLROLE_Reset(this)
			end)
		end))
	end))
end

function ROLLROLE_Reset(this)
	
	--根据答案生成初始角色和物品
	ROLLROLE_MakeBeginningCondition(this)

	--随机调整
	ROLLROLE_MakeRandomCondition(this)
	
	--显示按钮
	this.RoleConfirmButtonObj:SetActive(true);
    this.RoleResetButtonObj:SetActive(true);
    this.rolePanelObj.transform:FindChild("CancelButton").gameObject:SetActive(false);
	
	--显示
	this.rolePanel:Show(this.makeRole)
end

function ROLLROLE_MakeBeginningCondition(this)

	local makeItems = this:GenerateEmptyItems()
	local makeRole = Role.Generate("主角")
	local makeMoney = 100
	
	makeRole.Head = this.selectHeadKey
	makeRole.Name = RuntimeData.Instance.maleName
	makeItems:Clear()
	makeItems:Add(Item.GetItem("小还丹"))
    makeItems:Add(Item.GetItem("小还丹"))
    makeItems:Add(Item.GetItem("小还丹"))
	
	local results = this.results

	--你希望你在武侠小说中的出身是
	--商人的儿子
	if(results[1] == 0) then
		makeMoney = makeMoney + 5000;
		CommonSettings.adjustAttr(makeRole, "bili", -5);
		makeItems:Add(Item.GetItem("黑玉断续膏"))
		makeItems:Add(Item.GetItem("九转熊蛇丸"))
		makeItems:Add(Item.GetItem("金丝道袍"))
		makeItems:Add(Item.GetItem("金头箍"))
		makeRole.Animation = "zydx"

	--大草原上长大的孩子
	elseif(results[1] == 1) then 
		CommonSettings.adjustAttr(makeRole, "shenfa", 15);
		CommonSettings.adjustAttr(makeRole, "dingli", -2);
		CommonSettings.adjustAttr(makeRole, "quanzhang", 15);
		makeRole.TalentValue = makeRole.TalentValue .. "#猎人"
        makeRole.Animation = "caoyuan"

    --名门世家
	elseif(results[1] == 2) then 
		CommonSettings.adjustAttr(makeRole, "fuyuan", 3);
		CommonSettings.adjustAttr(makeRole, "bili", -3);
		CommonSettings.adjustAttr(makeRole, "dingli", 2);
		CommonSettings.adjustAttr(makeRole, "wuxing", 20);
		CommonSettings.adjustAttr(makeRole, "jianfa", 2);
		CommonSettings.adjustAttr(makeRole, "gengu", 2);
		makeItems:Add(Item.GetItem("银手镯"));
		makeMoney = makeMoney + 100;
		makeRole.Animation = "huodu";

	--市井流浪的汉子
	elseif(results[1] == 3) then 
		CommonSettings.adjustAttr(makeRole, "fuyuan", -5);
		CommonSettings.adjustAttr(makeRole, "bili", 12);
		CommonSettings.adjustAttr(makeRole, "daofa", 15);
		CommonSettings.adjustAttr(makeRole, "qimen", 12);
		makeItems:Add(Item.GetItem("草帽"));
		makeRole.Animation = "shijing";
		makeMoney = 0;

	--疯子
	elseif(results[1] == 4) then
		CommonSettings.adjustAttr(makeRole, "wuxing", 35);
		CommonSettings.adjustAttr(makeRole, "dingli", 10);
		CommonSettings.adjustAttr(makeRole, "gengu", 10);
		makeRole.TalentValue = makeRole.TalentValue .. "#神经病";
		makeRole.Animation = "fengzi";

	--书香门第
	elseif(results[1] == 5) then
		CommonSettings.adjustAttr(makeRole, "wuxing", 20);
		CommonSettings.adjustAttr(makeRole, "bili", 1);
		CommonSettings.adjustAttr(makeRole, "shenfa", -10);
		CommonSettings.adjustAttr(makeRole, "gengu", -5);
		makeRole.Animation = "xiake";
	end
	
	--以下你觉得最宝贵的是
	--无尽的财宝
	if(results[2] == 0) then
		makeMoney = makeMoney + 1000

	--永恒的爱情
	elseif(results[2] == 1) then
		CommonSettings.adjustAttr(makeRole, "fuyuan", 15);

	--坚强的意志
	elseif(results[2] == 2) then
		CommonSettings.adjustAttr(makeRole, "dingli", 15);

	--自由
	elseif(results[2] == 3) then
		CommonSettings.adjustAttr(makeRole, "shenfa", 15);

	--至高无上的权力
	elseif(results[2] == 4) then
		makeRole.TalentValue = makeRole.TalentValue .. "#自我主义";
	end
	
	--以下你觉得最可恶的行为是
	--调戏妇女
	if(results[3]==0) then
		CommonSettings.adjustAttr(makeRole, "dingli", 9);

	--欺软怕硬
	elseif(results[3]==1) then
		CommonSettings.adjustAttr(makeRole, "gengu", 6);
        CommonSettings.adjustAttr(makeRole, "bili", 6);

    --偷奸耍滑
	elseif(results[3]==2) then
		CommonSettings.adjustAttr(makeRole, "wuxing", 10);

	--切JJ练神功
	elseif(results[3]==3) then
		CommonSettings.adjustAttr(makeRole, "gengu", 10);

	--有美女不泡
	elseif(results[3]==4) then
		makeRole.TalentValue = makeRole.TalentValue .. "#好色";
	end
	
	--你最喜欢的兵刃是
	--拳
	if(results[4]==0) then
		CommonSettings.adjustAttr(makeRole, "quanzhang", 10);

	--剑
	elseif(results[4]==1) then
		CommonSettings.adjustAttr(makeRole, "jianfa", 10);

	--刀
	elseif(results[4]==2) then
		CommonSettings.adjustAttr(makeRole, "daofa", 20);

	--暗器
	elseif(results[4]==3) then
		CommonSettings.adjustAttr(makeRole, "qimen", 20);
	end
	
	--以下女性角色你最喜欢的是
	--黄蓉
	if(results[5]==0) then
		CommonSettings.adjustAttr(makeRole, "wuxing", 5);

	--小龙女
	elseif(results[5]==1) then
		CommonSettings.adjustAttr(makeRole, "dingli", 5);

	--郭襄
	elseif(results[5]==2) then
		CommonSettings.adjustAttr(makeRole, "fuyuan", 5);
        CommonSettings.adjustAttr(makeRole, "gengu", 5);

    --梅超风
	elseif(results[5]==3) then
		CommonSettings.adjustAttr(makeRole, "quanzhang", 6);
        CommonSettings.adjustAttr(makeRole, "bili", 6);

    --周芷若
	elseif(results[5]==4) then
		CommonSettings.adjustAttr(makeRole, "dingli", 10);
	end
	
	--以下男性角色你最喜欢的是
	--张无忌
	if(results[6]==0) then
		CommonSettings.adjustAttr(makeRole, "wuxing", 5);
        CommonSettings.adjustAttr(makeRole, "gengu", 10);

    --郭靖
	elseif(results[6]==1) then
		CommonSettings.adjustAttr(makeRole, "wuxing", -10);
		CommonSettings.adjustAttr(makeRole, "fuyuan", 15);
		CommonSettings.adjustAttr(makeRole, "bili", 5);

	--杨过
	elseif(results[6]==2) then
		CommonSettings.adjustAttr(makeRole, "wuxing", 5);
        CommonSettings.adjustAttr(makeRole, "dingli", 5);

    --令狐冲
	elseif(results[6]==3) then
		CommonSettings.adjustAttr(makeRole, "wuxing", 10);

	--林平之
	elseif(results[6]==4) then
		CommonSettings.adjustAttr(makeRole, "jianfa", 10);
        CommonSettings.adjustAttr(makeRole, "dingli", 10);
	end
	
	--以下你觉得最牛的人是
	--乔峰
	if(results[7]==0) then 
		CommonSettings.adjustAttr(makeRole, "bili", 10);
        CommonSettings.adjustAttr(makeRole, "quanzhang", 9);

    --韦小宝
	elseif(results[7]==1) then
		CommonSettings.adjustAttr(makeRole, "fuyuan", 30);

	--金庸先生
	elseif(results[7]==2) then
		CommonSettings.adjustAttr(makeRole, "wuxing", 13);
		CommonSettings.adjustAttr(makeRole, "jianfa", 5);
		CommonSettings.adjustAttr(makeRole, "daofa", 5);
		CommonSettings.adjustAttr(makeRole, "quanzhang", 5);
		CommonSettings.adjustAttr(makeRole, "qimen", 5);

	--东方不败
	elseif(results[7]==3) then
		CommonSettings.adjustAttr(makeRole, "gengu", 20);

	--汉家松鼠
	elseif(results[7]==4) then
		makeRole.InternalSkills[0]:SetLevel(20); --初始20级基本内功
		CommonSettings.adjustAttr(makeRole, "gengu", 10);
		makeItems:Add(Item.GetItem("松果"));
		makeItems:Add(Item.GetItem("松果"));
		makeItems:Add(Item.GetItem("松果"));

	--半瓶神仙醋
	elseif(results[7]==5) then
		makeItems:Add(Item.GetItem("天王保命丹"));
		makeItems:Add(Item.GetItem("天王保命丹"));
		makeItems:Add(Item.GetItem("天王保命丹"));
		makeItems:Add(Item.GetItem("天王保命丹"));
		makeItems:Add(Item.GetItem("天王保命丹"));
		makeItems:Add(Item.GetItem("天王保命丹"));
	end
	
	--选择你的游戏难度
	if(table.getn(ROLLROLE_QUESTIONS[8].opts)==4) then
		if(results[8]==0) then
			RuntimeData.Instance.GameMode = "normal";
            RuntimeData.Instance.FriendlyFire = false;
			RuntimeData.Instance.AutoSaveOnly = false;
		elseif(results[8]==1) then
			RuntimeData.Instance.GameMode = "hard";
			RuntimeData.Instance.FriendlyFire = true;
			RuntimeData.Instance.AutoSaveOnly = false;
		elseif(results[8]==2) then
			RuntimeData.Instance.GameMode = "crazy";
			RuntimeData.Instance.FriendlyFire = true;
			RuntimeData.Instance.AutoSaveOnly = false;
		elseif(results[8]==3) then
			RuntimeData.Instance.GameMode = "crazy";
			RuntimeData.Instance.FriendlyFire = true;
			RuntimeData.Instance.AutoSaveOnly = true;
		end
	elseif(table.getn(ROLLROLE_QUESTIONS[8].opts)==1) then
		RuntimeData.Instance.GameMode = "crazy";
		RuntimeData.Instance.FriendlyFire = true;
		RuntimeData.Instance.AutoSaveOnly = true;
	elseif(table.getn(ROLLROLE_QUESTIONS[8].opts)==2) then
		if(results[8]==0) then
			RuntimeData.Instance.GameMode = "hard";
			RuntimeData.Instance.FriendlyFire = true;
			RuntimeData.Instance.AutoSaveOnly = false;
		elseif(results[8]==1) then
			RuntimeData.Instance.GameMode = "crazy";
			RuntimeData.Instance.FriendlyFire = true;
			RuntimeData.Instance.AutoSaveOnly = false;
		end
	end
	
	this.makeRole = makeRole
	this.makeMoney = makeMoney
	this.makeItems = makeItems
	
	--生成周目和霹雳堂试炼的奖励，
	--懒得写lua了，有兴趣的话自己看DLL来改吧。
	this:MakeZhoumuAndShilianBonus()
end

function ROLLROLE_MakeRandomCondition(this)

	local randomAttr = {"gengu","bili","fuyuan","shenfa","dingli","wuxing","quanzhang","jianfa","daofa","qimen"}
	local randomAttrLength = table.getn(randomAttr)
	for i=0,2,1
    do
        local rnd = math.random(0, randomAttrLength - 1)
        local attr = randomAttr[rnd]
        ROLLROLE_adjustAttr(this, this.makeRole, attr, 10)
    end
    for i=0,9,1
    do
        local rnd = math.random(0, randomAttrLength - 1)
        local attr = randomAttr[rnd]
        ROLLROLE_adjustAttr(this, this.makeRole, attr, 1)
    end

end

function ROLLROLE_adjustAttr(this, makeRole, type, value)

	if (type == "hp") then
		makeRole.hp = makeRole.hp + value
	elseif (type == "maxhp") then
    	makeRole.maxhp = makeRole.maxhp + value
    elseif (type == "mp") then
    	makeRole.mp = makeRole.mp + value
    elseif (type == "maxmp") then	
    	makeRole.maxmp = makeRole.maxmp + value
    elseif (type == "gengu") then	
    	makeRole.gengu = makeRole.gengu + value
    elseif (type == "bili") then	
    	makeRole.bili = makeRole.bili + value
    elseif (type == "fuyuan") then
		makeRole.fuyuan = makeRole.fuyuan + value
    elseif (type == "shenfa") then
		makeRole.shenfa = makeRole.shenfa + value
    elseif (type == "dingli") then
		makeRole.dingli = makeRole.dingli + value
    elseif (type == "wuxing") then
		makeRole.wuxing = makeRole.wuxing + value
    elseif (type == "quanzhang") then
		makeRole.quanzhang = makeRole.quanzhang + value
    elseif (type == "jianfa") then
		makeRole.jianfa = makeRole.jianfa + value
    elseif (type == "daofa") then
		makeRole.daofa = makeRole.daofa + value
    elseif (type == "qimen") then
		makeRole.qimen = makeRole.qimen + value
	end

end