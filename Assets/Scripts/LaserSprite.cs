using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(BoxCollider2D))]
public class LaserSprite : MonoBehaviour
{
    [Header("References")]
    public Transform shootPoint;       // kéo vào điểm bắn của Player

    [Header("Timing")]
    public float lifeTime   = 1f;      // bật laser trong 1s
    public float offTime    = 1f;      // tắt laser trong 1s

    [Header("Beam Settings")]
    public float maxDistance       = 20f;  // nếu không có Enemy
    public float damagePerSecond   = 10f;  // sát thương mỗi giây

    SpriteRenderer _sr;
    BoxCollider2D  _bc;
    float          _spriteHeight;

    void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
        _bc = GetComponent<BoxCollider2D>();
        _spriteHeight = _sr.sprite.bounds.size.y;
    }

    void OnEnable()
    {
        // khi spawn, bắt đầu chu kỳ bật/tắt
        StartCoroutine(LaserCycle());
    }

    void OnDisable()
    {
        StopAllCoroutines();
    }

    IEnumerator LaserCycle()
    {
        while (true)
        {
            // — BẬT
            _sr.enabled = true;
            _bc.enabled = true;
            UpdateLaserOnce();          // update ngay khi bật
            yield return new WaitForSeconds(lifeTime);

            // — TẮT
            _sr.enabled = false;
            _bc.enabled = false;
            yield return new WaitForSeconds(offTime);
        }
    }

    void Update()
    {
        // mỗi frame nếu đang bật thì update lại hướng & độ dài
        if (_sr.enabled)
            UpdateLaserOnce();
    }

    void UpdateLaserOnce()
    {
        // 1) Đặt đúng vị trí
        transform.position = shootPoint.position;

        // 2) Tìm Enemy gần nhất
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float dist = maxDistance;
        Transform nearest = null;
        if (enemies.Length > 0)
        {
            nearest = enemies[0].transform;
            dist = Vector2.Distance(shootPoint.position, nearest.position);
            for (int i = 1; i < enemies.Length; i++)
            {
                float d = Vector2.Distance(shootPoint.position, enemies[i].transform.position);
                if (d < dist)
                {
                    dist = d;
                    nearest = enemies[i].transform;
                }
            }

            // 3) Quay về hướng nearest
            Vector2 dir = ((Vector2)nearest.position - (Vector2)shootPoint.position).normalized;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }

        // 4) Kéo dài sprite theo chiều Y
        float scaleY = dist / _spriteHeight;
        transform.localScale = new Vector3(
            transform.localScale.x,
            scaleY,
            transform.localScale.z
        );

        // 5) Điều chỉnh collider cho đúng
        _bc.size   = new Vector2(_bc.size.x, dist);
        _bc.offset = new Vector2(0f, dist / 2f);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Trừ máu theo giây
            other.GetComponent<Enemy>()?.TakeDamage(damagePerSecond * Time.deltaTime);
        }
    }
}
