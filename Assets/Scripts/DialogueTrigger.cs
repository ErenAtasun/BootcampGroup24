using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueCharater
{
    public string name;
    public Sprite icon;
}
[System.Serializable]
public class DialogueLine
{
    public DialogueCharater character;
    [TextArea(3, 10)]
    public string line;
}

[System.Serializable]
public class Dialogue
{
    public List<DialogueLine> dialogueLines = new List<DialogueLine>();
}
public class DialogueTrigger : MonoBehaviour
{

    public Dialogue dialogue;

    public void TriggerDialogue()
    {
        DialogueManager.Instance.StartDialogue(dialogue);
    }

    private void OnTriggerEnter(Collider collision) //collider�n i�ine girince �al��s�n
    {
        if(collision.tag == "Player")
        {
            TriggerDialogue();
        }
    }
    {
            
    }
}

// konu�aca��m�z ki�inin �st�ne koyaca��z bu scripti listeyi olu�turaca��z
// ayr�ca collider eklememiz laz�m o ki�iye (is Trigger � a�may� unutma)
// button a DialogueBox�m�z� at ve DisplayNextDialogueLine � se�
