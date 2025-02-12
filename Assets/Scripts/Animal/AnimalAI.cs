using System.Collections;
using System.Collections.Generic;
using TH.Core;
using UnityEngine;

[RequireComponent(typeof(Animal))]
public class AnimalAI : MonoBehaviour
{
	#region PublicVariables
	#endregion

	#region PrivateVariables
	protected Animal _animal;
	[SerializeField] protected float _attackRange;	// 월드 매니저 이니셜라이즈 안 하면 제대로 값이 안 들어가서 임시로 Serialize 시켜둠
	[SerializeField] protected float _recognitionIn;  // 월드 매니저 이니셜라이즈 안 하면 제대로 값이 안 들어가서 임시로 Serialize 시켜둠
	[SerializeField] public float _recognitionOut; // 월드 매니저 이니셜라이즈 안 하면 제대로 값이 안 들어가서 임시로 Serialize 시켜둠

	protected Animator _animator;
	protected Collider2D _collider;
	protected bool _chase;
	protected float timer;
	[SerializeField] protected float timeToNextDestinationSetting;
	#endregion

	#region PublicMethod
	public void SetRange(string objectID)
	{
		AnimalData data = WorldManager.Instance.GetObjectData(objectID) as AnimalData;
		_attackRange = data.attackRange;
		_recognitionIn = data.recognitionRangeIn;
		_recognitionOut = data.recognitionRangeOut;
	}
	#endregion

	#region PrivateMethod
	private void Awake()
	{
		transform.Find("Renderer").TryGetComponent(out _animator);
		TryGetComponent(out _animal);
		timer = 0f;
	}
	protected virtual void Update()
	{
		_collider = Physics2D.OverlapCircle(transform.position, _recognitionOut, 1 << LayerMask.NameToLayer("Player"));
		if (_collider != null)
		{
			timer = 0f;
			float distance = Vector2.Distance(transform.position, _collider.transform.position);
			if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Attack") == false && distance < _attackRange)
			{
				_animal.Attack(_collider);
			}
			else if(_chase == false && distance < _recognitionIn)
			{
				_chase = true;
				_animal.ChasePlayer();
			}
			else if(_chase == true && distance > _recognitionOut)
			{
				_chase = false;
				_animal.Idle();
			}
		}
		else
		{
			timer += Time.deltaTime;
			if(timer > timeToNextDestinationSetting)
			{
				timer = 0;
				_animal.Idle();
			}
		}
	}
	#endregion
}
