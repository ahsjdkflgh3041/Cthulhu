using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum dialogueState { Idle, Alert, Attack, Hit, FriendlyHit, Lost, Chasing}

public class HumanDialogue : MonoBehaviour
{
    #region PublicVariables
    #endregion

    #region PrivateVariables
    [SerializeField] private TMP_Text text;

    private Coroutine exitDialogueCoroutine;
	#endregion

	#region PublicMethod
	public void PlayDialogueByState(dialogueState state)
	{
        text.gameObject.SetActive(true);
        switch (state)
        {
            case dialogueState.Idle:
                PlayDialogue("여기가 어디지?",3f);
                break;
            case dialogueState.Alert:
                PlayDialogue("더 이상 가까이 오지 마!", 0f);
                break;
            case dialogueState.Attack:
                PlayDialogue("죽어라!", 3f);
                break;
            case dialogueState.Hit:
                PlayDialogue("으악!", 3f);
                break;
            case dialogueState.FriendlyHit:
                PlayDialogue("무슨 소리지?", 3f);
                break;
            case dialogueState.Lost:
                PlayDialogue("어디 갔지?", 3f);
                break;
            case dialogueState.Chasing:
                PlayDialogue("저기 있다!", 3f);
                break;
        }
    }
    #endregion

    #region PrivateMethod
    private void Start()
    {
        text.gameObject.SetActive(false);
    }

    private void PlayDialogue(string context, float time)
    {
        text.text = context;
        if (exitDialogueCoroutine != null)
            StopCoroutine(exitDialogueCoroutine);
        exitDialogueCoroutine = StartCoroutine(nameof(ExitDialogue), time);
    }

    private IEnumerator ExitDialogue(float time)
    {
        yield return new WaitForSeconds(time);
        text.gameObject.SetActive(false);
        exitDialogueCoroutine = null;
    }
    #endregion
}
