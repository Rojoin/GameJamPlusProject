using Autodesk.Fbx;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<Transform> spawnLocations = new();
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObjectChannelSO playerChannel;
    [SerializeField] private VoidChannelSO playerDeathChannel;

    [SerializeField] private float maxTimer = 7.0f;
    [SerializeField] private Transform playerTransform;

    private float currentTimer = 0.0f;

    private void OnEnable()
    {
        playerChannel.Subscribe(AssignPlayer);
        playerDeathChannel.Subscribe(GoToFirstScene);
    }

    private void GoToFirstScene()
    {
        SceneManager.LoadScene(0);
    }

    private void OnDisable()
    {
        playerChannel.Unsubscribe(AssignPlayer);
        playerDeathChannel.Unsubscribe(GoToFirstScene);
    }

    private void Update()
    {
        SpawnTimer();
    }

    private void AssignPlayer(GameObject playerRef)
    {
        playerTransform = playerRef.transform;
    }

    private void SpawnTimer()
    {
        currentTimer += Time.deltaTime;

        if (currentTimer > maxTimer)
        {
            SpawnEnemy();

            currentTimer = 0.0f;
        }
    }

    private void SpawnEnemy()
    {
        int rnd = Random.Range(0, spawnLocations.Count);

        var enemy = Instantiate(enemyPrefab, spawnLocations[rnd].position, spawnLocations[rnd].rotation);
        enemy.GetComponent<Enemy>().SetPlayer(playerTransform);
    }
}