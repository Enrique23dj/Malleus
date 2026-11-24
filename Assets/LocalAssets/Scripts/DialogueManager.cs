using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [Header("Interfaz")]
    [SerializeField] private GameObject panel;
    [SerializeField] private Image portraitImage;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text bodyText;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button[] choiceButtons; // arrastra aquí los 2 botones

    [Header("Pruebas")]
    [SerializeField] private DialogueData testDialogue;

    private DialogueData current;
    private int index;

    public void StartTestDialogue()
    {
        StartDialogue(testDialogue);
    }

    public void StartDialogue(DialogueData data)
    {
        if (data == null || data.lines.Length == 0) return;

        current = data;
        index = 0;
        panel.SetActive(true);
        ShowLine();
    }

    // Botón Continuar
    public void Next()
    {
        if (current.lines[index].endsDialogue)
        {
            EndDialogue();
            return;
        }

        GoToLine(index + 1);
    }

    private void Choose(DialogueChoice choice)
    {
        int target;

        if (choice.isSkillCheck)
        {
            int roll = Random.Range(0, 100); // entero de 0 a 99
            bool success = roll > choice.successChance;
            target = success ? choice.successLineIndex : choice.failLineIndex;

            Debug.Log($"Tirada: {roll} contra {choice.successChance} → {(success ? "ACIERTO" : "FALLO")}");
        }
        else
        {
            target = choice.nextLineIndex;
        }

        // -1 significa "la siguiente línea"
        GoToLine(target < 0 ? index + 1 : target);
    }

    private void GoToLine(int newIndex)
    {
        if (newIndex < 0 || newIndex >= current.lines.Length)
        {
            EndDialogue();
            return;
        }

        index = newIndex;
        ShowLine();
    }

    private void ShowLine()
    {
        DialogueLine line = current.lines[index];

        nameText.text = line.speakerName;
        bodyText.text = line.text;

        portraitImage.sprite = line.portrait;
        portraitImage.enabled = line.portrait != null;

        bool showChoices = line.hasChoices && line.choices != null && line.choices.Length > 0;

        // Continuar solo se ve si NO hay opciones
        continueButton.gameObject.SetActive(!showChoices);

        for (int i = 0; i < choiceButtons.Length; i++)
        {
            bool active = showChoices && i < line.choices.Length;
            choiceButtons[i].gameObject.SetActive(active);

            if (!active) continue;

            DialogueChoice choice = line.choices[i];
            choiceButtons[i].GetComponentInChildren<TMP_Text>().text = choice.text;

            choiceButtons[i].onClick.RemoveAllListeners();
            choiceButtons[i].onClick.AddListener(() => Choose(choice));
        }
    }

    private void EndDialogue()
    {
        panel.SetActive(false);
        current = null;
    }
}