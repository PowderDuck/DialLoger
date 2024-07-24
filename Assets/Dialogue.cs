using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Dialogue")]
public class Dialogue : ScriptableObject
{
    public delegate void SegmentCallback();
    public delegate DialogueSegment OptionCallback(string answer, int optionID);

    public List<DialogueSegment> segments = new List<DialogueSegment>();
    public DialogueSegment CurrentSegment => segmentIndex < segments.Count ? segments[segmentIndex] : null;

    private int segmentIndex = 0;

    public void Continue()
    {
        if (segmentIndex < segments.Count)
        {
            DialogueSegment currentSegment = segments[segmentIndex];
            for (int i = 0; i < currentSegment.EndedCallbacks.Count; i++)
            {
                currentSegment.EndedCallbacks[i]();
            }

            segmentIndex += 1;
        }
    }

    public void InsertSegment(DialogueSegment segment, int offset = 0)
    {
        segments.Insert(segmentIndex + 1 + offset, segment);
    }

    public bool CanContinue() => segmentIndex + 1 < segments.Count;
}

[System.Serializable]
public class DialogueSegment
{
    public string Content = "";
    public List<DialogueOption> Options = new List<DialogueOption>();
    public List<Dialogue.SegmentCallback> EndedCallbacks = new List<Dialogue.SegmentCallback>();
}

[System.Serializable]
public class DialogueOption
{
    public string Answer = "";
    [HideInInspector] public int optionID = 0;
    public List<Dialogue.OptionCallback> AnswerCallbacks = new List<Dialogue.OptionCallback>();
}