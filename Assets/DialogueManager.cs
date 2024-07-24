using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;


    private Dialogue currentDialogue = null;
    private DialogueObject dialogueSkeleton = null;
    public Dialogue CurrentDialogue { get => currentDialogue; }

    public void Awake()
    {
        Instance = Instance == null ? this : throw new System.Exception("[!] DialogueManager Already Exists.");

        dialogueSkeleton = FindObjectOfType<DialogueObject>();
    }

    public void InitiateDialogue(Dialogue dialogue)
    {
        currentDialogue = dialogue;
        dialogueSkeleton.Sync(CurrentDialogue.CurrentSegment);
    }

    public void Continue()
    {
        if(!currentDialogue.CanContinue())
        {
            dialogueSkeleton.gameObject.SetActive(false);
            return;
        }

        Debug.Log(currentDialogue.CanContinue());
        currentDialogue.Continue();
        dialogueSkeleton.Sync(CurrentDialogue.CurrentSegment);
    }

    public static Dialogue GetDialogueInstance(Dialogue original)
    {
        Dialogue instance = ScriptableObject.CreateInstance<Dialogue>();
        for (int i = 0; i < original.segments.Count; i++)
        {
            DialogueSegment currentSegment = original.segments[i];
            instance.segments.Add(currentSegment);
            for (int o = 0; o < currentSegment.Options.Count; o++)
            {
                instance.segments[instance.segments.Count - 1].Options[o].optionID = o;
            }
        }

        return instance;
    }
}
