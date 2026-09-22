using UnityEngine;

public class FruitBox : MonoBehaviour
{
    [Header("Fruit Sprites")]
    public Sprite appleSprite;
    public Sprite blueberrySprite;
    public Sprite strawberrySprite;

    [Header("Fruit Object")]
    public GameObject fruitObject;

    private SpriteRenderer fruitDisplay;

    private bool isHoldingFruit = false;

    private void Start()
    {
        // Ambil SpriteRenderer dari Fruit Object
        fruitDisplay = fruitObject.GetComponent<SpriteRenderer>();

        // Sembunyikan buah saat awal
        fruitObject.SetActive(false);
    }

    private void OnMouseDown()
    {
        isHoldingFruit = true;

        ShowFruit();
    }

    private void Update()
    {
        if (!isHoldingFruit)
            return;

        Vector3 mousePosition = Input.mousePosition;

        mousePosition.z =
            Mathf.Abs(Camera.main.transform.position.z);

        Vector3 worldPosition =
            Camera.main.ScreenToWorldPoint(mousePosition);

        worldPosition.z = 0;

        // Gerakkan GAME OBJECT buah
        fruitObject.transform.position = worldPosition;
    }

    private void OnMouseUp()
    {
        isHoldingFruit = false;
    }

    private void ShowFruit()
    {
        if (CompareTag("apple"))
        {
            fruitDisplay.sprite = appleSprite;
        }
        else if (CompareTag("blueberry"))
        {
            fruitDisplay.sprite = blueberrySprite;
        }
        else if (CompareTag("strawberry"))
        {
            fruitDisplay.sprite = strawberrySprite;
        }
        else
        {
            Debug.LogWarning(
                "Tag tidak dikenali: " + gameObject.tag
            );

            return;
        }

        fruitObject.SetActive(true);
    }
}