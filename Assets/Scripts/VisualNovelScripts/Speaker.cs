using UnityEngine;

[CreateAssetMenu(fileName = "NewSpeaker", menuName = "Data/New Speaker")]
[System.Serializable]
public class Speaker : ScriptableObject
{
    // Speakers will be highlighted in a specific colour
    public string speakerName;
    public Color textColor;
}
