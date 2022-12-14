using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public Text highScoreText;
    public static bool mute = false;
   

    // Start is called before the first frame update
    void Start()
    {
        highScoreText.text = "HighScore : " + (int)PlayerPrefs.GetFloat("HighScore");
     
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ToGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
}
