using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TH.Core;

public class Merchant : Animal, IInteractable
{
    #region PublicVariables
    #endregion

    #region PrivateVariables
    private MerchantAI _merchantAI;
    [SerializeField] private List<ItemDataWrapper> _ItemPool;
    private ItemDataWrapper[] _dealItem = new ItemDataWrapper[3];
    #endregion

    #region PublicMethod

    public void Interact(int inventoryIndex)
    {
        if (_merchantAI.state == MerchantAI.MerchantState.Attack)
            return;
        Debug.Log("Deal");
        DealManager.Instance.OpenDealUI(_dealItem);
        Time.timeScale = 0f;
    }
    #endregion

    #region PrivateMethod
    private void Start()
    {
        TryGetComponent(out _merchantAI);
    }

    private void OnEnable()
    {
        for (int i = 0; i < _dealItem.Length; i++)
        {
            int index = Random.Range(0, _ItemPool.Count);
            _dealItem[i] = _ItemPool[index];
        }
    }
    #endregion
}
