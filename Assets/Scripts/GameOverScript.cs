using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    // Sound effects
    public AudioSource AS;
    public AudioClip GameOverSFX;
    
    // Play sound upon showing this screen
    void Start()
    {
        AS.PlayOneShot(GameOverSFX);
    }
    
    //Every frame, we check for player inputs
    void Update()
    {
        //If the player hit space. . .
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            //Then load the 'Example 3' scene
            SceneManager.LoadScene("Level 1");
        }
    }
}
