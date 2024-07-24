using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OptionObject : MonoBehaviour, Syncable<DialogueOption>
{
    public TextMeshProUGUI OptionContent = null;
    private List<Dialogue.OptionCallback> AnswerCallbacks = new List<Dialogue.OptionCallback>();
    private int OptionID = 0;

    public void Sync(DialogueOption option)
    {
        OptionContent.text = option.Answer;
        AnswerCallbacks = option.AnswerCallbacks;
        OptionID = option.optionID;
    }

    public void SelectAnswer()
    {
        /*DialogueManager.Instance.CurrentDialogue.InsertSegment(
            new DialogueSegment() { Content = OptionContent.text } );

        for (int i = 0; i < AnswerCallbacks.Count; i++)
        {
            DialogueSegment answer = AnswerCallbacks[i](OptionContent.text, OptionID);
            if(answer != null)
                DialogueManager.Instance.CurrentDialogue.InsertSegment(answer, i + 1);
        }*/

        bool isNew = false;
        for (int i = 0; i < AnswerCallbacks.Count + 1; i++)
        {
            DialogueSegment answer = new DialogueSegment() { Content = OptionContent.text };
            if (i > 0)
            {
                answer = AnswerCallbacks[i - 1](OptionContent.text, OptionID);
                isNew = !isNew ? answer == null : isNew;
            }

            if (answer != null)
            {
                DialogueManager.Instance.CurrentDialogue.InsertSegment(answer, i);
            }
        }

        if(!isNew)
            DialogueManager.Instance.Continue();
    }
}
