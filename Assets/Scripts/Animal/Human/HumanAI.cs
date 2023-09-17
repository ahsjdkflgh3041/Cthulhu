using System.Collections;
using System.Collections.Generic;
using TH.Core;
using UnityEngine;

[RequireComponent(typeof(Animal))]
public class HumanAI : AnimalAI
{
    #region PublicVariables
	#endregion

	#region PrivateVariables
	[SerializeField] private float _alertTimer = 0f;
	[SerializeField] private float _alertTimeMax = 5f;

	private bool _isAttackMode;

	private HumanDialogue _humanDialogue;
	#endregion

	#region PublicMethod
	public void Hit()
	{
		_isAttackMode = true;
		_humanDialogue.PlayDialogueByState(dialogueState.Hit);
	}
	public void FriendlyHit()
	{
		_isAttackMode = true;
		_humanDialogue.PlayDialogueByState(dialogueState.FriendlyHit);

	}
	#endregion

	#region PrivateMethod

	private void Start()
    {
		TryGetComponent(out _humanDialogue);
    }

    protected override void Update()
	{
		_collider = Physics2D.OverlapCircle(transform.position, _recognitionOut, 1 << LayerMask.NameToLayer("Player"));
		if (_isAttackMode)
		{
			if (_collider != null)
			{
				timer = 0f;
				float distance = Vector2.Distance(transform.position, _collider.transform.position);
				if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Attack") == false && distance < _attackRange)
				{
					_animal.Attack(_collider);
					_humanDialogue.PlayDialogueByState(dialogueState.Attack);
				}
				else if (_chase == false && distance < _recognitionIn)
				{
					_chase = true;
					_animal.ChasePlayer();
					_humanDialogue.PlayDialogueByState(dialogueState.Chasing);
				}
				else if (_chase == true && distance > _recognitionOut)
				{
					_chase = false;
					_animal.Idle();
					_humanDialogue.PlayDialogueByState(dialogueState.Lost);
				}
			}
		}
		else if (_collider != null)
		{
			_humanDialogue.PlayDialogueByState(dialogueState.Alert);
			_alertTimer += Time.deltaTime;
			if (_alertTimer > _alertTimeMax)
			{
				_isAttackMode = true;
			}
		}
		else
		{
			_alertTimer = 0f;
			timer += Time.deltaTime;
			if (timer > timeToNextDestinationSetting)
			{
				timer = 0;
				_humanDialogue.PlayDialogueByState(dialogueState.Idle);
				_animal.Idle();
				
			}
		}
    }
	#endregion
}
