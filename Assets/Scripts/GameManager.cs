using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Enemy & Spawn Settings")]
    public GameObject enemyPrefab;
    public Transform[] EnemyspawnPoints1;  
    public float  delayBetweenWaves = 1f;  

    private int killsThisWave = 0;

    void Start()
    {
        SpawnWave();
    }
    private void SpawnWave()
    {
        killsThisWave = 0;
        foreach (var pt in EnemyspawnPoints1)
        {
            Instantiate(enemyPrefab, pt.position, Quaternion.identity);
        }
    }
    public void OnKill()
    {
        killsThisWave++;
        if (killsThisWave >= EnemyspawnPoints1.Length)
        {
            Time.timeScale = 0f;
            FindObjectOfType<UpgradeManager>().ShowChoices();
        }
    }
    public IEnumerator ResumeAndSpawnNext()
    {
        yield return new WaitForSecondsRealtime(delayBetweenWaves);
        Time.timeScale = 1f;
        SpawnWave();
    }
}