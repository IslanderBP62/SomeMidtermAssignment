using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    // Player
    public PlayerScript Player;
    
    
    // Display final score
    void Start()
    {
        Player.FinalScore();
    }
    
    void Update()
    {
        // Hitting the space key
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Player.ResetStats();
            Player.NextLevel();
        }
    }
}
