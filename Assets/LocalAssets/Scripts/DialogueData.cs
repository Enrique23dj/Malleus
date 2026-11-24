using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

[System.Serializable]
public class DialogueChoice
{
    public string text;
    [Tooltip("Número de línea (Element) a la que salta. -1 = pasar a la siguiente.")]
    public int nextLineIndex = -1;
}

[System.Serializable]
public class DialogueLine
{
    public string speakerName;
    public Sprite portrait;
    [TextArea(2, 5)] public string text;

    [Header("Opciones (opcional)")]
    public bool hasChoices;
    public DialogueChoice[] choices; // pon 2

    [Header("Final de rama (opcional)")]
    [Tooltip("Si está marcada, el diálogo termina tras esta línea.")]
    public bool endsDialogue;
}

[CreateAssetMenu(fileName = "NuevoDialogo", menuName = "Dialogo/Conversacion")]
public class DialogueData : ScriptableObject
{
    public DialogueLine[] lines;
}