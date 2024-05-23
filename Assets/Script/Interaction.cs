using UnityEngine;
using TMPro;

public class Interaction : MonoBehaviour
{
    [Header("Object Assign")]
    public GameObject buttonPrefab;
    public Transform buttonParent;
    public GameObject dialogueCanvas;
    public TextMeshProUGUI dialogueTextMeshPro;
    public TextMeshProUGUI npcNameTextMeshPro;
    public KeyCode interactKey = KeyCode.E;
    public DialogueTrigger DialogueTrigger;

    private GameObject interactionButton;
    private GameObject currentNPC; // Track the current NPC being interacted with
    private bool dialogueActive; // Flag to track if dialogue is active

    private int currentDialogueIndex; // Current index of the displayed dialogue line

    void Start()
    {
        if (buttonPrefab == null || buttonParent == null)
        {
            Debug.LogError("Button prefab or button parent is not set in Interaction script!");
            return;
        }

        // Instantiate the button prefab and deactivate it initially
        interactionButton = Instantiate(buttonPrefab, buttonParent);
        interactionButton.SetActive(false);

        dialogueActive = false; // Initialize dialogueActive flag

        if (dialogueCanvas != null)
        {
            dialogueCanvas.SetActive(false); // Deactivate the dialogue canvas initially
        }
    }

    void Update()
    {
        // Check if player presses the interact key
        if (Input.GetKeyDown(interactKey))
        {
            if (!dialogueActive && currentNPC != null)
            {
                OpenDialogue();
            }
            else
            {
                NextDialogue();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            currentNPC = other.gameObject;
            interactionButton.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            currentNPC = null;
            interactionButton.SetActive(false);
            CloseDialogue();
        }
    }

    void OpenDialogue()
    {
        DialogueTrigger npcDialogue = DialogueTrigger;
        if (npcDialogue != null && npcDialogue.dialogueLines.Length > 0)
        {
            dialogueActive = true;
            npcNameTextMeshPro.text = "- " + npcDialogue.npcName + " -";
            dialogueTextMeshPro.text = npcDialogue.dialogueLines[currentDialogueIndex];
            dialogueCanvas.SetActive(true); // Activate the dialogue canvas
        }
    }

    void NextDialogue()
    {
        DialogueTrigger npcDialogue = DialogueTrigger;
        if (dialogueActive && npcDialogue != null && npcDialogue.dialogueLines.Length > 0)
        {
            if (currentDialogueIndex < npcDialogue.dialogueLines.Length - 1)
            {
                currentDialogueIndex++;
                dialogueTextMeshPro.text = npcDialogue.dialogueLines[currentDialogueIndex];
            }
            else
            {
                CloseDialogue();
            }
        }
    }

    void CloseDialogue()
    {
        dialogueActive = false;
        npcNameTextMeshPro.text = "";
        dialogueTextMeshPro.text = "";
        currentDialogueIndex = 0;
        dialogueCanvas.SetActive(false); // Deactivate the dialogue canvas
    }

}
