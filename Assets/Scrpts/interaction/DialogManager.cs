using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public static DialogManager Instance { get; private set; }

    public GameObject dialoguePanal;
    public TMP_Text dialogueText, nameText;
    public Image portraitImage;
    public Transform choiceConTainer;
    public GameObject choiceButtonPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

    }

    public void ShowDialogueUI(bool show)
    {
        dialoguePanal.SetActive(show);
    }

    public void SetInInfo(string nameS, Sprite protrail)
    {
        nameText.text = nameS;
        portraitImage.sprite = protrail;
    }

    public void SetDialogueText(string text)
    {
        dialogueText.text = text;
    }

    public void ClearChoices()
    {
        foreach (Transform child in choiceConTainer) Destroy(child.gameObject);
    }

    public GameObject CreateChoiceButton(string choiceText, UnityEngine.Events.UnityAction onClick)
    {
        GameObject choiceButton = Instantiate(choiceButtonPrefab, choiceConTainer);
        choiceButton.GetComponentInChildren<TMP_Text>().text = choiceText;
        choiceButton.GetComponent<Button>().onClick.AddListener(onClick);
        return choiceButton;
    }
}
