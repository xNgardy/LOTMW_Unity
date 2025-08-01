using UnityEngine;

[CreateAssetMenu(menuName = "Story Block")]

public class StoryBlocks : ScriptableObject
{
    [TextArea(10, 14)] [SerializeField] private string storyText;
    [SerializeField] private StoryBlocks[] nextStoryBlocks;
    [SerializeField] private string[] options;
    
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
    
}
