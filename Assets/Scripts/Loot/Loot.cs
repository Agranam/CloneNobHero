using System.Collections;
using UnityEngine;

public class Loot : MonoBehaviour
{
    [SerializeField] private Collider _collider;

    public void Collect(Collector collector)
    {
        _collider.enabled = false;
        StartCoroutine(MoveToCollector(collector));
    }

    IEnumerator MoveToCollector(Collector collector)
    {
        Vector3 a = transform.position;
        Vector3 b = a + Vector3.up * 2.5f;
        
        
        for (float t = 0; t < 1f; t+=Time.deltaTime * 3)
        {
            Vector3 d = collector.transform.position;
            Vector3 c = d + Vector3.up * 2.5f;
            Vector3 position = Bezier.GetPoint(a, b, c, d, t);
            
            transform.position = position;
            yield return null;
        }
        
        Take(collector);
    }

    public virtual void Take(Collector collector)
    {
        Destroy(gameObject);
    }
}
