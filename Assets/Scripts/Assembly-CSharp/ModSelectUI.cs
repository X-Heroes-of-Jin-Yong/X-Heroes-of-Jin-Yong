using System;
using System.Collections;
using System.IO;
using System.Xml;
using Cysharp.Threading.Tasks;
using JyGame;
using UnityEngine;
using UnityEngine.UI;

public class ModSelectUI : MonoBehaviour
{
	public SelectMenu selectMenu;

	public ModItemUI modItem;

	public Text downloadingText;

	public GameObject gonggaoPanel;

	public Text gonggaoText;

	public Text versionText;

	public Button closeBtn;

	private void Start()
	{
		OnLocalMod();
		closeBtn.gameObject.SetActive(AssetBundleManager.IsInited);
	}

	public ModInfo LoadSingleModInfo(string xml)
	{
		XmlDocument xmlDocument = new XmlDocument();
		xmlDocument.LoadXml(xml);
		IEnumerator enumerator = xmlDocument.SelectNodes("root/mod").GetEnumerator();
		try
		{
			if (enumerator.MoveNext())
			{
				XmlNode xmlNode = (XmlNode)enumerator.Current;
				return BasePojo.Create<ModInfo>(xmlNode.OuterXml);
			}
		}
		finally
		{
			IDisposable disposable = enumerator as IDisposable;
			if (disposable != null)
			{
				disposable.Dispose();
			}
		}
		return null;
	}

	public void ImportMod()
	{
		downloadingText.gameObject.SetActive(true);
		downloadingText.text = "请选择MOD压缩包...";
#if UNITY_ANDROID && !UNITY_EDITOR
		AndroidSafModPicker.Open(gameObject.name, nameof(OnModArchiveSelected));
#elif UNITY_EDITOR
		string archivePath = UnityEditor.EditorUtility.OpenFilePanel("选择MOD压缩包", string.Empty, "zip");
		OnModArchiveSelected(archivePath);
#else
		downloadingText.text = "当前平台暂未实现本地文件选择";
#endif
	}

	public void OnModArchiveSelected(string result)
	{
		if (string.IsNullOrEmpty(result))
		{
			downloadingText.text = "已取消导入";
			return;
		}
		if (result.StartsWith("ERROR:", StringComparison.Ordinal))
		{
			downloadingText.text = "选择文件失败：" + result.Substring(6);
			return;
		}
		ImportModAsync(result).Forget();
	}

	private async UniTaskVoid ImportModAsync(string archivePath)
	{
		try
		{
			downloadingText.text = "正在校验并导入MOD...";
			ModInfo importedMod = await ModArchiveImporter.ImportAsync(archivePath);
			downloadingText.text = $"MOD {importedMod.name} 导入成功";
			OnLocalMod();
		}
		catch (Exception ex)
		{
			Debug.LogException(ex);
			downloadingText.text = "导入失败：" + ex.Message;
		}
	}

	public void RefreshLocal()
	{
		selectMenu.Clear();
		string path = CommonSettings.persistentDataPath + "/modcache/";
		if (!Directory.Exists(path))
		{
			Directory.CreateDirectory(path);
		}
		FileInfo[] files = new DirectoryInfo(path).GetFiles("*.xml");
		foreach (FileInfo fileInfo in files)
		{
			using (StreamReader streamReader = new StreamReader(fileInfo.FullName))
			{
				ModInfo modInfo = LoadSingleModInfo(streamReader.ReadToEnd());
				if (modInfo != null)
				{
					GameObject gameObject = UnityEngine.Object.Instantiate(modItem.gameObject);
					gameObject.GetComponent<ModItemUI>().Bind(modInfo, this);
					selectMenu.AddItem(gameObject);
				}
			}
		}
	}

	public void OnLocalMod()
	{
		selectMenu.gameObject.SetActive(true);
		gonggaoPanel.SetActive(false);
		RefreshLocal();
	}

	public void OnGonggao()
	{
		selectMenu.gameObject.SetActive(false);
		gonggaoPanel.SetActive(true);
	}

	public void OnClose()
	{
		LoadingUI.Load("MainMenu");
	}
}
