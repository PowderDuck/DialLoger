using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueObject : MonoBehaviour, Syncable<DialogueSegment>
{
    public TextMeshProUGUI SpeakerNameText = null;
    public TextMeshProUGUI ContentText = null;

    [SerializeField] private OptionObject optionPrefab = null;
    [SerializeField] private Vector2 optionStartingPosition = new Vector2(210f, -60f);
    [SerializeField] private Vector2 optionOffset = new Vector2(-70f, 0f);
    [SerializeField] private int maxOptions = 7;
    
    [SerializeField] private List<OptionObject> options = new List<OptionObject>();
    [SerializeField] private GameObject nextButton = null;

    public void Sync(DialogueSegment segment)
    {
        ContentText.text = segment.Content;
        List<DialogueOption> segmentOptions = segment.Options;

        for (int i = 0; i < maxOptions; i++)
        {
            if(i < segmentOptions.Count)
            {
                options[i].Sync(segmentOptions[i]);
            }

            options[i].gameObject.SetActive(i < segmentOptions.Count);
        }

        nextButton.SetActive(segmentOptions.Count <= 0f);
    }
}
