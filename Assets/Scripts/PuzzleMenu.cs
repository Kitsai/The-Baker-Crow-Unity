using UnityEngine;
using Cinemachine;

public class PuzzleMenu : Menu
{
    bool open = false;

    [SerializeReference] CinemachineVirtualCamera puzzleCamera;
    [SerializeReference] CinemachineVirtualCamera baseCamera;

    void Awake()
    {
        puzzleCamera = GetComponentInChildren<CinemachineVirtualCamera>();
    }

    public override bool IsOpen()
    {
        return open;
    }
    
    public override void OpenMenu()
    {
        baseCamera.gameObject.SetActive(false); 
        puzzleCamera.gameObject.SetActive(true);
    }
    public override void CloseMenu()
    {
        baseCamera.gameObject.SetActive(true);
        puzzleCamera.gameObject.SetActive(false);
    }

    public override void ToggleOpen()
    {
        baseCamera.gameObject.SetActive(!baseCamera.gameObject.activeSelf);
        puzzleCamera.gameObject.SetActive(!puzzleCamera.gameObject.activeSelf);
    }
}
