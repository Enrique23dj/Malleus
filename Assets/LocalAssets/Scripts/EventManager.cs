using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EventManager : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject eventButton;
    [SerializeField] private Image portraitImage;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text bodyText;

    private DialogueData current;
    private int index;

    public void StartDialogue(DialogueData data)
    {
        if (data == null || data.lines.Length == 0) return;

        current = data;
        index = 0;
        panel.SetActive(true);
        ShowLine();
    }

    public void Next()
    {
        index++;

        if (index >= current.lines.Length)
        {
            EndDialogue();
            return;
        }

        ShowLine();
    }

    private void ShowLine()
    {
        DialogueLine line = current.lines[index];

        nameText.text = line.speakerName;
        bodyText.text = line.text;

        portraitImage.sprite = line.portrait;
        portraitImage.enabled = line.portrait != null; 
    }

    private void EndDialogue()
    {
        panel.SetActive(false);
        current = null;

    }

}
