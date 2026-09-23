using UnityEngine;

public class FruitObject : MonoBehaviour
{
    public enum FruitType
    {
        Apple,
        Blueberry,
        Strawberry
    }

    [Header("Fruit Information")]
    public FruitType fruitType;

    private SpriteRenderer[] spriteRenderers;
    private Collider2D[] fruitColliders;

    private void Awake()
    {
        // Ambil semua SpriteRenderer pada object
        spriteRenderers =
            GetComponentsInChildren<SpriteRenderer>(true);

        // Ambil semua collider pada object
        // dan child-nya
        fruitColliders =
            GetComponentsInChildren<Collider2D>(true);

        Debug.Log(
            gameObject.name +
            " memiliki " +
            spriteRenderers.Length +
            " SpriteRenderer."
        );
    }

    public void HideFruit()
    {
        Debug.Log(
            "HideFruit dipanggil untuk: " +
            gameObject.name
        );

        // Matikan semua SpriteRenderer
        foreach (SpriteRenderer sprite in spriteRenderers)
        {
            if (sprite != null)
            {
                sprite.enabled = false;

                Debug.Log(
                    "Sprite dimatikan: " +
                    sprite.gameObject.name
                );
            }
        }

        // Matikan semua collider
        foreach (Collider2D collider in fruitColliders)
        {
            if (collider != null)
            {
                collider.enabled = false;
            }
        }
    }

    public void ShowFruit()
    {
        // Nyalakan kembali semua SpriteRenderer
        foreach (SpriteRenderer sprite in spriteRenderers)
        {
            if (sprite != null)
            {
                sprite.enabled = true;
            }
        }

        // Nyalakan kembali semua collider
        foreach (Collider2D collider in fruitColliders)
        {
            if (collider != null)
            {
                collider.enabled = true;
            }
        }
    }
}
