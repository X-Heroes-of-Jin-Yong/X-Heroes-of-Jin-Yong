using System.Collections;
using JyGame;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
	public GameObject savePanelObj;

	public GameObject musicPanelObj;

	public Text modText;

	public GameObject uiCanvas;

	public GameObject copyRightTextObj;

	public Image backgroundImage;

	public GameObject ClearAllConfirmPanelObj;

	private int _clearAllCount;

	private static bool _mod_editor_checked;

	public void OnNewGame()
	{
		RuntimeData.Instance.Init();
		LoadingUI.Load("RollRole");
	}

	public void OnLoad()
	{
		savePanelObj.GetComponent<SavePanelUI>().Show(SavePanelMode.LOAD);
	}

	public void OnMusic()
	{
		musicPanelObj.GetComponent<MusicPanelUI>().Show();
	}

	public void OnDeveloper()
	{
		RuntimeData.Instance.gameEngine.SwitchGameScene("story", "aboutus_main");
	}

	public void OnMod()
	{
		Application.LoadLevel("ModSelect");
	}

	public void OnGameFinButton()
	{
		Application.LoadLevel("MainMenu");
	}

	public void OnDebugClearAll()
	{
		_clearAllCount++;
		if (_clearAllCount > 10 && ClearAllConfirmPanelObj != null)
		{
			ClearAllConfirmPanelObj.SetActive(true);
		}
	}

	public void OnConfirmClear()
	{
		ModData.ClearAll();
		if (ClearAllConfirmPanelObj != null) ClearAllConfirmPanelObj.SetActive(false);
	}

	public void OnCancelClear()
	{
		if (ClearAllConfirmPanelObj != null) ClearAllConfirmPanelObj.SetActive(false);
		_clearAllCount = 0;
	}

	private void Start()
	{
		LuaManager.Reload();
		if (CommonSettings.MOD_MODE)
		{
			if (!_mod_editor_checked && CommonSettings.CHECK_SCRIPT_RESOURCE)
			{
				ResourceChecker.CheckAll(new FileLogger());
				_mod_editor_checked = true;
			}
			if (copyRightTextObj != null)
				copyRightTextObj.GetComponent<Text>().text = "版权声明：金庸群侠传X（MOD版）所有非官方发布的MOD，MOD作者负有全部责任。涉及的游戏内容、素材、发布行为，均与汉家松鼠工作室无关。";
			if (modText != null)
				modText.text = string.Format("MOD版本:{0}(V{1})", ModData.CurrentMod.name, ModData.CurrentMod.version);
		}
		else
		{
			if (modText != null) modText.text = string.Empty;
		}
		if (uiCanvas != null) uiCanvas.gameObject.SetActive(true);
		ShowMainMenu();
	}

	private void ShowMainMenu()
	{
		if (!RuntimeData.Instance.IsInited)
		{
			RuntimeData.Instance.Init();
		}
		string config = GameConfig.MAINMENU_BG;
		if (!string.IsNullOrEmpty(config) && Resource.GetImage(config) != null && backgroundImage != null)
		{
			backgroundImage.sprite = Resource.GetImage(config);
		}
		string config2 = GameConfig.MAINMENU_MUSIC;
		AudioManager.Instance.Play(config2);
	}

	private void Update()
	{
	}
}
