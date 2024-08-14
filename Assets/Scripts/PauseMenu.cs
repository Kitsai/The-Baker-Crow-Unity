using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public  bool open = false;
    public  void ToggleOpen()
    {
        open = !open;
    }
}
