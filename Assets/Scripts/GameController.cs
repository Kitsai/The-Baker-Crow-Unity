using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeReference] PauseMenu pause;
    float timeScale = 1f;

    enum GameState
    {
        Running,
        Paused,
        Inventory,
        Puzzle,
    }
    GameState state = GameState.Running;
    void Update()
    {
        if (state != GameState.Running) 
        {
            timeScale = 0f;
        }
        else 
        {
            timeScale = 1f;
        }
        if (pause.open) 
        {
            pause.gameObject.SetActive(true);
            timeScale = 0f;
        } 
        else
        {
            pause.gameObject.SetActive(false);
            timeScale = 1f;
        }
    }
    void SetGameState(GameState newState)
    {
        switch (state)
        {
            case GameState.Paused:
                pause.gameObject.SetActive(true); break;
            default:
                break;
        }
        switch (newState) 
        {
            case GameState.Paused:
                pause.gameObject.SetActive(true); break;
            default:
                break;
        }
    }        
    void LateUpdate()
    {
        Time.timeScale = timeScale;
    }
    public void OnPause()
    {
        pause.ToggleOpen();
    }
}
