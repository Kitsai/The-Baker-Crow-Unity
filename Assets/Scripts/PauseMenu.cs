using UnityEngine;

public class PauseMenu : Menu
{
    public bool open = false;
    public void OnAttack()
    {
        Debug.Log("Ataque");
    }
    public void OnResume()
    {
        open = false;
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
