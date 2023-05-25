using UnityEngine;

public class PowerUpController : MonoBehaviour
{
    [SerializeField]
    private GameObject shieldPower;
    [SerializeField]
    private GameObject scoreBoostPower;
    [SerializeField]
    private GameObject speedUpPower;
}

public enum PowerUpTypes
{
    Shield,
    ScoreBoost,
    SpeedUp
}