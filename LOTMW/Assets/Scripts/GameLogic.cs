using UnityEngine;
using TMPro;

public class GameLogic : MonoBehaviour
{
    [SerializeField] private TMP_Text storyText;
    [SerializeField] private TMP_Text option1;
    [SerializeField] private TMP_Text option2;
    [SerializeField] private StoryBlocks startingStoryBlock;
    [SerializeField] AudioSource soundPlayer;
    [SerializeField] private GameObject button1;
    [SerializeField] private GameObject button2;
    
    private StoryBlocks _currentStory;
    private int _optionLength;
    void Start()
    {
        _currentStory = startingStoryBlock;
        _optionLength = _currentStory.GetOptions().Length;
        storyText.text = startingStoryBlock.GetStoryText();
        ManageOptions();
    }

    void Update()
    {
        storyText.text = _currentStory.GetStoryText();
    }

    private void MainLogic(StoryBlocks[] nextStories, int index)
    {
        _currentStory = nextStories[index];
        soundPlayer.Play();
        ManageOptions();
    }

    private void ManageOptions()
    {
        button2.SetActive(true);
        
        if (_currentStory.GetOptions().Length == 1)
        {
            button2.SetActive(false);
            option1.text = _currentStory.GetOptions()[0];
        }
        else if (_currentStory.GetOptions().Length == 2)
        {
            option1.text = _currentStory.GetOptions()[0];
            option2.text = _currentStory.GetOptions()[1];
        }

    }

    public void Option1()
    {
        var next = _currentStory.GetStoryBlocks();
        MainLogic(next, 0);
    }

    public void Option2()
    {
        var next = _currentStory.GetStoryBlocks();
        MainLogic(next, 1);
    }
}
