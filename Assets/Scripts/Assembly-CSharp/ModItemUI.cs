using System.IO;
using JyGame;
using UnityEngine;
using UnityEngine.UI;

public class ModItemUI : MonoBehaviour
{
	public Image image;

	public Text text;

	public Text desc;

	public Text loadingText;

	public Button startBtn;

	public Button deleteBtn;

	public ConfirmPanel confirmPanel;

	public ModInfo _mod;

	public ModSelectUI _parent;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void Bind(ModInfo mod, ModSelectUI parent)
	{
		_mod = mod;
		_parent = parent;
		if (startBtn != null) startBtn.gameObject.SetActive(true);
		if (deleteBtn != null) deleteBtn.gameObject.SetActive(true);
		string posterPath = "file:///" + mod.LocalDirPath + "poster.jpg";
		StartCoroutine(Tools.DownloadImage(posterPath, delegate(object sp)
		{
			if (image != null) image.sprite = sp as Sprite;
			if (loadingText != null) loadingText.gameObject.SetActive(false);
		}, delegate
		{
		}, Vector2.zero, 1f));
		if (text != null)
		{
			if (string.IsNullOrEmpty(mod.name))
				text.text = "官方原版";
			else
				text.text = mod.name;
		}
		if (desc != null)
			desc.text = string.Format("作者:{0}  容量:{2}\n版本:{1}  发布时间:{3}\n简介:{4}", mod.author, mod.version, mod.size, mod.date, mod.desc);
	}

	public void OnLoad()
	{
		ModManager.SetCurrentMod(_mod);
		LoadingUI.Load("MainMenu");
	}

	public void OnDelete()
	{
		if (confirmPanel != null)
		{
			confirmPanel.Show("确认要删除吗？（该MOD存档也将被删除）", delegate
			{
				File.Delete(_mod.LocalXmlPath);
				Directory.Delete(_mod.LocalDirPath, true);
				if (_parent != null) _parent.RefreshLocal();
			});
		}
	}

	public void OnDownload()
	{
	}

	public void AbortDownload()
	{
	}
}
