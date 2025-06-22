using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public static PlayerShooting Instance;

    [Header("Setup")]
    public GameObject Prefab_Bullet_Player; 
    public Transform shootPoint; 
    public float baseDamage  = 10f;
    public float spreadAngle = 15f;

    // internal state
    private bool  _pierce;
    private bool  _triple;
    private float _damageMult = 1f;

    void Awake()
    {
        Instance = this;
    }

    public void Shoot()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length == 0) return;
        
        Transform nearest = enemies[0].transform;
        float minDist = Vector2.Distance(shootPoint.position, nearest.position);

        for (int i = 1; i < enemies.Length; i++)
        {
            float d = Vector2.Distance(shootPoint.position, enemies[i].transform.position);
            if (d < minDist)
            {
                minDist = d;
                nearest = enemies[i].transform;
            }
        }
        Vector2 baseDir = ((Vector2)nearest.position - (Vector2)shootPoint.position).normalized;
        if (_triple)
        {
            SpawnAtDirection(Rotate(baseDir, -spreadAngle));
            SpawnAtDirection(baseDir);
            SpawnAtDirection(Rotate(baseDir, +spreadAngle));
        }
        else
        {
            SpawnAtDirection(baseDir);
        }
    }
    private void SpawnAtDirection(Vector2 dir)
    {
        var b = Instantiate(Prefab_Bullet_Player, shootPoint.position, Quaternion.identity);
        var sb = b.GetComponent<StraightBullet>();
        sb.Initialize(baseDamage * _damageMult, _pierce, dir);
    }
    
    private Vector2 Rotate(Vector2 v, float deg)
    {
        float rad = deg * Mathf.Deg2Rad;
        float ca  = Mathf.Cos(rad);
        float sa  = Mathf.Sin(rad);
        return new Vector2(v.x * ca - v.y * sa,
            v.x * sa + v.y * ca);
    }


    public void ApplyUpgrade(UpgradeOptionSO.UpgradeType type, float value = 0f)
    {
        switch (type)
        {
            case UpgradeOptionSO.UpgradeType.Pierce:
                _pierce = true;
                break;
            case UpgradeOptionSO.UpgradeType.TripleShot:
                _triple = true;
                break;
            case UpgradeOptionSO.UpgradeType.DamageUp:
                _damageMult += value;
                break;
        }
    }
}
