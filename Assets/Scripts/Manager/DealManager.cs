using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TH.Core;

public class DealManager : Singleton<DealManager>
{
	#region PublicVariables
	#endregion

	#region PrivateVariables
	[SerializeField] private DealUI dealUI;
	private InventoryOwner inventoryOwnerPlayer;
	#endregion

	#region PublicMethod
	public void OpenDealUI(ItemDataWrapper[] gotItemDatas)
	{
		dealUI.gameObject.SetActive(true);
		dealUI.SetCardUI(gotItemDatas);
	}

	public bool Deal(ItemData dealItem)
	{
		if (GameManager.Instance.Gold - dealItem.Gold < 0)
			return false;
		GameManager.Instance.AddGold(-dealItem.Gold);
		InventorySystem.Instance.GetInventory(inventoryOwnerPlayer).AddItem(dealItem, 1);
		return true;
	}
    #endregion

    #region PrivateMethod
    private void Awake()
    {
		inventoryOwnerPlayer = FindObjectOfType<PlayerItemGetter>();
    }
    #endregion
}
