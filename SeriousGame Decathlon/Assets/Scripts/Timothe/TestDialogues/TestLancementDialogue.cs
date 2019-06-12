using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLancementDialogue : MonoBehaviour
{
    public DialogueManager dialManage;
    public Dialogue dialogue;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.J))
        {
            dialManage.dialogueGlobal.SetActive(true);
            dialManage.isDialogueOpen = true;
            dialManage.actualDialogue = dialogue;
        }
    }

    public void LancementDialogue()
    {
        dialManage.dialogueGlobal.SetActive(true);
        dialManage.isDialogueOpen = true;
        dialManage.actualDialogue = dialogue;
    }
}
