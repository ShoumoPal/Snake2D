using UnityEngine;

public class FoodController : MonoBehaviour
{
    public BoxCollider2D gridArea;
    Bounds bounds;

    private void Start()
    {
        bounds = gridArea.bounds;

        RandomizedPosition();
    }
    private void RandomizedPosition()
    {
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0.0f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<SnakeController>() != null)
        {
            RandomizedPosition();
        }
    }
}
