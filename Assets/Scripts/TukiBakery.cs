using UnityEngine;

public class TukiBakery : Player
{
    bool onPuzzleArea = false;
    bool onClientArea = false;
    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Puzzle"))
            onPuzzleArea = true;
        if(other.CompareTag("Client"))
            onClientArea = true;
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Puzzle"))
            onPuzzleArea = false;
        if(other.CompareTag("Client"))
            onClientArea = false;
    }
    public void OnInteract()
    {
        if(onPuzzleArea)
        {
            GameController.Instance.SetState(GameController.GameState.Puzzle);
        }
        if(onClientArea)
        {
        }
    }
}
