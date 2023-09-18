using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animal))]
public class MerchantAI : AnimalAI, IHittableHuman
{
	#region PublicVariables
	#endregion

	#region PrivateVariables
	private MerchantDialogue _merchantDialogue;

	public enum MerchantState { Idle, Attack};

	public MerchantState state = MerchantState.Idle;
	#endregion

	#region PublicMethod
	public void Hit()
	{
		state = MerchantState.Attack;
		_merchantDialogue.PlayDialogueByState(dialogueState.Hit);
	}
	public void FriendlyHit()
	{
		state = MerchantState.Attack;
		_merchantDialogue.PlayDialogueByState(dialogueState.FriendlyHit);
	}
	#endregion

	#region PrivateMethod

	private void Start()
	{
		TryGetComponent(out _merchantDialogue);
	}

	protected override void Update()
	{
		_collider = Physics2D.OverlapCircle(transform.position, _recognitionOut, 1 << LayerMask.NameToLayer("Player"));
		if (state == MerchantState.Attack)
		{
			if (_collider != null)
			{
				timer = 0f;
				float distance = Vector2.Distance(transform.position, _collider.transform.position);
				if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Attack") == false && distance < _attackRange)
				{
					_animal.Attack(_collider);
					_merchantDialogue.PlayDialogueByState(dialogueState.Attack);
				}
				else if (_chase == false && distance < _recognitionIn)
				{
					_chase = true;
					_animal.ChasePlayer();
					_merchantDialogue.PlayDialogueByState(dialogueState.Chasing);
				}
				else if (_chase == true && distance > _recognitionOut)
				{
					_chase = false;
					_animal.Idle();
					_merchantDialogue.PlayDialogueByState(dialogueState.Lost);
				}
			}
		}
		else if (_collider != null)
		{
			_merchantDialogue.PlayDialogueByState(dialogueState.Alert);
        }
		else if (state == MerchantState.Idle)
		{
			timer += Time.deltaTime;
			if (timer > timeToNextDestinationSetting)
			{
				timer = 0;
				_merchantDialogue.PlayDialogueByState(dialogueState.Idle);
				_animal.Idle();
			}
		}
	}
	#endregion
}
