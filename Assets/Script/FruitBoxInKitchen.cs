using UnityEngine;

public class FruitBoxInKitchen : MonoBehaviour
{
    [Header("Fruit Sprites")]
    public Sprite appleSprite;
    public Sprite blueberrySprite;
    public Sprite strawberrySprite;

    [Header("Fruit Object")]
    public GameObject fruitObject;

    [Header("Swap Controller")]
    public ObjectSwapOnCollision swapController;

    private SpriteRenderer fruitDisplay;

    private bool isHoldingFruit = false;


    // =====================================================
    // START
    // =====================================================

    private void Start()
    {
        if (fruitObject == null)
        {
            Debug.LogWarning(
                "Fruit Object belum dimasukkan pada " +
                gameObject.name
            );

            return;
        }

        fruitDisplay =
            fruitObject.GetComponentInChildren<SpriteRenderer>();

        fruitObject.SetActive(false);

        if (swapController != null)
        {
            swapController.ResetSwap();
        }
    }


    // =====================================================
    // MOUSE DOWN
    // =====================================================

    private void OnMouseDown()
    {
        Debug.Log(
            "Mouse Down: " +
            gameObject.name
        );

        isHoldingFruit = true;

        // Reset buah sebelum digunakan
        ResetFruit();

        ShowFruit();
    }


    // =====================================================
    // DRAG
    // =====================================================

    private void Update()
    {
        if (!isHoldingFruit)
            return;

        if (fruitObject == null)
            return;

        if (Camera.main == null)
            return;

        Vector3 mousePosition =
            Input.mousePosition;

        mousePosition.z =
            Mathf.Abs(
                Camera.main.transform.position.z
            );

        Vector3 worldPosition =
            Camera.main.ScreenToWorldPoint(
                mousePosition
            );

        worldPosition.z = 0;

        fruitObject.transform.position =
            worldPosition;
    }


    // =====================================================
    // MOUSE UP
    // =====================================================

    private void OnMouseUp()
    {
        isHoldingFruit = false;

        Debug.Log(
            "Selesai drag: " +
            gameObject.name
        );
    }


    // =====================================================
    // SHOW FRUIT
    // =====================================================

    private void ShowFruit()
    {
        // Reset proses cutting
        if (swapController != null)
        {
            swapController.ResetSwap();
        }

        // lanjutkan aktifkan buah

        if (fruitDisplay == null)
        {
            Debug.LogWarning(
                "SpriteRenderer Fruit tidak ditemukan!"
            );

            return;
        }


        // -------------------------------------------------
        // Tentukan Sprite berdasarkan Tag
        // -------------------------------------------------

        if (CompareTag("apple"))
        {
            fruitDisplay.sprite =
                appleSprite;
        }
        else if (CompareTag("blueberry"))
        {
            fruitDisplay.sprite =
                blueberrySprite;
        }
        else if (CompareTag("strawberry"))
        {
            fruitDisplay.sprite =
                strawberrySprite;
        }
        else
        {
            Debug.LogWarning(
                "Tag buah tidak dikenali: " +
                gameObject.tag
            );

            return;
        }


        // -------------------------------------------------
        // Aktifkan Fruit
        // -------------------------------------------------

        fruitObject.SetActive(true);

        fruitDisplay.enabled = true;


        // -------------------------------------------------
        // Aktifkan Collider
        // -------------------------------------------------

        Collider2D fruitCollider =
            fruitObject.GetComponentInChildren<Collider2D>();

        if (fruitCollider != null)
        {
            fruitCollider.enabled = true;
        }


        Debug.Log(
            "Fruit berhasil muncul: " +
            fruitObject.name
        );
    }


    // =====================================================
    // RESET FRUIT
    // =====================================================

    private void ResetFruit()
    {
        // -------------------------------------------------
        // Reset Swap Controller
        // -------------------------------------------------

        if (swapController != null)
        {
            swapController.ResetSwap();
            Debug.Log("SUDAH DIRESET");
        }


        // -------------------------------------------------
        // Pastikan Fruit Object aktif
        // -------------------------------------------------

        if (fruitObject != null)
        {
            fruitObject.SetActive(true);
        }


        // -------------------------------------------------
        // Pastikan Sprite Renderer aktif
        // -------------------------------------------------

        if (fruitDisplay != null)
        {
            fruitDisplay.enabled = true;
        }


        // -------------------------------------------------
        // Pastikan Collider aktif
        // -------------------------------------------------

        if (fruitObject != null)
        {
            Collider2D fruitCollider =
                fruitObject.GetComponentInChildren<Collider2D>();

            if (fruitCollider != null)
            {
                fruitCollider.enabled = true;
            }
        }


        Debug.Log(
            "Fruit berhasil di-reset: " + gameObject.name);
    }
}