using UnityEngine;

public class InventoryMenu : Menu
{
    [SerializeReference] public GameObject[] ingredients;

    void Update()
    {
        bool[] playerInventory = Player.GetInventory();
        for (int i=0;i<ingredients.Length;i++)
        {
            ingredients[i].SetActive(playerInventory[i]);
        }
    }
}