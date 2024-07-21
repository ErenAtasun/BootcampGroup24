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

    private DialogueTrigger currentTrigger; // Mevcut diyalog tetikleyicisini sakla

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

    public void StartDialogue(Dialogue dialogue, DialogueTrigger trigger)
    {
        if (isDialogueActive)
        {
            Debug.Log("Dialogue is already active.");
            return; // E�er diyalog aktifse tekrar ba�latma
        }

        currentTrigger = trigger; // Mevcut diyalog tetikleyicisini sakla
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

        isTyping = false; // Yazma i�lemi bitti
    }

    void OnNextButtonClicked()
    {
        if (isDialogueActive && !isTyping)
        {
            DisplayNextDialogueLine(); // Butona t�klan�rsa bir sonraki diyalo�u g�ster
        }
    }

    void EndDialogue()
    {
        isDialogueActive = false;
        animator.SetBool("isOpen", false); // Diyalog bitti�inde animasyonu kapat

        // Mevcut diyalog tetikleyicisini yok et
        if (currentTrigger != null)
        {
            Destroy(currentTrigger.gameObject);
        }
    }
}

