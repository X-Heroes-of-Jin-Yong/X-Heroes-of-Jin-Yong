using JyGame;
using UnityEngine;
using UnityEngine.UI;

public class SystemPanelUI : MonoBehaviour
{
	public GameObject SavePanelObj;

	public GameObject consolePanelObj;

	public GameObject confirmPanelObj;

	private SavePanelUI SavePanel
	{
		get
		{
			return SavePanelObj.GetComponent<SavePanelUI>();
		}
	}

	public void CancelButtonClicked()
	{
		base.gameObject.SetActive(false);
	}

	public void Save()
	{
		base.gameObject.SetActive(false);
		SavePanel.Show(SavePanelMode.SAVE);
	}

	public void Load()
	{
		base.gameObject.SetActive(false);
		SavePanel.Show(SavePanelMode.LOAD);
	}

	public void BackToMenu()
	{
		confirmPanelObj.GetComponent<ConfirmPanel>().Show("提示，若当前没有存档将丢失目前进度，确认吗？", delegate
		{
			Application.LoadLevel("MainMenu");
		});
	}

	public void OnAutoBattle()
	{
		bool isOn = base.transform.FindChild("Toggles").FindChild("AutoBattleToggle").GetComponent<Toggle>()
			.isOn;
		Configer.IsAutoBattle = isOn;
	}

	public void OnAutoSave()
	{
		bool isOn = base.transform.FindChild("Toggles").FindChild("AutoSaveToggle").GetComponent<Toggle>()
			.isOn;
		Configer.IsAutoSave = isOn;
	}

	public void OnMusic()
	{
		bool isOn = base.transform.FindChild("Toggles").FindChild("MusicToggle").GetComponent<Toggle>()
			.isOn;
		Configer.IsMusicOn = isOn;
	}

	public void OnEffect()
	{
		bool isOn = base.transform.FindChild("Toggles").FindChild("EffectToggle").GetComponent<Toggle>()
			.isOn;
		Configer.IsEffectOn = isOn;
	}

	public void OnScaleBigMap()
	{
		bool isOn = base.transform.FindChild("Toggles").FindChild("ScaleBigMapToggle").GetComponent<Toggle>()
			.isOn;
		Configer.IsBigmapFullScreen = isOn;
	}

	public void OnBattleTip()
	{
		bool isOn = base.transform.FindChild("Toggles").FindChild("BattleTipToggle").GetComponent<Toggle>()
			.isOn;
		Configer.IsBattleTipShow = isOn;
	}

	private void Refresh()
	{
		base.transform.FindChild("Toggles").FindChild("AutoBattleToggle").GetComponent<Toggle>()
			.isOn = Configer.IsAutoBattle;
		base.transform.FindChild("Toggles").FindChild("MusicToggle").GetComponent<Toggle>()
			.isOn = Configer.IsMusicOn;
		base.transform.FindChild("Toggles").FindChild("EffectToggle").GetComponent<Toggle>()
			.isOn = Configer.IsEffectOn;
		base.transform.FindChild("Toggles").FindChild("AutoSaveToggle").GetComponent<Toggle>()
			.isOn = Configer.IsAutoSave;
		base.transform.FindChild("Toggles").FindChild("ScaleBigMapToggle").GetComponent<Toggle>()
			.isOn = Configer.IsBigmapFullScreen;
		base.transform.FindChild("Toggles").FindChild("BattleTipToggle").GetComponent<Toggle>()
			.isOn = Configer.IsBattleTipShow;
	}

	public void Show()
	{
		Refresh();
		base.gameObject.SetActive(true);
	}

	private void Start()
	{
		consolePanelObj.gameObject.SetActive(CommonSettings.DEBUG_CONSOLE);
		base.transform.FindChild("_hotKeySuggestText").gameObject.SetActive(!CommonSettings.TOUCH_MODE);
		base.transform.FindChild("Toggles").FindChild("ScaleBigMapToggle").gameObject.SetActive(CommonSettings.TOUCH_MODE);
	}

	private void Update()
	{
	}
}
