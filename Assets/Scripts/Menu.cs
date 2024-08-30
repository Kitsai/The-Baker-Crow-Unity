using UnityEngine;

public class Menu : MonoBehaviour
{
    virtual public void ToggleOpen()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
    virtual public void CloseMenu()
    {
        gameObject.SetActive(false);
    }
    virtual public void OpenMenu()
    {
        gameObject.SetActive(true);
    }
    virtual public bool IsOpen()
    {
        return gameObject.activeSelf;
    }
}
