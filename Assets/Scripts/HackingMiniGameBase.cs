using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public abstract class HackingMiniGameBase : MonoBehaviour
{
    public UnityEvent onStart;
    public UnityEvent onFinished;

    public List<Func<bool>> objectives = new List<Func<bool>>();


    public virtual void StartMiniGame()
    {
        onStart?.Invoke();
    }

    public abstract void UpdateMiniGame();

    private void Update()
    {
        UpdateMiniGame();
    }

    public virtual void FinishMiniGame()
    {
        onFinished?.Invoke();
    }
}