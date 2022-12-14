using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private const float COIN_SCORE_MULTIPLIER = 5.0f;

    public static GameManager Instance { set; get; }

    private PlayerMotor motor;

    private Animator anim;

    public static int numOfPower;

    public float modifierScore = 3.0f;
    public Text scoreText, energyText;
    private float score, lastScore, energyScore;

    public GameObject startText;

    public static bool isGameStarted;

    public DeathMenu deathMenu;

    private void Start()
    {
        isGameStarted = false;
        numOfPower = 0;
    }
    private void Awake()
    {
        Instance = this;
        modifierScore = 1;

        scoreText.text = score.ToString("0");
        energyText.text = energyScore.ToString("0");


        motor = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMotor>();
    }



    void Update()
    {
        if (MobileInput.Instance.Tap && !isGameStarted)
        {
            isGameStarted = true;
            Destroy(startText);
            anim.SetBool("isGameStarted", true);
            
        }

        // Display score on screen
        if (isGameStarted)
        {
            score += Time.deltaTime * modifierScore;

            if (lastScore != (int)score)
            {
                lastScore = (int)score;
                scoreText.text = ((int)score).ToString("0");
            }
        }

        // 
        // energy = numOfPower;
        // energyText.text = ((int)energy).ToString();
    }


    public void OnDeath()
    {

        if (PlayerPrefs.GetFloat("HighScore") < score)
            PlayerPrefs.SetFloat("HighScore", score);

        deathMenu.ToggleEndMenu(score);
    }

    public void UpdateModifier(float modifierAmount)
    {
        modifierScore = 1.0f + modifierAmount;
    }
}
