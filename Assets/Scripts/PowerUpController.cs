using UnityEngine;

public class PowerUpController : MonoBehaviour
{
    [SerializeField]
    private float spawnTime;
    [SerializeField]
    private float spawnDelay;
    [SerializeField]
    private GameObject shieldPower;
    [SerializeField]
    private GameObject scoreBoostPower;
    [SerializeField]
    private GameObject speedUpPower;

    public BoxCollider2D gridArea;
    Bounds bounds;

    private void Start()
    {
        bounds = gridArea.bounds;
        InvokeRepeating(nameof(RandomizedSpawn), spawnTime, spawnDelay);
    }
    private void RandomizedSpawn()
    {
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        int randomNumber = Random.Range(1, 4);

        switch (randomNumber)
        {
            case 1:
                Instantiate(shieldPower, new Vector3(Mathf.Round(x), Mathf.Round(y), 0.0f), Quaternion.identity);
                break;
            case 2:
                Instantiate(scoreBoostPower, new Vector3(Mathf.Round(x), Mathf.Round(y), 0.0f), Quaternion.identity);
                break;
            case 3:
                Instantiate(speedUpPower, new Vector3(Mathf.Round(x), Mathf.Round(y), 0.0f), Quaternion.identity);
                break;
        }
        
    }
}

public enum PowerUpTypes
{
    Shield,
    ScoreBoost,
    SpeedUp
}