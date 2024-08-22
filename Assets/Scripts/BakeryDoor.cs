using UnityEngine;
using UnityEngine.SceneManagement;

public class BakeryDoor : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController pc = other.gameObject.GetComponent<PlayerController>();
        if (pc.Facing == PlayerController.FaceDirection.Down)
        {
            SceneManager.LoadScene("OW", 0);
        }
    }
}
