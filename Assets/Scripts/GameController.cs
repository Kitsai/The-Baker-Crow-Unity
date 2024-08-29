using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeReference] PauseMenu pause;
    [SerializeReference] InventoryMenu inventory;     
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
        if(state == GameState.Running)
        {
            state = GameState.Paused;
            inventory.OpenMenu();
        }

    }
    public void OnInventory()
    {
        if(state == GameState.Running) 
        {
            state = GameState.Inventory;
            inventory.OpenMenu();
        }
    }
    public void OnExitMenu()
    {
        switch(state)
        {
            case GameState.Paused:
                pause.CloseMenu(); break;
            case GameState.Inventory:
                pause.CloseMenu(); break;
            default:
                break;
        }
        state = GameState.Running;
    }
}
