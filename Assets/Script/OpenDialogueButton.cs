using UnityEngine;

public class OpenDialogueButton : MonoBehaviour
{
    [Header("Dialogue Reference")]
    public IntroDialogue dialogueManager;

    public void OpenDialogue()
    {
        if (dialogueManager == null)
        {
            Debug.LogWarning(
                "Dialogue Manager belum dihubungkan!"
            );
            return;
        }

        dialogueManager.OpenDialogue();
    }
}