using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public static PlayerShooting Instance;
    public GameObject bulletPrefab;
    public Transform shootPoint;
    public float baseDamage = 10f;

    bool pierce, triple;
    float damageMult = 1f;

    void Awake() => Instance = this;

    public void Shoot()
    {
        if (triple)
        {
            // 2 viên cạnh không homing
            Spawn(-15f, homing: false);
            // viên giữa homing
            Spawn(  0f, homing: true);
            // viên phải không homing
            Spawn( 15f, homing: false);
        }
        else
        {
            // single-shot luôn homing
            Spawn(0f, homing: true);
        }
    }

    void Spawn(float angle, bool homing)
    {
        var b = Instantiate(bulletPrefab, shootPoint.position, Quaternion.Euler(0, 0, angle));
        b.GetComponent<HomingBullet>()
            .Initialize(baseDamage * damageMult, pierce, homing);
    }

    public void ApplyUpgrade(UpgradeOptionSO.UpgradeType type, float value = 0f)
    {
        switch (type)
        {
            case UpgradeOptionSO.UpgradeType.Pierce:
                pierce = true; break;
            case UpgradeOptionSO.UpgradeType.TripleShot:
                triple = true; break;
            case UpgradeOptionSO.UpgradeType.DamageUp:
                damageMult += value; break;
        }
    }
}