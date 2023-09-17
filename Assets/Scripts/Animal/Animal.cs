using DG.Tweening;
using Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TH.Core;

public class Animal : WorldObject
{
	#region PublicVariables
	#endregion

	#region PrivateVariables
	private AnimalAI _ai;
	private Animator _animator;
	private AnimalMove _move;

	private float detectionRange = 10f;
	private LayerMask objectLayer = 1 << 6;

	private int _damage;
	#endregion

	#region PublicMethod
	public override void Init(string id, Vector2Int areaPos, Action<string, Vector2Int> onObjectDestroyed)
	{
		base.Init(id, areaPos, onObjectDestroyed);
		AnimalData data = WorldManager.Instance.GetObjectData(_objectID) as AnimalData;
		_damage = data.damage;
		_move.SetSpeed(data.speedIdle);
		_drop.SetObjectID(_objectID);
		_move.SetObjectID(_objectID);
		_ai.SetRange(_objectID);
	}
	public string GetObjectID() => _objectID;
	public void Idle()
	{
		_move.RandomMove();
	}
	public void ChasePlayer()
	{
		_move.ChasePlayer();
	}
	public void Attack(Collider2D target)
	{
		_animator.SetTrigger("attack");
		target.GetComponent<Player>().Hit(_damage);
	}
	#endregion

	#region PrivateMethod
	protected override void Awake()
	{
		base.Awake();
		TryGetComponent(out _move);
		TryGetComponent(out _ai);
		transform.Find("Renderer").TryGetComponent(out _animator);
	}

    public override void Hit(int damage)
    {
        base.Hit(damage);
		if (_ai is HumanAI humanAI)
		{
			humanAI.Hit();
			Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, detectionRange, objectLayer);

			foreach (Collider2D collider in colliders)
			{
				// 특정 컴포넌트가 있는지 확인 (예: ItemComponent)
				HumanAI foundHumanAI = collider.GetComponent<HumanAI>();

				if (foundHumanAI != null)
				{
					// 검출된 아이템 처리 또는 상호작용 코드를 여기에 추가
					foundHumanAI.FriendlyHit();
				}
			}
		}
    }
    #endregion
}