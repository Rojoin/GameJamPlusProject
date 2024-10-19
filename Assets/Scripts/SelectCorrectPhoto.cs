using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ImageData
{
    public Sprite sprite;
    public bool isCorrect;
}

public class SelectCorrectPhoto : HackingMiniGameBase
{
    public List<Sprite> garbageSprites = new List<Sprite>();
    public List<Sprite> correctSprites = new List<Sprite>();
    private List<ImageData> spriteToBeUsed = new List<ImageData>();

    public List<Button> buttons;
    public List<Button> activeButtons = new List<Button>();

    public GameObject gameContainer;

    public int gridHeight = 3;
    public int gridWidth = 3;

    public string minigameDescription;

    [ContextMenu("Generate Games")]
    public override void StartMiniGame()
    {
        base.StartMiniGame();

        garbageSprites = garbageSprites.OrderBy(x => Random.value).ToList();
        correctSprites = correctSprites.OrderBy(x => Random.value).ToList();

        for (var index = 0; index < buttons.Count; index++)
        {
            var button = buttons[index];
            bool isEven = index % 2 == 0;

            ImageData imageData = new ImageData();
            imageData.isCorrect = !isEven;
            imageData.sprite = isEven ? garbageSprites[index] : correctSprites[index];

            spriteToBeUsed.Add(imageData);
        }

        spriteToBeUsed = spriteToBeUsed.OrderBy(x => Random.value).ToList();

        for (var index = 0; index < buttons.Count; index++)
        {
            var button = buttons[index];
            bool isEven = index % 2 == 0;
            
            button.GetComponent<Image>().sprite = spriteToBeUsed[index].sprite;
            if (spriteToBeUsed[index].isCorrect)
            {
                button.onClick.AddListener(() => OnCompleteIndividualObjective(button));
                activeButtons.Add(button);
            }
        }
    }

    private void OnCompleteIndividualObjective(Button button)
    {
        button.onClick.RemoveListener(() => OnCompleteIndividualObjective(button));
        activeButtons.Remove(button);
        if (activeButtons.Count == 0)
        {
            FinishMiniGame();
        }
    }

    public override void UpdateMiniGame()
    {
    }

    public override void FinishMiniGame()
    {
        base.FinishMiniGame();
        Debug.Log("You win");
    }
}