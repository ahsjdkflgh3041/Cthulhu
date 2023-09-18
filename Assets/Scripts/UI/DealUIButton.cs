using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TH.Core;

public class DealUIButton : MonoBehaviour, IPointerClickHandler
{
	#region PublicVariables
	#endregion

	#region PrivateVariables
	private ItemData _itemData;
	private Func<ItemData, bool> _action;
	private bool _isClicked;
	private bool _unselect;
	private int _price;
	#endregion

	#region PublicMethod
	public void SetText(ItemData data, Func<ItemData, bool> action)
	{
		_itemData = data;
		_action = action;
		_isClicked = false;
		_unselect = false;
		transform.Find("DealImage").GetComponent<Image>().sprite = data.ItemSprite;
		transform.Find("DealName").GetComponent<TMPro.TextMeshProUGUI>().text = data.ItemName;
		transform.Find("DealDiscription").GetComponent<TMPro.TextMeshProUGUI>().text = data.ItemDescription;
		_price = data.Gold;
		transform.Find("DealPrice").GetComponent<TMPro.TextMeshProUGUI>().text = _price.ToString();
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if (_unselect) return;
		if (_isClicked) return;
		_isClicked = true;
		if(_action(_itemData))
			Select();
	}

	public void Select()
	{
		GetComponent<Image>().color = Color.black;
		_unselect = true;
	}
	#endregion

	#region PrivateMethod
	#endregion
}
