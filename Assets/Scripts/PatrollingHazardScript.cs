using UnityEngine;

public class PatrollingHazardScript : MonoBehaviour
{
    // Hazard components
    public Rigidbody2D RB;
    public Transform T;
    public SpriteRenderer SR;
    public NodeScript Target;

    // Hazard-specific stats
    public float Speed = 3;
    public float SpeedCountdown;
    public float SpeedTimer;
    public float NodeWaitCountdown;
    public float NodeWaitTimer;
    
    void Start()
    {
        NodeWaitCountdown = NodeWaitTimer;
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
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (Target.gameObject == other.gameObject)
        {
            NodeWaitCountdown -= Time.deltaTime;
            
            if (NodeWaitCountdown <= 0)
            {
                Target = Target.NextNode;
                NodeWaitCountdown = NodeWaitTimer;
            }
        }
    }
}
