using JyGame;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	private GameObject _previewPanel;

	private ItemInstance _item;

	public void Bind(ItemInstance item, int count, CommonSettings.VoidCallBack callback, CommonSettings.JudgeCallback isActiveCallback = null, GameObject previewPanel = null)
	{
		_previewPanel = previewPanel;
		_item = item;
		if (isActiveCallback != null)
		{
			if (!isActiveCallback(item))
			{
				base.transform.FindChild("CrossImage").gameObject.SetActive(true);
			}
			else
			{
				base.transform.FindChild("CrossImage").gameObject.SetActive(false);
			}
		}
		else
		{
			base.transform.FindChild("CrossImage").gameObject.SetActive(false);
		}
		base.transform.FindChild("Text").GetComponent<Text>().text = item.Name;
		Color color = item.GetColor();
		color.a = 0.3f;
		base.transform.FindChild("_textBg").GetComponent<Image>().color = color;
		if (count == 1 || count == -1)
		{
			base.transform.FindChild("NumberText").GetComponent<Text>().text = string.Empty;
		}
		else
		{
			base.transform.FindChild("NumberText").GetComponent<Text>().text = count.ToString();
		}
		GetComponent<Button>().onClick.RemoveAllListeners();
		GetComponent<Button>().onClick.AddListener(delegate
		{
			callback();
		});
		if (base.transform.FindChild("_mask") != null && base.transform.FindChild("_mask").FindChild("Image") != null)
		{
			base.transform.FindChild("_mask").FindChild("Image").GetComponent<Image>()
				.sprite = Resource.GetImage(item.pic);
		}
		else if (base.transform.FindChild("Image") != null)
		{
			base.transform.FindChild("Image").GetComponent<Image>().sprite = Resource.GetImage(item.pic);
		}
		else
		{
			GetComponent<Image>().sprite = Resource.GetImage(item.pic);
		}
	}

	public void OnPointerEnter(PointerEventData data)
	{
		if (!CommonSettings.TOUCH_MODE && _previewPanel != null)
		{
			_previewPanel.GetComponent<ItemPreviewPanelUI>().Show(_item.Name + "\n" + _item.DescriptionInRichtextBlackEnd);
		}
	}

	public void OnPointerExit(PointerEventData data)
	{
		if (!CommonSettings.TOUCH_MODE && _previewPanel != null)
		{
			_previewPanel.GetComponent<ItemPreviewPanelUI>().Hide();
		}
	}

	private void Start()
	{
	}

	private void Update()
	{
	}
}
