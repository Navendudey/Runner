using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOver : MonoBehaviour
{
    // Start is called before the first frame update


public class GameOverManager : MonoBehaviour
{
    public string gameOverSceneName = "GameOver";  // Name of the scene to load when game is over

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the object the player collided with has the tag "Obstacle"
        if (collision.gameObject.CompareTag("Object"))
        {
            // Load the Game Over scene
            SceneManager.LoadScene(gameOverSceneName);
        }
    }
}

}
