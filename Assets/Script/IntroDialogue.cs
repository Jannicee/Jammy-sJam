using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IntroDialogue : MonoBehaviour
{
    [System.Serializable]
    public class DialogueLine
    {
        public string characterName;

        [TextArea(2, 4)]
        public string dialogue;

        public Sprite characterSprite;
    }

    [Header("UI References")]
    public GameObject introPanel;
    public Image characterImage;
    public TMP_Text nameText;
    public TMP_Text dialogueText;
    public Button nextButton;

    [Header("Dialogue Content")]
    public DialogueLine[] dialogueLines;

    private int currentLine = 0;
    private bool isDialogueActive = false;

    private void Awake()
    {
        introPanel.SetActive(false);
    }

    private void OnEnable()
    {
        if (nextButton == null)
            return;

        nextButton.onClick.RemoveListener(NextDialogue);
        nextButton.onClick.AddListener(NextDialogue);
    }

    private void OnDisable()
    {
        if (nextButton != null)
            nextButton.onClick.RemoveListener(NextDialogue);
    }

    public void OpenDialogue()
    {
        if (dialogueLines == null || dialogueLines.Length == 0)
        {
            Debug.LogWarning("Dialog belum diisi!");
            return;
        }

        currentLine = 0;
        isDialogueActive = true;

        introPanel.SetActive(true);

        ShowDialogue();
    }

    public void NextDialogue()
    {
        if (!isDialogueActive)
        {
            Debug.LogWarning("Dialog sedang tidak aktif!");
            return;
        }

        Debug.Log(
            $"Sebelum Next: currentLine = {currentLine}, " +
            $"jumlah dialog = {dialogueLines.Length}"
        );

        currentLine++;

        Debug.Log("Sesudah increment: currentLine = " + currentLine);

        if (currentLine >= dialogueLines.Length)
        {
            Debug.Log("Dialog selesai, panel akan ditutup.");
            CloseDialogue();
            return;
        }

        ShowDialogue();
    }

    private void ShowDialogue()
    {
        if (dialogueLines == null ||
            currentLine < 0 ||
            currentLine >= dialogueLines.Length)
        {
            Debug.LogError("Indeks dialog tidak valid!");
            return;
        }

        DialogueLine line = dialogueLines[currentLine];

        Debug.Log(
            $"Menampilkan dialog indeks {currentLine}: {line.dialogue}"
        );

        nameText.text = line.characterName;
        dialogueText.text = line.dialogue;

        Debug.Log("Teks UI saat ini: " + dialogueText.text);
        Debug.Log("Dialogue Text aktif: " + dialogueText.gameObject.activeInHierarchy);
        Debug.Log("Dialogue Text enabled: " + dialogueText.enabled);

        if (line.characterSprite != null)
        {
            characterImage.sprite = line.characterSprite;
            characterImage.enabled = true;
        }
        else
        {
            characterImage.enabled = false;
        }

        TMP_Text buttonText =
            nextButton.GetComponentInChildren<TMP_Text>();

        if (buttonText != null)
        {
            buttonText.text =
                currentLine == dialogueLines.Length - 1
                ? "Selesai"
                : "Next";
        }
    }
    public void CloseDialogue()
    {
        isDialogueActive = false;
        introPanel.SetActive(false);
    }
}