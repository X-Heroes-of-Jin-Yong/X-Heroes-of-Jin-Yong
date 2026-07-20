using JyGame;
using UnityEngine;
using UnityEngine.UI;

public class ModDownloadPanelUI : MonoBehaviour
{
	public Slider slider;

	public Text text;

	public ModItemUI modUI;

	public Button abortButton;

	public Button startButton;

	private ModItemUI _modItem;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void Bind(ModItemUI modItem)
	{
		_modItem = modItem;
		if (modUI != null && modItem != null)
		{
			if (modUI.image != null && modItem.image != null)
				modUI.image.sprite = modItem.image.sprite;
			if (modUI.text != null && modItem.text != null)
				modUI.text.text = modItem.text.text;
			if (modUI.desc != null && modItem.desc != null)
				modUI.desc.text = modItem.desc.text;
		}
		if (startButton != null) startButton.gameObject.SetActive(false);
	}

	public void Show(float progress, string info)
	{
		base.gameObject.SetActive(true);
		if (slider != null) slider.value = progress;
		if (text != null) text.text = info;
	}

	public void Hide()
	{
		base.gameObject.SetActive(false);
	}

	public void DownloadFinished()
	{
		if (text != null) text.text = "下载完成!";
		if (startButton != null) startButton.gameObject.SetActive(true);
	}

	public void OnCancel()
	{
		Hide();
		if (_modItem != null) _modItem.AbortDownload();
	}

	public void OnStartGame()
	{
		if (_modItem != null) ModManager.SetCurrentMod(_modItem._mod);
		LoadingUI.Load("MainMenu");
	}
}
