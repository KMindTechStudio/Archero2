using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class HomingBullet : MonoBehaviour
{
    [HideInInspector] public float damage;
    [HideInInspector] public bool canPierce;
    public float speed = 5f, rotateSpeed = 200f;

    Rigidbody2D rb; 
    Transform target;

    void Awake() => rb = GetComponent<Rigidbody2D>();

    void Start()
    {
        target = GameObject.FindWithTag("Player")?.transform;
        Destroy(gameObject, 5f);
    }

    void Update()
    {
        if (target == null) return;
        Vector2 dir = (target.position - transform.position).normalized;
        float rotateAmt = Vector3.Cross(dir, transform.up).z;
        rb.angularVelocity = -rotateAmt * rotateSpeed;
        rb.linearVelocity = transform.up * speed;
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.CompareTag("Enemy"))
        {
            c.GetComponent<Enemy>().TakeDamage(damage);
            if (!canPierce) Destroy(gameObject);
        }
    }

    public void Initialize(float dmg, bool pierce)
    {
        damage = dmg;
        canPierce = pierce;
    }
}
