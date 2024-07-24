using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Repeater : MonoBehaviour
{
    [SerializeField] private Dialogue dialogue = null;
    [SerializeField] private List<Dialogue> subDialogues = new List<Dialogue>();

    private void Start()
    {
        dialogue = DialogueManager.GetDialogueInstance(dialogue);

        for (int i = 0; i < subDialogues.Count; i++)
        {
            subDialogues[i] = DialogueManager.GetDialogueInstance(subDialogues[i]);
        }

        /*for (int i = 0; i < dialogue.segments.Count; i++)
        {
            for (int x = 0; x < dialogue.segments[i].Options.Count; x++)
            {
                dialogue.segments[i].Options[x].AnswerCallbacks.Add(PreferenceAnswerCallback);
            }
        }*/

        for (int i = 0; i < dialogue.segments[1].Options.Count; i++)
        {
            dialogue.segments[1].Options[i].AnswerCallbacks.Add(PreferenceAnswerCallback);
        }

        StartConversation();
    }
    public void StartConversation()
    {
        DialogueManager.Instance.InitiateDialogue(dialogue);
    }

    public DialogueSegment PreferenceAnswerCallback(string answer, int optionID)
    {
        DialogueManager.Instance.InitiateDialogue(subDialogues[optionID]);
        return null;
        //return new DialogueSegment() { Content = $"Wow, we have {answer} appreciator here." };
    }
}
