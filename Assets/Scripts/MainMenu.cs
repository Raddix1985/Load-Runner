using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public Text highScoreText;
    public Text PowerText;

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
}
