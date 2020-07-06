using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public enum GameState { Play, Paused }

public class GameControls : MonoBehaviour
{  
    public GameState currentState;



    // Start is called before the first frame update
    void Start()
    {
        currentState = GameState.Play;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameObject.FindWithTag("Player"))
        {
            Reload();
        }

        if (PauseMenu.isPaused)
        {
            currentState = GameState.Paused;
        }
        else
        {
            currentState = GameState.Play;
        }
    }

    void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
