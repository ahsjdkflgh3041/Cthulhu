using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HumanDropITems : MonoBehaviour
{
	#region PublicVariables
	#endregion

	#region PrivateVariables
	[SerializeField] private List<GameObject> dropItemPool;
	[SerializeField] private int pickItemCount;

    [SerializeField] private float circleRadius = 1.0f; // 원의 반지름
    #endregion

    #region PublicMethod
    #endregion

    #region PrivateMethod
    private void OnEnable()
    {
        for (int i = 0; i < pickItemCount; i++)
        {
            int itemNum = Random.Range(0, dropItemPool.Count);
            Instantiate(dropItemPool[itemNum], SetCirclePosition(360f * ((float)i / pickItemCount)), Quaternion.identity);
        }
    }

    private Vector2 SetCirclePosition(float angle)
    {
        float lineAngleRadians = Mathf.Deg2Rad * angle;

        // 원과 선의 교점 계산
        float x = transform.position.x + circleRadius * Mathf.Cos(lineAngleRadians);
        float y = transform.position.y + circleRadius * Mathf.Sin(lineAngleRadians);

        // 교점 위치를 설정
        return new(x, y);
    }
    #endregion
}
