using System.Collections;
using UnityEngine;

public class FoodController : MonoBehaviour
{
    [SerializeField]
    private FoodType type;

    private BoxCollider2D gridArea;
    Bounds bounds;

    [SerializeField]
    private float deSpawnTime;

    private void Start()
    {
        gridArea = GameObject.FindGameObjectWithTag("GridArea").GetComponent<BoxCollider2D>();
        bounds = gridArea.bounds;
    }
    private void OnEnable()
    {
        StartCoroutine(FoodLifetime());
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<SnakeController>() != null)
        {
            RandomizedPosition();
        }
    }
    private void RandomizedPosition()
    {
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0.0f);
    }
    IEnumerator FoodLifetime()
    {
        yield return new WaitForSeconds(deSpawnTime);
        Destroy(gameObject);
    }
    public FoodType GetFoodType()
    {
        return type;
    }
}

//Enum for food type
public enum FoodType
{
    MassGainer,
    MassBurner
}
