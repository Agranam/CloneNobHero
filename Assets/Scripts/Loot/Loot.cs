using System;
using System.Collections;
using UnityEngine;

public class Loot : MonoBehaviour
{
    [SerializeField] private int _speedRotation = 200;
    [SerializeField] private Collider _collider;

    private void Update()
    {
        Rotation();
    }

    private void Rotation()
    {
        transform.Rotate(Vector3.up * _speedRotation * Time.deltaTime);
    }
    
    public void Collect(PlayerCollector playerCollector)
    {
        _collider.enabled = false;
        StartCoroutine(MoveToCollector(playerCollector));
    }

    IEnumerator MoveToCollector(PlayerCollector playerCollector)
    {
        Vector3 a = transform.position;
        Vector3 b = a + Vector3.up * 2.5f;
        
        
        for (float t = 0; t < 1f; t+=Time.deltaTime * 3)
        {
            Vector3 d = playerCollector.transform.position;
            Vector3 c = d + Vector3.up * 2.5f;
            Vector3 position = Bezier.GetPoint(a, b, c, d, t);
            
            transform.position = position;
            yield return null;
        }
        
        Take(playerCollector);
    }

    protected virtual void Take(PlayerCollector playerCollector)
    {
        Destroy(gameObject);
    }
}
