using UnityEngine;

[CreateAssetMenu]
public class IngredientSO : ScriptableObject
{
    public enum IngredientType
    {
        Butter,
        Chocolate,
        Egg,
        Honey,
        Milk,
        Strawberry,
        Flour,
        Sugar,
    }
    [SerializeReference] Sprite ingredientImage;

}
