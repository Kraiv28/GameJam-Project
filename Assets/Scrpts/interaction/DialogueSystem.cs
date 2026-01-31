using UnityEngine;

[CreateAssetMenu(fileName = "NewDialogue", menuName = "Dialogue")]
public class DialogueSystem : ScriptableObject
{
    public string nameS;
    public Sprite portrait;
    public string[] dialogueLines;
    public float typingSpeed = 0.05f;
    public AudioClip typinSound;
    public float voicePitch = 1f;
    public bool[] autoProgressLines;
    public float autoProgressDisplay = 1.5f;
}
