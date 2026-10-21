using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    // Player components
    public Rigidbody2D RB;
    public Transform T;
    public SpriteRenderer SR;

    // Player-specific stats
    public float Speed = 5;
    public int Score = 0;
    public int Health = 3;
    
    // Text meshes
    public TextMeshPro ScoreText;
    public TextMeshPro HealthText;
    

    void Start()
    {
        // Make sure the text displays correctly
        UpdateScore();
        UpdateHealth();
    }

    void Update()
    {
        Vector2 vel = new Vector2(0,0);
        
        if (Keyboard.current.dKey.isPressed) // Moving right
        {
            vel.x = Speed;
        }
        if (Keyboard.current.aKey.isPressed) // Moving left
        {
            vel.x = -Speed;
        }
        if (Keyboard.current.wKey.isPressed) // Moving up
        {
            vel.y = Speed;
        }
        if (Keyboard.current.sKey.isPressed) // Moving down
        {
            vel.y = -Speed;
        }
        
        if (Keyboard.current.shiftKey.isPressed) // Moving right
        {
            vel.x = Speed;
        }
        
        // Give these changes to the Rigidbody2D
        RB.linearVelocity = vel;
        
        
        if (Keyboard.current.xKey.wasPressedThisFrame)
        {
            if (SR.flipX == true)
            {
                SR.flipX = false;
            }
            else
            {
                SR.flipX = true;
            }
        }
    }
    
    // If the player bumped into something
    private void OnCollisionEnter2D(Collision2D other)
    {
        // That something has the Hazard tag
        if (other.gameObject.CompareTag("Hazard"))
        {
            Health = Health - 1;
            UpdateHealth();
            
            // If our health depleted to 0 or less, load the game over screen
            if (Health <= 0)
            {
                Die();
            }
        }
    }

    // If the player moved into a trigger
    private void OnTriggerEnter2D(Collider2D other)
    {
        // That trigger has the CoinScript
        CoinScript coin = other.gameObject.GetComponent<CoinScript>();
        if (coin != null)
        {
            coin.GetBumped(); // Make the coin destroy itself
            Score++;
            Speed = Speed + 0.2f; // Makes the player move slightly faster
            UpdateScore();
            
            // If our score increases to at least 20, load the win screen
            if (Score >= 20)
            {
                Win();
            }
        }
    }

    public void UpdateScore() // Updates the score text
    {
        ScoreText.text = "Score: " + Score;
    }
    public void UpdateHealth() // Updates the health text
    {
        HealthText.text = Health + " HP";
    }

    public void Die()
    {
        SceneManager.LoadScene("Game Over");
    }
    
    public void Win()
    {
        SceneManager.LoadScene("Game Win");
    }
}
