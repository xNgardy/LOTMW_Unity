using System;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using Random = UnityEngine.Random;

public class GameLogic : MonoBehaviour
{
    [Header("Story Configurations")]
    [SerializeField] private TMP_Text storyText;
    [SerializeField] private TMP_Text option1;
    [SerializeField] private TMP_Text option2;
    [SerializeField] private TMP_Text itemNumber;
    [SerializeField] private TMP_Text dice;
    [SerializeField] private StoryBlocks startingStoryBlock;
    [SerializeField] AudioSource soundPlayer;
    [SerializeField] private GameObject button1;
    [SerializeField] private GameObject button2;
    
    private StoryBlocks _currentStory;
    private bool _waitingForFinalChoice = false;
    private bool _buttonPressed = false;
    private int _itemCount;
    private int _dice;
    private float _timer  = 0;
    private float _duration = 0.25f;
    
    void Start()
    {
        dice.GameObject().SetActive(false);
        _currentStory = startingStoryBlock;
        storyText.text = startingStoryBlock.GetStoryText();
        itemNumber.text = "Current Item Number: " + 0;
        ManageOptions();
        _itemCount = 0;
    }

    private void Update()
    {
        if(_buttonPressed)
        {
            _timer += Time.deltaTime;
            if(_timer >= _duration)
            {
                _timer = 0;
                _buttonPressed = false;
            }
        }
    }

    private void MainLogic(StoryBlocks nextStory)
    {
        _currentStory = nextStory;
        storyText.text = _currentStory.GetStoryText();
        soundPlayer.Play();
        ManageOptions();
    }

    private void ManageOptions()
    {
        button2.SetActive(true);
        
        if (_currentStory.IsGettingItem())
        {
            _itemCount++;
            itemNumber.text = "Current Item Number: " + _itemCount;
        }
        
        if (_currentStory.IsResettingItems())
        {
            itemNumber.text = "Current Item Number: " + _itemCount;
            _itemCount = 0;
            _dice = -1;
        }
        
        if (_currentStory.HasItemRestriction())
        {
            
            if (_itemCount >= 1)
            {
                var next = _currentStory.GetStoryBlocks();
                MainLogic(next[0]);
                
            }
            else
            {
                var next = _currentStory.GetStoryBlocks();
                MainLogic(next[1]);
            }
        }

        if (_currentStory.IsFinalScene())
        {
            _waitingForFinalChoice = true;
            ContinueStory();
        }
        else
        {
            ContinueStory();
        }

    }
    
    private void HandleFinalScene()
    {
        
        var finalBlocks = _currentStory.GetStoryBlocks();

        int endingIndex = -1;
        _dice = Random.Range(0, 5);
        dice.GameObject().SetActive(true);
            
        switch (_itemCount)
        {
            case 0:
                endingIndex = 0;
                break;
            case 1:
                endingIndex = (_dice == 4) ? 2 : 1;
                break;
            case 2:
                endingIndex = (_dice >= 3) ? 4 : 3;
                break;
            case 3:
                endingIndex = (_dice >= 2) ? 6 : 5;
                break;
            case 4:
                endingIndex = 7;
                break;
        }
        
        if (endingIndex >= 0 && endingIndex < finalBlocks.Length)
        {
            dice.text = "Dice: " + _dice;
            MainLogic(finalBlocks[endingIndex]);
            button2.SetActive(true);
        }

    }

    private void ContinueStory()
    {
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
        if (_buttonPressed) return;
        
        if (dice.GameObject().activeInHierarchy)
        {
            dice.GameObject().SetActive(false);
        }
        
        if (_waitingForFinalChoice)
        {
            _waitingForFinalChoice = false;
            HandleFinalScene();
            _buttonPressed = true;
            return;
        }
        
        var next = _currentStory.GetStoryBlocks();
        MainLogic(next[0]);
        _buttonPressed = true;
    }

    public void Option2()
    {
        if (_buttonPressed) return;
        
        if (dice.GameObject().activeInHierarchy)
        {
            dice.GameObject().SetActive(false);
        }
        
        var next = _currentStory.GetStoryBlocks();
        MainLogic(next[1]);
        _buttonPressed = true;
    }
    
    
}
