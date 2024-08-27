using UnityEngine;
public class Stairs : MonoBehaviour
{
    [SerializeField] PlayerController.FaceDirection faceRequirement;
    [SerializeField] Vector3 newPos;

    public void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController pc = other.GetComponent<PlayerController>();
        if(pc.Facing == faceRequirement)
            pc.transform.position = newPos; 
    }
}
