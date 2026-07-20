using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using JyGame;
using UnityEngine;
using UnityEngine.UI;

public class ModSelectUI : MonoBehaviour
{
	public SelectMenu selectMenu;

	public ModItemUI modItem;

	private void Start()
	{
		RefreshLocal();
		selectMenu.gameObject.SetActive(true);
	}

	private void Update()
	{
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

	public void NoMod()
	{
		ModManager.SetCurrentMod(null);
		LoadingUI.Load("MainMenu");
	}

	public void RefreshLocal()
	{
		if (selectMenu == null) return;
		selectMenu.Clear();
		string path = CommonSettings.persistentDataPath + "modcache/";
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
				if (modInfo != null && modItem != null)
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
		RefreshLocal();
	}

	public void OnOnlineMod()
	{
	}

	public void OnGonggao()
	{
	}

	public void OnClose()
	{
		LoadingUI.Load("MainMenu");
	}
}
