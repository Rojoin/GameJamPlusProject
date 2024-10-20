using System;
using System.Collections.Generic;
using UnityEngine;
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
    private CanvasGroup canvasGroup;
    private List<int> positionsTaken = new List<int>();

    public void OnEnable()
    {
        canvasGroup = gameObject.GetComponent<CanvasGroup>();
        toggleHudInteratability.Subscribe(ToggleHudInteratability);
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
                Transform parent = GetRandomPosition();
                HackingMiniGameBase currentGame =
                    Instantiate(minigames[Random.Range(0, minigames.Count)], parent);
                limiterChannel[minigamesActive].RaiseEvent(false);
                minigamesActive++;
                activeMinigames.Add(currentGame);
                currentGame.onFinished.AddListener(() => DestroyMinigame(currentGame));

                currentGame.StartMiniGame();
            }
        }
    }

    private Transform GetRandomPosition()
    {
        int range;
        do
        {
            range = Random.Range(0, possibleSpawnPoints.Count);
        } while (positionsTaken.Contains(range));

        positionsTaken.Add(range);
        return possibleSpawnPoints[range];
    }


    private void DestroyMinigame(HackingMiniGameBase minigame)
    {
        minigame.onFinished.RemoveAllListeners();
        minigamesActive--;
        limiterChannel[minigamesActive].RaiseEvent(true);

        Destroy(minigame.gameObject);
    }
}