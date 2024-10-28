using UnityEngine;
using UnityEngine.InputSystem;

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
    public static GameController Instance {get; private set;}
    void Awake()
    {
        if(Instance != this)
        {
            Destroy(Instance);
        }
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
        DontDestroyOnLoad(menus[(int)GameState.Paused].gameObject);
        DontDestroyOnLoad(menus[(int)GameState.Inventory].gameObject);
    }
    void Start()
    {
        if(startSequence)
            Player.Instance.transform.position = new Vector2(-23.05f,-6.32f);
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
    public void OnPause(InputAction.CallbackContext _)
    {
        Debug.Log("Pause");
        if(state.Equals(GameState.Running)) SetState(GameState.Paused);
        else SetState(GameState.Running);
    }
    public void OnInventory(InputAction.CallbackContext _)
    {
        SetState(GameState.Inventory);
    }
    void RunStartSequence()
    {
        startSequence = false;
    }
}
