using UnityEngine;

public class GameController : MonoBehaviour
{
    const int NUM_MENUS = 4;
    [SerializeReference] Menu[] menus = new Menu[NUM_MENUS];
    static float timeScale = 1f;
    static bool startSequence = true;
    public enum GameState
    {
        Running = -1,
        Paused = 0,
        Inventory = 1,
        Puzzle = 2,
        Cutscene = 3,
    }
    void Start()
    {
        if(startSequence)
            Player.Instance.transform.position = new Vector2(-34.31f,-27.32f);
        RunStartSequence();
    }
    static GameState state = GameState.Running;
    public GameState GetState()
    {
        return state;
    }
    public void SetState(GameState newState)
    {
        switch (newState)
        {
            case GameState.Running:
                if(!state.Equals(GameState.Running))
                {
                    menus[(int)state].CloseMenu();
                    state = GameState.Running;
                }
                timeScale = 1f;
                break;
            case GameState.Paused:
            case GameState.Inventory:
            case GameState.Puzzle:
                if(state.Equals(GameState.Running))
                {
                    state = newState;
                    menus[(int)newState].OpenMenu();
                }
                timeScale = 0f;
                break;
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
        if(state.Equals(GameState.Running)) SetState(GameState.Paused);
        else SetState(GameState.Running);
    }
    public void OnInventory()
    {
        SetState(GameState.Inventory);
    }
    void RunStartSequence()
    {
        startSequence = false;
    }
}
