using UnityEngine;

public class FruitBox : MonoBehaviour
{
    [Header("Fruit Sprites")]
    public Sprite appleSprite;
    public Sprite blueberrySprite;
    public Sprite strawberrySprite;

    [Header("Fruit Object")]
    public GameObject fruitObject;

    [Header("Cutting System")]
    [SerializeField]
    private ObjectSwapOnCollision swapController;

    private SpriteRenderer fruitDisplay;
    private Collider2D fruitCollider;
    private bool isHoldingFruit = false;

    private void Start()
    {
        if (fruitObject == null)
        {
            Debug.LogError(
                "Fruit Object belum dimasukkan ke Inspector!"
            );
            return;
        }

        // Ambil komponen dari Fruit Object.
        fruitDisplay =
            fruitObject.GetComponent<SpriteRenderer>();

        fruitCollider =
            fruitObject.GetComponent<Collider2D>();

        if (swapController == null)
        {
            swapController =
                fruitObject.GetComponent<ObjectSwapOnCollision>();
        }

        if (fruitDisplay == null)
        {
            Debug.LogError(
                "SpriteRenderer tidak ditemukan pada Fruit Object!"
            );
        }

        if (swapController == null)
        {
            Debug.LogError(
                "ObjectSwapOnCollision tidak ditemukan pada Fruit Object!"
            );
        }

        // Sembunyikan buah saat awal.
        fruitObject.SetActive(false);
    }

    private void OnMouseDown()
    {
        if (fruitObject == null)
            return;

        Debug.Log("Mouse down on: " + gameObject.name);

        ShowFruit();

        if (fruitObject.activeSelf)
            isHoldingFruit = true;
    }

    private void Update()
    {
        if (!isHoldingFruit || fruitObject == null)
            return;

        if (Camera.main == null)
            return;

        Vector3 mousePosition = Input.mousePosition;

        mousePosition.z =
            Mathf.Abs(Camera.main.transform.position.z);

        Vector3 worldPosition =
            Camera.main.ScreenToWorldPoint(mousePosition);

        worldPosition.z = 0f;

        fruitObject.transform.position = worldPosition;
    }

    private void OnMouseUp()
    {
        isHoldingFruit = false;
    }

    private void ShowFruit()
    {
        if (fruitObject == null ||
            fruitDisplay == null ||
            swapController == null)
        {
            Debug.LogWarning(
                "Fruit Object, SpriteRenderer, atau Swap Controller belum siap."
            );
            return;
        }

        // Tentukan sprite berdasarkan tag Fruit Box.
        Sprite selectedSprite = null;

        if (CompareTag("apple"))
        {
            selectedSprite = appleSprite;
        }
        else if (CompareTag("blueberry"))
        {
            selectedSprite = blueberrySprite;
        }
        else if (CompareTag("strawberry"))
        {
            selectedSprite = strawberrySprite;
        }
        else
        {
            Debug.LogWarning(
                "Tag tidak dikenali: " + gameObject.tag
            );
            return;
        }

        if (selectedSprite == null)
        {
            Debug.LogWarning(
                "Sprite untuk " + gameObject.tag +
                " belum dimasukkan ke Inspector!"
            );
            return;
        }

        // Siapkan buah baru hanya ketika diambil.
        swapController.PrepareNewFruit();

        // Atur sprite dan aktifkan Raw.
        fruitDisplay.sprite = selectedSprite;
        fruitObject.SetActive(true);
        fruitDisplay.enabled = true;

        if (fruitCollider != null)
            fruitCollider.enabled = true;

        Debug.Log(
            "Buah baru diambil dari box: " + gameObject.tag
        );
    }
}