using UnityEngine;

public class PauseMenu : Menu
{
    GameController gc;
    void Awake()
    {
        gc = FindObjectOfType<GameController>();
    }
    public void OnResume()
    {
       gc.SetState(GameController.GameState.Running);
    }
    public void OnSave()
    {
        Debug.Log("save");
    }
    public void OnExit()
    {
        Debug.Log("Exit");
    }
}
