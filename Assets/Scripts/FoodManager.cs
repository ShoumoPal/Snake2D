using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodManager : MonoBehaviour
{
    [SerializeField]
    private GameObject massGainer;
    [SerializeField]
    private GameObject massBurner;
    [SerializeField]
    private float spawnTimer;
    [SerializeField]
    private float spawnDelay;

    public BoxCollider2D gridArea;
    Bounds bounds;

    private void Start()
    {
        bounds = gridArea.bounds;
        InvokeRepeating(nameof(RandomSpawn), spawnTimer, spawnDelay);
    }
    private void RandomSpawn()
    {
        float x1 = Random.Range(bounds.min.x, bounds.max.x);
        float y1 = Random.Range(bounds.min.y, bounds.max.y);

        float x2 = Random.Range(bounds.min.x, bounds.max.x);
        float y2 = Random.Range(bounds.min.y, bounds.max.y);

        if(x1 == x2 && y1 == y2)
        {
            RandomSpawn();
        }
        else
        {
            //massGainer.transform.position = new Vector3(Mathf.Round(x1), Mathf.Round(y1), 0.0f);
            //massBurner.transform.position = new Vector3(Mathf.Round(x2), Mathf.Round(y2), 0.0f);
            Instantiate(massGainer, new Vector3(Mathf.Round(x1), Mathf.Round(y1), 0.0f), Quaternion.identity);
            Instantiate(massBurner, new Vector3(Mathf.Round(x2), Mathf.Round(y2), 0.0f), Quaternion.identity);
        }
    }
}
