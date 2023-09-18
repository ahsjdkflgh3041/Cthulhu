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
                PlayDialogue("�ŷ��� ��� ��������?",3f);
                break;
            case dialogueState.Alert:
                PlayDialogue("�ŷ����� �ʰڳ�?", 0f);
                break;
            case dialogueState.Attack:
                PlayDialogue("���� �� ������ �ǵ��!", 3f);
                break;
            case dialogueState.Hit:
                PlayDialogue("����!", 3f);
                break;
            case dialogueState.FriendlyHit:
                PlayDialogue("���� �Ҹ���?", 3f);
                break;
            case dialogueState.Lost:
                PlayDialogue("��� ����?", 3f);
                break;
            case dialogueState.Chasing:
                PlayDialogue("��� ������!", 3f);
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
