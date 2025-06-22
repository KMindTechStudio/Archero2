using UnityEngine;
using DG.Tweening;

public class OrbsController : MonoBehaviour
{
    public GameObject orbPrefab;
    public int     orbCount = 2;
    public float   radius   = 1.5f;
    public float   orbitDuration = 2f;

    private Transform[] _orbs;

    void Start()
    {
        _orbs = new Transform[orbCount];
        for (int i = 0; i < orbCount; i++)
        {
            float ang = i * 360f / orbCount;
            Vector3 pos = transform.position 
                          + new Vector3(Mathf.Cos(ang * Mathf.Deg2Rad),
                              Mathf.Sin(ang * Mathf.Deg2Rad),
                              0) * radius;
            var o = Instantiate(orbPrefab, pos, Quaternion.identity, transform);
            _orbs[i] = o.transform;
        }
        // Animate orbit
        transform
            .DORotate(new Vector3(0,0,360f), orbitDuration, RotateMode.FastBeyond360)
            .SetLoops(-1, LoopType.Restart)
            .SetEase(Ease.Linear);
    }
}