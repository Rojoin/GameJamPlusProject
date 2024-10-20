using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class GamblingMinigame : HackingMiniGameBase
{
    public RectTransform slotRectTransform;
    public float minPosY = -20f;
    public float maxPosY = 840f;
    public float symbolHeight = 120f;
    public float scrollSpeed = 0.1f;
    public float duration = 3f;
    private float timer = 0;
    private bool isScrolling = false;
    private float targetPosY;
    private int totalSymbols = 8;
    public int maxCountUntilSecure = 3;
    private int pullCount = 0;


    [ContextMenu("Start Minigame")]
    public override void StartMiniGame()
    {
        base.StartMiniGame();
        StartScrolling();
    }

    public override void UpdateMiniGame()
    {
        if (isScrolling)
        {
            timer += Time.deltaTime;

            if (timer < duration)
            {
                slotRectTransform.anchoredPosition += new Vector2(0, scrollSpeed * Time.deltaTime);

                if (slotRectTransform.anchoredPosition.y >= maxPosY)
                {
                    slotRectTransform.anchoredPosition = new Vector2(slotRectTransform.anchoredPosition.x,
                        minPosY + (slotRectTransform.anchoredPosition.y - maxPosY));
                }
            }
            else
            {
                slotRectTransform.anchoredPosition = new Vector2(slotRectTransform.anchoredPosition.x, targetPosY);
                isScrolling = false;
            }
        }
    }

    public void StartScrolling()
    {
        isScrolling = true;
        timer = 0;

        int randomSymbolIndex = Random.Range(0, totalSymbols);

        if (randomSymbolIndex == totalSymbols)
        {
            randomSymbolIndex = 0;
        }

        targetPosY = minPosY + randomSymbolIndex * symbolHeight;
    }
}