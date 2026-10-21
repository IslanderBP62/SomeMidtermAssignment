using UnityEngine;

public class MovingHazardScript : MonoBehaviour
{
    // Hazard components
    public Rigidbody2D RB;
    public Transform T;
    public SpriteRenderer SR;
    public GameObject Target;

    // Hazard-specific stats
    public float Speed = 3;
    public float SpeedCountdown;
    public float SpeedTimer;
    
    void Start()
    {
        
    }

    void Update()
    {
        Vector2 offset = Target.transform.position - transform.position;
        RB.linearVelocity = offset.normalized * Speed;
        
        SpeedCountdown -= Time.deltaTime;
        
        if (SpeedCountdown <= 0)
        {
            Speed = Speed + 0.015f;
            SpeedCountdown = SpeedTimer;
        }
    }
}
