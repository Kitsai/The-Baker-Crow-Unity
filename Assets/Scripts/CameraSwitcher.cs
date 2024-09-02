using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public static class CameraSwitcher
{
    static List<CinemachineVirtualCamera> cameras = new List<CinemachineVirtualCamera>();
    static public CinemachineVirtualCamera activeCamera = null;

    public static bool IsActiveCamera(CinemachineVirtualCamera camera)
    {
        return activeCamera == camera;
    }

    public static void SwitchCamera(CinemachineVirtualCamera camera)
    {
        camera.Priority = 10;
        activeCamera = camera;

        foreach(CinemachineVirtualCamera c in cameras)
        {
            if(c != camera && c.Priority != 0)
            {
                c.Priority = 0;
            } 
        }
    } 

    public static void Register(CinemachineVirtualCamera camera)
    {
        cameras.Add(camera);
        Debug.Log("Registered camera: "+camera);
    }

    public static void Unregister(CinemachineVirtualCamera camera)
    {
        cameras.Remove(camera);
        Debug.Log("Unregistered camera: "+camera);
    }
}
