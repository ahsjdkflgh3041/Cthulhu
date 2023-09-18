using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MerchantDialogue : MonoBehaviour
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
                PlayDialogue("거래할 사람 없으려나?",3f);
                break;
            case dialogueState.Alert:
                PlayDialogue("거래하지 않겠나?", 0f);
                break;
            case dialogueState.Attack:
                PlayDialogue("감히 내 물건을 건드려!", 3f);
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
                PlayDialogue("어딜 도망가!", 3f);
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
