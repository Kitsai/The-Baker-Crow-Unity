using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeReference] PauseMenu pause;
    float timeScale = 1f;


    void Update()
    {
        if (pause.open) 
        {
            pause.gameObject.SetActive(true);
            timeScale = 0f;
        } 
        else
        {
            pause.gameObject.SetActive(false);
            timeScale = 1f;
        }
    }
    void LateUpdate()
    {
        Time.timeScale = timeScale;
    }
}
