using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public static class CameraSwitcher
{
    static List<CinemachineCamera> cameras = new();
    public static CinemachineCamera activeCamera = null;

    public static bool IsActiveCamera(CinemachineCamera camera)
    {
        return activeCamera == camera;
    }

    public static void SwitchCamera(CinemachineCamera camera)
    {
        camera.Priority = 10;
        activeCamera = camera;

        foreach (CinemachineCamera c in cameras)
        {
            if (c != camera && c.Priority != 0)
            {
                c.Priority = 0;
            }
        }
    }

    public static void Register(CinemachineCamera camera)
    {
        cameras.Add(camera);
        Debug.Log("Registered camera: " + camera);
    }

    public static void Unregister(CinemachineCamera camera)
    {
        cameras.Remove(camera);
        Debug.Log("Unregistered camera: " + camera);
    }
}
