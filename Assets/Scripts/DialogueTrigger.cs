using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueCharater
{
    public string name; // Karakter ismi
    public Sprite icon; // Karakter resmi
}

[System.Serializable]
public class DialogueLine
{
    public DialogueCharater character; // Diyalog karakteri
    [TextArea(3, 10)]
    public string line; // Diyalog metni
}

[System.Serializable]
public class Dialogue
{
    public List<DialogueLine> dialogueLines = new List<DialogueLine>(); // Diyalog sat�rlar�n� saklayan liste
}

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue; // Bu nesne i�in diyalog

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // E�er tetikleyici "Player" etiketine sahipse
        {
            TriggerDialogue(); // Diyalo�u ba�lat
        }
    }

    public void TriggerDialogue()
    {
        if (DialogueManager.Instance != null)
        {
            DialogueManager.Instance.StartDialogue(dialogue); // Diyalo�u ba�lat
        }
    }
}



// konu�aca��m�z ki�inin �st�ne koyaca��z bu scripti listeyi olu�turaca��z
// ayr�ca collider eklememiz laz�m o ki�iye (is Trigger � a�may� unutma)
// button a DialogueBox�m�z� at ve DisplayNextDialogueLine � se� 
/*
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueManager dialogueManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DialogueSetup npc = GetComponent<DialogueSetup>();

            if (npc != null)
            {
                dialogueManager.StartDialogue(npc.dialogues);
            }
        }
    }
}

*/
