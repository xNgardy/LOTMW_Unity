using UnityEngine;
using TMPro;

public class GameLogic : MonoBehaviour
{
    [SerializeField] private TMP_Text storyText;
    [SerializeField] private StoryBlocks startingStoryBlock;
    [SerializeField] AudioSource soundPlayer;
    
    private StoryBlocks _currentStory;
    void Start()
    {
        _currentStory = startingStoryBlock;
        storyText.text = startingStoryBlock.GetStoryText();
    }

    void Update()
    {
        var nextStories = _currentStory.GetStoryBlocks();

        for (int index = 0; index < nextStories.Length; index++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + index) || Input.GetKeyDown(KeyCode.Keypad1 + index))
            {
                _currentStory = nextStories[index];
            }
        }

        storyText.text = _currentStory.GetStoryText();
    }
}
