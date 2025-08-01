using UnityEngine;

[CreateAssetMenu(menuName = "Story Block")]

public class StoryBlocks : ScriptableObject
{
    [TextArea(10, 14)] [SerializeField] private string storyText;
    [SerializeField] private StoryBlocks[] nextStoryBlocks;
    [SerializeField] private string[] options;
    [SerializeField] private bool hasItem = false;
    [SerializeField] private bool resetItems;
    [SerializeField] private bool hasRestriction;
    [SerializeField] private bool finalScene;
    
    public string GetStoryText()
    {
        return storyText;
    }

    public StoryBlocks[] GetStoryBlocks()
    {
        return nextStoryBlocks;
    }

    public string[] GetOptions()
    {
        return options;
    }

    public bool IsGettingItem()
    {
        return hasItem;
    }

    public bool IsResettingItems()
    {
        return resetItems;
    }

    public bool HasItemRestriction()
    {
        return hasRestriction;
    }

    public bool IsFinalScene()
    {
        return finalScene;
    }
    
}
