using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public bool open = false;
    public void ToggleOpen()
    {
        open = !open;
    }
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
