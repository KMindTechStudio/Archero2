using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Enemy & Spawn Settings")]
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;
    public int killsToUpgrade = 3;

    private int killCount = 0;

    void Start()
    {
        SpawnEnemy();
    }

    // Gọi từ Enemy.TakeDamage khi enemy chết
    public void OnKill()
    {
        killCount++;
        if (killCount >= killsToUpgrade)
        {
            killCount = 0;
            // Pause cả game
            Time.timeScale = 0f;
            // Mở UI chọn upgrade
            FindObjectOfType<UpgradeManager>().ShowChoices();
        }
        else
        {
            // Spawn ngay nếu chưa đủ số kill cần upgrade
            SpawnEnemy();
        }
    }

    // Spawn enemy ngay lập tức
    public void SpawnEnemy()
    {
        if (spawnPoints.Length == 0 || enemyPrefab == null) return;
        int idx = Random.Range(0, spawnPoints.Length);
        Instantiate(enemyPrefab, spawnPoints[idx].position, Quaternion.identity);
    }

    // Gọi sau khi user chọn xong nâng cấp
    public IEnumerator ResumeAndSpawnNext()
    {
        // Đợi 1 giây “thực” bất chấp Time.timeScale = 0
        yield return new WaitForSecondsRealtime(1f);

        // Resume game
        Time.timeScale = 1f;

        // Spawn enemy mới
        SpawnEnemy();
    }
}