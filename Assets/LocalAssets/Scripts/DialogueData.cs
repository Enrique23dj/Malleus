using UnityEngine;

[System.Serializable]
public class DialogueLine
{
    public string speakerName;
    public Sprite portrait;
    [TextArea(2, 5)] public string text;
}

[CreateAssetMenu(fileName = "NuevoDialogo", menuName = "Dialogo/Conversacion")]
public class DialogueData : ScriptableObject
{
    public DialogueLine[] lines;
}