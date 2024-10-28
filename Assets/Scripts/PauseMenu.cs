using UnityEngine;

public class PauseMenu : Menu
{
    GameController gc;

    void Awake()
    {
        gc = FindAnyObjectByType<GameController>();
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
