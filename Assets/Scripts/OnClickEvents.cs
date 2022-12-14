using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OnClickEvents : MonoBehaviour
{
    public TextMeshProUGUI soundText;
  

    // Start is called before the first frame update
    void Start()
    {
        if (MainMenu.mute)
            soundText.text = "/";
        else
            soundText.text = "";
    }

    // Update is called once per frame
    public void ToggleMute()
    {
        if (MainMenu.mute)
        {
            MainMenu.mute = false;
            soundText.text = "";

        }
        else
        {
            MainMenu.mute = true;
            soundText.text = "/";        }
    }
}
