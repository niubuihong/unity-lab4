using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameBehavior : MonoBehaviour
{
    public bool showWinScreen = false;
    public bool showLossScreen = false;

    public string labelText = "Collect all 4 goals and win the game!";
    public int maxGoals = 4;

    private int _goalsCollected = 0;
    public int Goals
    {
        // 2
        get { return _goalsCollected; }
        // 3
        set {
            _goalsCollected = value;
            Debug.LogFormat("Goals: {0}", _goalsCollected);
            if(_goalsCollected >= maxGoals)
            {
                labelText = "You've found all the goals!";
                showWinScreen = true;
                Time.timeScale = 0f;
            }
            else
            {
                labelText = "Goal found, only " + (maxGoals -
                _goalsCollected) + " more to go!";
            }
        }
    }
    
    private int _marbleHealth = 10;
    // 4
    public int Health
    {
        get { return _marbleHealth; }
        set {
            _marbleHealth = value;
            Debug.LogFormat("Lives: {0}", _marbleHealth);

            if(_marbleHealth <= 0)
            {
                labelText = "You want another life with that?";
                showLossScreen = true;
                Time.timeScale = 0;
            }
            else
            {
                labelText = "Ouch... that's got hurt.";
            }
        }
    }

    void OnGUI()
    {
        GUI.Box(new Rect(20, 20, 150, 25), "Marble Health:" +
        _marbleHealth);
        GUI.Box(new Rect(20, 50, 150, 25), "Goals Collected: " +
        _goalsCollected);
        GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height -
        50, 300, 50), labelText);

        if (showWinScreen)
        {
            // 4
            if (GUI.Button(new Rect(Screen.width/2 - 100,
            Screen.height/2 - 50, 200, 100), "YOU WON!"))
            {
                SceneManager.LoadScene(0);
                Time.timeScale = 1.0f;
            }
        }

        if(showLossScreen)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 100,
            Screen.height / 2 - 50, 200, 100), "You lose..."))
            {
                SceneManager.LoadScene(0);
                Time.timeScale = 1.0f;
            }
        }
    }
}
