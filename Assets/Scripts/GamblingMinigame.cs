using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

[ExecuteInEditMode]
public class GamblingMinigame : HackingMiniGameBase
{
    public List<RectTransform> slotRectTransform;
    public float minPosY = -20f;
    public float maxPosY = 840f;
    public float symbolHeight = 120f;
    public float scrollSpeed = 0.1f;
    public float duration = 3f;
    private float timer = 0;
    private bool isScrolling = false;
    private List<float> targetPosY;
    public int totalSymbols = 5;
    public int maxCountUntilSecure = 3;
    private int pullCount = 0;

    private void OnEnable()
    {
        isScrolling = false;
        targetPosY = new List<float>();
    }

    [ContextMenu("Start Minigame")]
    public override void StartMiniGame()
    {
        base.StartMiniGame();
        if (isScrolling)
            return;
        StartScrolling();
    }

    public override void UpdateMiniGame()
    {
        if (isScrolling)
        {
            timer += Time.deltaTime;

            for (var index = 0; index < slotRectTransform.Count; index++)
            {
                SpinRoulette(slotRectTransform[index], targetPosY[index]);
            }

            if (timer > duration)
            {
                isScrolling = false;
                float currentValue = targetPosY[0];
                if (targetPosY.All(x => Mathf.Approximately(x, targetPosY.First())))
                {
                    FinishMiniGame();
                }
            }
        }
    }

    private void SpinRoulette(RectTransform slotRectTransform, float targetPosY)
    {
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
        }
    }

    public override void FinishMiniGame()
    {
        base.FinishMiniGame();
        pullCount = 0;
        Debug.Log("I cant stop winning");
    }

    public void StartScrolling()
    {
        isScrolling = true;
        timer = 0;
        pullCount++;
        targetPosY.Clear();
        if (pullCount < maxCountUntilSecure)
        {
            for (var index = 0; index < slotRectTransform.Count; index++)
            {
                int randomSymbolIndex = RandomSymbolIndex();

                targetPosY.Add(minPosY + randomSymbolIndex * symbolHeight);
            }
        }
        else
        {
            int randomSymbolIndex = RandomSymbolIndex();

            for (var index = 0; index < slotRectTransform.Count; index++)
            {
                targetPosY.Add(minPosY + randomSymbolIndex * symbolHeight);
            }
        }
    }

    private int RandomSymbolIndex()
    {
        int randomSymbolIndex = Random.Range(0, totalSymbols);

        if (randomSymbolIndex == totalSymbols)
        {
            randomSymbolIndex = 0;
        }

        return randomSymbolIndex;
    }
}