using UnityEngine;

public class FruitObject : MonoBehaviour
{
    public enum FruitType
    {
        Apple,
        Blueberry,
        Strawberry
    }

    public FruitType fruitType;

    public SpriteRenderer spriteRenderer;

    public void HideFruit()
    {
        spriteRenderer.enabled = false;
    }
}