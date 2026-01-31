using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Box : MonoBehaviour, IInteraction
{
    public DialogueSystem dialogueData;
    private DialogManager dialoggueUI;
    private int dialogueIndex;
    private bool isTyping, isDialogueActive;


    private void Start()
    {
        dialoggueUI = DialogManager.Instance;
    }
    public bool CanInteract()
    {
        return !isDialogueActive;
    }

    public void Interact()
    {
        if (dialogueData == null)
            return;

        if (isDialogueActive)
        {
            NextLine();
        }
        else
        {
            StartDialogue();
        }
    }

    void StartDialogue()
    {
        isDialogueActive = true;
        dialogueIndex = 0;

        dialoggueUI.SetInInfo(dialogueData.nameS, dialogueData.portrait);
        dialoggueUI.ShowDialogueUI(true);

        DisplayCurrentLine();

    }

    void NextLine()
    {
        if (isTyping)
        {
            StopAllCoroutines();
            dialoggueUI.SetDialogueText(dialogueData.dialogueLines[dialogueIndex]);
            isTyping = false;
            return;
        }

        // 1. ตรวจสอบว่าบรรทัดปัจจุบัน มี Choice ที่ต้องแสดงไหม
        // เราจะเช็คว่ามี DialogueChoice อันไหนที่มี Index ตรงกับ dialogueIndex ปัจจุบัน
        foreach (DialogueChoice choice in dialogueData.choices)
        {
            // หากใน Inspector ตั้ง Dailogue Index ไว้ตรงกับบรรทัดที่เพิ่งอ่านจบ
            if (choice.dailogueIndex == dialogueIndex)
            {
                dialoggueUI.ClearChoices();
                DisplayChoices(choice);
                return; // หยุดทำงานตรงนี้ เพื่อให้ผู้เล่นเลือก Choice (ไม่ไปบรรทัดถัดไป)
            }
        }

        // 2. เช็คเงื่อนไขจบการสนทนา (End Flag)
        if (dialogueData.endDialogueLines.Length > dialogueIndex && dialogueData.endDialogueLines[dialogueIndex])
        {
            EndDialogue();
            return;
        }

        // 3. ถ้าไม่มี Choice และยังไม่จบ ให้ไปบรรทัดถัดไป
        if (dialogueIndex + 1 < dialogueData.dialogueLines.Length)
        {
            dialogueIndex++;
            DisplayCurrentLine();
        }
        else
        {
            EndDialogue();
        }
    }

    IEnumerator TypeLine()
    {
        isTyping = true;
        dialoggueUI.SetDialogueText("");

        // ใช้ตัวแปรช่วยเพื่อเลี่ยงปัญหาตัวเลข (ที่เคยเจอตอนแรก)
        string fullText = dialogueData.dialogueLines[dialogueIndex];
        string currentText = "";

        foreach (char letter in fullText)
        {
            currentText += letter;
            dialoggueUI.SetDialogueText(currentText);
            yield return new WaitForSeconds(dialogueData.typingSpeed);
        }

        isTyping = false;

        // เช็ค Auto Progress: ถ้าบรรทัดนี้ตั้งค่าให้ข้ามเอง มันจะเรียก NextLine() ทันที
        // ให้แน่ใจว่าใน Inspector คุณไม่ได้ติ๊กถูก Auto Progress ไว้ใน Element สุดท้าย
        if (dialogueData.autoProgressLines.Length > dialogueIndex && dialogueData.autoProgressLines[dialogueIndex])
        {
            yield return new WaitForSeconds(dialogueData.autoProgressDisplay);
            NextLine();
        }
    }

    void DisplayChoices(DialogueChoice choice)
    {
        for (int i = 0; i < choice.choices.Length; i++)
        {
            int nextIndex = choice.nexDialogueIndexs[i];
            dialoggueUI.CreateChoiceButton(choice.choices[i], () => ChooseOption(nextIndex));
        }
    }

    void ChooseOption(int nextIndex)
    {
        dialogueIndex = nextIndex;
        dialoggueUI.ClearChoices();
        DisplayCurrentLine();
    }

    void DisplayCurrentLine()
    {
        StopAllCoroutines();
        StartCoroutine(TypeLine());
    }

    public void EndDialogue()
    {
        StopAllCoroutines();
        isDialogueActive = false;
        dialoggueUI.SetDialogueText("");
        dialoggueUI.ShowDialogueUI(false);

    }
}
