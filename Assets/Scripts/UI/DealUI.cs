using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using TH.Core;

public class DealUI : MonoBehaviour
{
	#region PublicVariables
	#endregion

	#region PrivateVariables
	[SerializeField] private DealUIButton[] _dealUI;
	#endregion

	#region PublicMethod
	[Button]
	public void SetCardUI(ItemDataWrapper[] gotItemDatas)
	{
		if (gotItemDatas.Length != _dealUI.Length)
		{
			//시간 남을 시 스크롤과 자동 추가 넣기
			Debug.LogError("Count of dealUI and Item that merchent got is different! Fix!!!!");
			return;
		}
		for (int i = 0; i < _dealUI.Length; i++)
		{
			_dealUI[i].SetText(gotItemDatas[i].itemData, (item) => DealManager.Instance.Deal(item));
		}
	}

	public void Deactivate()
	{
		Time.timeScale = 1f;
		gameObject.SetActive(false);
	}
	#endregion

	#region PrivateMethod
    #endregion
}
