using System.Collections;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public static WaveSpawner Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void StartSpawning()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        Wave wave = GameManager.Instance.CurrentWave;
        if (wave == null)
        {
            Debug.Log("Can not spawn Enemies. Game might already be over or no level is loaded.");
            yield break;
        }

        for (int i = 0; i < wave.enemies.Length; i++)
        {
            for (int j = 0; j < wave.enemies[i].amount; j++)
            {
                GameManager.Instance.SpawnPoints[wave.enemies[i].spawnPoint].Spawn(wave.enemies[i].enemyData);
                yield return new WaitForSeconds(wave.enemies[i].timePerSpawn);
            }
        }
    }
}
