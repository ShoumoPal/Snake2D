using System.Collections;
using UnityEngine;

public class PowerUpBehaviour : MonoBehaviour
{
    [SerializeField]
    private float despawnTime;
    [SerializeField]
    private ParticleSystem _powerParticles;
    [SerializeField]
    private PowerUpTypes _powerUpType;

    private void OnEnable()
    {
        //_powerParticles = GameObject.FindGameObjectWithTag("PowerParticle").GetComponent<ParticleSystem>();
        //_powerParticles.transform.position = transform.position;
        //_powerParticles.Play();
        StartCoroutine(nameof(PowerLifeTime));
    }
    IEnumerator PowerLifeTime()
    {
        yield return new WaitForSeconds(despawnTime);
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<SnakeController>() != null)
        {
            Destroy(gameObject);
        }
    }
    public PowerUpTypes GetPowerUpType()
    {
        return _powerUpType;
    }
}
