using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HomingBullet : MonoBehaviour
{
    [HideInInspector] public float damage;
    [HideInInspector] public bool  canPierce;
    [HideInInspector] public bool  isHoming = true;  // ← thêm flag

    public float speed       = 5f;
    public float rotateSpeed = 200f;

    private Transform target;

    void Start()
    {
        // nếu homing thì tìm target
        if (isHoming)
            AssignRandomTarget();

        Destroy(gameObject, 5f);
    }

    void Update()
    {
        // nếu homing và có target
        if (isHoming)
        {
            if (target == null)
            {
                AssignRandomTarget();
                if (target == null) return;
            }

            // --- homing rotation ---
            Vector2 dir = (target.position - transform.position).normalized;
            float currentAngle = transform.eulerAngles.z;
            float targetAngle  = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;
            float angle = Mathf.MoveTowardsAngle(currentAngle, targetAngle, rotateSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
        // luôn bay về phía up
        transform.position += transform.up * speed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.CompareTag("Enemy"))
        {
            c.GetComponent<Enemy>()?.TakeDamage(damage);
            if (!canPierce) Destroy(gameObject);
        }
    }

    public void Initialize(float dmg, bool pierce, bool homing = true)
    {
        damage    = dmg;
        canPierce = pierce;
        isHoming  = homing;
        if (isHoming && target == null)
            AssignRandomTarget();
    }

    private void AssignRandomTarget()
    {
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length == 0) return;
        target = enemies[Random.Range(0, enemies.Length)].transform;
    }
}