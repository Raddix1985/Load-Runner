using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    // Variables
    private float score = 0.0f;
    public int energy;

    private int difficultyLevel = 1;
    private int maxDifficultyLevel = 20;
    private int scoreToNextLevel = 10;

    public Text scoreText;
    public Text energyText;

    private bool  isDead = false;
    public DeathMenu deathMenu;

    void Update()
    {
        if (isDead)
            return;

        // Score increases as player progresses
        if (score >= scoreToNextLevel)
            LevelUp();

        // Display score on screen
        score += Time.deltaTime * difficultyLevel;
        scoreText.text = ((int)score).ToString();
        energy = PlayerMotor.numOfPower;
        energyText.text = ((int)energy).ToString();
    }

    // Increase game speed and difficulty
    void LevelUp()
    {
        if (difficultyLevel == maxDifficultyLevel)
            return;

        scoreToNextLevel *= 2;
        difficultyLevel++;

       GetComponent<PlayerMotor>().SetSpeed(difficultyLevel);
    }

    public void OnDeath()
    {
        isDead = true;
        if (PlayerPrefs.GetFloat("HighScore") < score)
            PlayerPrefs.SetFloat("HighScore", score);

        deathMenu.ToggleEndMenu(score);
    }
}
