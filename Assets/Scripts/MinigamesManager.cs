using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class MinigamesManager : MonoBehaviour
{
    public float timeUntilNextMinigame = 20f;
    private float timer = 0;

    public int maxMinigames = 3;
    private int minigamesActive = 0;

    public List<HackingMiniGameBase> minigames = new List<HackingMiniGameBase>();
    private List<HackingMiniGameBase> activeMinigames = new List<HackingMiniGameBase>();
    public List<RectTransform> possibleSpawnPoints;

    public List<BoolChannelSO> limiterChannel;
    public BoolChannelSO toggleHudInteratability;
    public FloatChannelSO healthChannel;
    public Image healthSprite;
    private CanvasGroup canvasGroup;
    private List<int> positionsTaken = new List<int>();

    public void OnEnable()
    {
        canvasGroup = gameObject.GetComponent<CanvasGroup>();
        healthChannel.Subscribe(ChangeHealthBar);
        toggleHudInteratability.Subscribe(ToggleHudInteratability);
    }

    private void ChangeHealthBar(float obj)
    {
        healthSprite.fillAmount = obj;
    }

    private void OnDisable()
    {
        toggleHudInteratability.Unsubscribe(ToggleHudInteratability);
    }

    private void ToggleHudInteratability(bool value)
    {
        canvasGroup.interactable = value;
    }


    public void Update()
    {
        if (minigamesActive < maxMinigames)
        {
            timer += Time.deltaTime;

            if (timer > timeUntilNextMinigame)
            {
                timer = 0;
                Transform parent = GetRandomPosition(out var range);
                HackingMiniGameBase currentGame =
                    Instantiate(minigames[Random.Range(0, minigames.Count)], parent);
                limiterChannel[minigamesActive].RaiseEvent(false);
                minigamesActive++;
                activeMinigames.Add(currentGame);
                currentGame.onFinished.AddListener(() => DestroyMinigame(currentGame, range));

                currentGame.StartMiniGame();
            }
        }
    }

    private Transform GetRandomPosition(out int range)
    {
        do
        {
            range = Random.Range(0, possibleSpawnPoints.Count);
        } while (positionsTaken.Contains(range));

        positionsTaken.Add(range);
        return possibleSpawnPoints[range];
    }


    private void DestroyMinigame(HackingMiniGameBase minigame, int parent)
    {
        minigame.onFinished.RemoveAllListeners();
        minigamesActive--;
        limiterChannel[minigamesActive].RaiseEvent(true);
        positionsTaken.Remove(parent);
        Destroy(minigame.gameObject);
    }
}