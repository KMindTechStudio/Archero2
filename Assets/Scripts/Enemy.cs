using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 20f;

    public void TakeDamage(float dmg)
    {
        health -= dmg;
        if (health <= 0f)
        {
            FindObjectOfType<GameManager>().OnKill();
            Destroy(gameObject);
        }
    }
}