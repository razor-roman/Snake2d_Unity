using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//HIDED BEHIND BOX
public class Food : MonoBehaviour
{
    public BoxCollider2D gridArea;
    private void Start()
    {
        RandomizePosition();
    }
    public Transform GetCoordinate
    {
        get { return this.transform; }
    }
    private void RandomizePosition()
    {
        Bounds bounds = this.gridArea.bounds;
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);
        this.transform.position = new Vector2(Mathf.Round(x), Mathf.Round(y));
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            RandomizePosition();
        }
        do
        {
            RandomizePosition();
        }
        while (collision.tag == "Obstacle");
    }
}