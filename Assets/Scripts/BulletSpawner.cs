using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public float spawnInterval = 1f;
    float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            PlayerShooting.Instance.Shoot();
            timer = 0f;
        }
    }
}