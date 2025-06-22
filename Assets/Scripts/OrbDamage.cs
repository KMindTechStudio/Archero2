using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class OrbDamage : MonoBehaviour
{
    public float damage = 1f;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            var e = other.GetComponent<Enemy>();
            if (e != null) e.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}