using UnityEngine;

public class StraightBullet : MonoBehaviour
{
    [HideInInspector] public float damage;
    [HideInInspector] public bool  canPierce;
    public float speed = 8f;

    private Vector2 _moveDir;

    void Start()
    {
        Destroy(gameObject, 5f);
    }

    void Update()
    {
        transform.position += (Vector3)_moveDir * speed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.CompareTag("Enemy"))
        {
            c.GetComponent<Enemy>()?.TakeDamage(damage);
            if (!canPierce)
                Destroy(gameObject);
        }
    }
    public void Initialize(float dmg, bool pierce, Vector2 direction)
    {
        damage    = dmg;
        canPierce = pierce;
        _moveDir  = direction.normalized;

        // Set rotation để sprite hướng đúng
        float angle = Mathf.Atan2(_moveDir.y, _moveDir.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}