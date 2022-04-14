using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BoxSpawner : MonoBehaviour
{
    [SerializeField] private Transform boxPrefab;
    private List<Transform> _boxes = new List<Transform>();
    private float spawnOffset = 5;

    public BoxCollider2D gridArea;
    public LayerMask mask;
    
    private Vector2 RandomizePosition()
    {
        Bounds bounds = this.gridArea.bounds;
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);
        return (new Vector2(Mathf.Round(x), Mathf.Round(y)));

    }
    public void Spawn()
    {
        Transform segment = Instantiate(this.boxPrefab);
        _boxes.Add(segment);
        do
        {
            _boxes[_boxes.Count - 1].position = RandomizePosition();
        }
        while (Physics2D.OverlapCircle(_boxes[_boxes.Count - 1].position, spawnOffset, mask));
    }
    public void BoxClear()
    {
        for (int i = 0; i < _boxes.Count; i++)
        {
            Destroy(_boxes[i].gameObject);
        }
        _boxes.Clear();        
    }
    
}
