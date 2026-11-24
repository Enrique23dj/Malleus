using UnityEngine;

[System.Serializable]
public class DialogueChoice
{
    public string text;

    [Tooltip("Línea (Element) a la que salta. -1 = la siguiente. Se ignora si es una tirada.")]
    public int nextLineIndex = -1;

    [Header("Tirada de habilidad (opcional)")]
    public bool isSkillCheck;

    [Range(0, 100)]
    [Tooltip("Probabilidad de acierto en %")]
    public float successChance = 50f;

    [Tooltip("Línea (Element) si aciertas")]
    public int successLineIndex = -1;

    [Tooltip("Línea (Element) si fallas")]
    public int failLineIndex = -1;
}

[System.Serializable]
public class DialogueLine
{
    [HideInInspector] public string name; // título del elemento (automático), debe ir PRIMERO

    [Tooltip("Solo para ti: aparece en el título del elemento en el editor. No se muestra en el juego.")]
    public string editorNote;

    public string speakerName;
    public Sprite portrait;
    [TextArea(2, 5)] public string text;

    [Header("Opciones (opcional)")]
    public bool hasChoices;
    public DialogueChoice[] choices;

    [Header("Final de rama (opcional)")]
    [Tooltip("Si está marcada, el diálogo termina tras esta línea.")]
    public bool endsDialogue;
}

[CreateAssetMenu(fileName = "NuevoDialogo", menuName = "Dialogo/Conversacion")]
public class DialogueData : ScriptableObject
{
    public DialogueLine[] lines;

    private void OnValidate()
    {
        if (lines == null) return;

        for (int i = 0; i < lines.Length; i++)
        {
            string who = string.IsNullOrEmpty(lines[i].speakerName) ? "Sin nombre" : lines[i].speakerName;
            string title = $"{who} ({i})";

            if (!string.IsNullOrWhiteSpace(lines[i].editorNote))
                title += $" ({lines[i].editorNote})";

            lines[i].name = title;
        }
    }
}