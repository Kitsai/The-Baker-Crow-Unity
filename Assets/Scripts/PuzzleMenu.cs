using UnityEngine;
using Cinemachine;

public class PuzzleMenu : Menu
{
    bool open = false;

    [SerializeReference] CinemachineVirtualCamera puzzleCamera;
    [SerializeReference] CinemachineVirtualCamera baseCamera;

    public void OnEnable()
    {
        CameraSwitcher.Register(baseCamera);
        CameraSwitcher.Register(puzzleCamera);
        CameraSwitcher.SwitchCamera(baseCamera);
    }
    public void OnDisable()
    {
        CameraSwitcher.Unregister(baseCamera);
        CameraSwitcher.Unregister(puzzleCamera);
    }
    public override bool IsOpen()
    {
        return open;
    }
    
    public override void OpenMenu()
    {
        CameraSwitcher.SwitchCamera(puzzleCamera);
    }
    public override void CloseMenu()
    {
        CameraSwitcher.SwitchCamera(baseCamera);
    }

    public override void ToggleOpen()
    {
        if(CameraSwitcher.IsActiveCamera(puzzleCamera))
            CameraSwitcher.SwitchCamera(baseCamera);
        else if(CameraSwitcher.IsActiveCamera(baseCamera))
            CameraSwitcher.SwitchCamera(puzzleCamera);
    }
}
