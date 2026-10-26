using System;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;
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
    public int LevelScore = 0;
    public static int TotalScore;
    public static int Health = 3;
    public int LevelPicker = 3;
    public static int CurrentLevel = 1;
    
    // Text meshes
    public TextMeshPro ScoreText;
    public TextMeshPro HealthText;
    public TextMeshPro LevelText;
    

    void Start()
    {
        // Make sure the text displays correctly
        UpdateScore();
        UpdateHealth();
        UpdateLevel();
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
            LevelScore++;
            TotalScore++;
            Speed = Speed + 0.2f; // Makes the player move slightly faster
            UpdateScore();
            
            // If our score increases to at least 20, load the win screen
            if (LevelScore >= 5)
            {
                NextLevel();
            }
        }
    }

    public void UpdateScore() // Updates the score text
    {
        ScoreText.text = "Score: " + TotalScore;
    }
    public void UpdateHealth() // Updates the health text
    {
        HealthText.text = Health + " HP";
    }
    public void UpdateLevel() // Updates the level text
    {
        LevelText.text = "Level " + CurrentLevel;
    }

    public void Die()
    {
        SceneManager.LoadScene("Game Over");
        Health = 3;
        LevelScore = 0;
        TotalScore = 0;
        CurrentLevel = 1;
    }
    
    public void NextLevel()
    {
        CurrentLevel++;
        
        LevelPicker = Random.Range(1, 3);
        
        if (LevelPicker == 1)
        {
            SceneManager.LoadScene("Level 1");
        }
        if (LevelPicker == 2)
        {
            SceneManager.LoadScene("Level 2");
        }
        if (LevelPicker == 3)
        {
            SceneManager.LoadScene("Level 3");
        }
    }
}
