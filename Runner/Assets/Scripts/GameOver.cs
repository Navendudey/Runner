using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOver : MonoBehaviour
{
    // Start is called before the first frame update
public void Restart_Button()
    {
        SceneManager.LoadScene(0);
    }
}
