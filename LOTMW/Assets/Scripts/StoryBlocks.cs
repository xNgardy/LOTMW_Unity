using UnityEngine;

[CreateAssetMenu(menuName = "Story Block")]

public class StoryBlocks : ScriptableObject
{
    [TextArea(10, 14)] [SerializeField] private string storyText;
    [SerializeField] private StoryBlocks[] nextStoryBlocks;

    public string GetStoryText()
    {
        return storyText;
    }

    public StoryBlocks[] GetStoryBlocks()
    {
        return nextStoryBlocks;
    }
}
