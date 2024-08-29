using UnityEngine;

public class Menu: MonoBehaviour
{
    public void ToggleOpen()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
    public void CloseMenu()
    {
        gameObject.SetActive(false);
    }
    public void OpenMenu()
    {
        gameObject.SetActive(true);
    }
}
