using System.Collections;
using UnityEngine;

public class WaveEndChecker : MonoBehaviour
{
    public static WaveEndChecker Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void StartChecking() => StartCoroutine(CheckForAliveEnemies());

    private IEnumerator CheckForAliveEnemies()
    {
        while (GameManager.Instance.AliveEnemies.Count != 0)
        {
            yield return new WaitForSeconds(0.6f);
        }
        GameManager.Instance.OnWaveEnded();
    }
}
