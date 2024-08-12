using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameOver : MonoBehaviour
{
   
    public void Restart_Button()
    {
    SceneManager.LoadScene(0);
    }


}
