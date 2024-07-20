using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    public Image characterIcon; // Karakter resmini g�stermek i�in
    public TextMeshProUGUI characterName; // Karakter ismini g�stermek i�in
    public TextMeshProUGUI dialogueArea; // Diyalog metnini g�stermek i�in
    public Button nextButton; // Sonraki diyaloga ge�mek i�in buton

    private Queue<DialogueLine> lines; // Diyalog sat�rlar�n� saklamak i�in
    private bool isDialogueActive = false; // Diyalog aktif mi kontrol�
    private bool isTyping = false; // Metin yaz�m�n�n devam edip etmedi�ini kontrol etmek
    public float typingSpeed = 0.05f; // Metin yazma h�z�
    public Animator animator; // Diyalog kutular�n�n animasyonunu kontrol etmek i�in

    void Awake()
    {
        if (Instance == null)
            Instance = this;

        lines = new Queue<DialogueLine>(); // Kuyru�u ba�lat
        if (nextButton != null)
        {
            nextButton.onClick.AddListener(OnNextButtonClicked); // Butona t�klama olay�n� ekle
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        if (isDialogueActive)
        {
            Debug.Log("Dialogue is already active.");
            return; // E�er diyalog aktifse tekrar ba�latma
        }

        isDialogueActive = true;
        animator.SetBool("isOpen", true); // Diyalog a��ld���nda animasyonu ba�lat

        lines.Clear(); // �nceki diyaloglar� temizle

        foreach (DialogueLine dialogueLine in dialogue.dialogueLines)
        {
            lines.Enqueue(dialogueLine); // Diyalog sat�rlar�n� kuyruga ekle
        }

        DisplayNextDialogueLine(); // �lk diyalo�u g�ster
    }

    public void DisplayNextDialogueLine()
    {
        if (lines.Count == 0)
        {
            EndDialogue(); // Diyalog bitmi�se, diyalogu sonland�r
            return;
        }

        if (isTyping) return; // E�er yaz�m devam ediyorsa, ge�i� yapma


        DialogueLine currentLine = lines.Dequeue(); // Kuyruktan bir diyalog sat�r� al

        characterIcon.sprite = currentLine.character.icon; // Karakter resmini g�ncelle
        characterName.text = currentLine.character.name; // Karakter ismini g�ncelle

        StopAllCoroutines(); // �nceki coroutine'leri durdur
        StartCoroutine(TypeSentence(currentLine.line)); // Yeni diyalo�u yazd�r
    }

    IEnumerator TypeSentence(string sentence)
    {
        isTyping = true; // Yazma i�lemi ba�l�yor

        dialogueArea.text = ""; // Metin alan�n� temizle

        foreach (char letter in sentence.ToCharArray())
        {
            dialogueArea.text += letter; // Her harfi ekle
            yield return new WaitForSeconds(typingSpeed); // Yazma h�z�n� ayarla
        }
        isTyping = false;
    }

    void OnNextButtonClicked()
    {
        if (isDialogueActive)
        {
            DisplayNextDialogueLine(); // Butona t�klan�rsa bir sonraki diyalo�u g�ster
        }
    }

    void EndDialogue()
    {
        isDialogueActive = false;
        animator.SetBool("isOpen", false); // Diyalog bitti�inde animasyonu kapat
    }
}




//dialogueBox a at

/*
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class Dialogue
{
    public string characterName;
    public Sprite characterImage;
    public string dialogueText;
}

public class DialogueManager : MonoBehaviour
{
    public GameObject dialogueBox1;
    public GameObject dialogueBox2;

    public TextMeshProUGUI nameText1;
    public Image characterImage1;
    public TextMeshProUGUI dialogueText1;

    public TextMeshProUGUI nameText2;
    public Image characterImage2;
    public TextMeshProUGUI dialogueText2;

    public Button nextButton1;
    public Button nextButton2;

    private Dialogue[] dialogues;
    private int currentDialogueIndex = 0;
    public Animator animator1;
    public Animator animator2;

    private void Start()
    {
        if (nextButton1 != null)
        {
            nextButton1.onClick.AddListener(ShowNextDialogue);
        }
        if (nextButton2 != null)
        {
            nextButton2.onClick.AddListener(ShowNextDialogue);
        }

        // Ba�lang��ta t�m diyalog kutular�n�n g�r�n�rl���n� kontrol et
        dialogueBox1.SetActive(false);
        dialogueBox2.SetActive(false);
    }

    public void StartDialogue(Dialogue[] npcDialogues)
    {
        dialogues = npcDialogues;
        currentDialogueIndex = 0;

        // Diyalog kutular�n� ba�lat
        dialogueBox1.SetActive(true);
        dialogueBox2.SetActive(false);

        // Animasyonlar� ba�lat
        if (animator1 != null)
        {
            animator1.SetBool("isOpen", true);
        }
        if (animator2 != null)
        {
            animator2.SetBool("isOpen", true);
        }

        ShowNextDialogue(); // �lk diyalo�u g�ster
    }

    public void ShowNextDialogue()
    {
        if (currentDialogueIndex < dialogues.Length)
        {
            Dialogue currentDialogue = dialogues[currentDialogueIndex];
            if (currentDialogueIndex % 2 == 0)
            {
                nameText1.text = currentDialogue.characterName;
                characterImage1.sprite = currentDialogue.characterImage;
                dialogueText1.text = currentDialogue.dialogueText;

                // �kinci kutuyu kapat ve birincisini a�
                dialogueBox2.SetActive(false);
                dialogueBox1.SetActive(true);

                // Butonu aktif hale getir
                nextButton1.gameObject.SetActive(true);
                nextButton2.gameObject.SetActive(false);
            }
            else
            {
                nameText2.text = currentDialogue.characterName;
                characterImage2.sprite = currentDialogue.characterImage;
                dialogueText2.text = currentDialogue.dialogueText;

                // Birinci kutuyu kapat ve ikincisini a�
                dialogueBox1.SetActive(false);
                dialogueBox2.SetActive(true);

                // Butonu aktif hale getir
                nextButton1.gameObject.SetActive(false);
                nextButton2.gameObject.SetActive(true);
            }
            currentDialogueIndex++;
        }
        else
        {
            EndDialogue();
        }
    }

    private void EndDialogue()
    {
        // Diyalog kutular�n� kapat
        dialogueBox1.SetActive(false);
        dialogueBox2.SetActive(false);

        // Animasyonlar� durdur
        if (animator1 != null)
        {
            animator1.SetBool("isOpen", false);
        }
        if (animator2 != null)
        {
            animator2.SetBool("isOpen", false);
        }
    }
}
*/