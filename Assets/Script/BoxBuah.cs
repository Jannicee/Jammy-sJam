using UnityEngine;

public class BoxBuah : MonoBehaviour
{
    [Header("Fruit Sprites")]
    public Sprite appleSprite;
    public Sprite blueberrySprite;
    public Sprite strawberrySprite;

    [Header("Fruit Object")]
    public GameObject fruitObject;

    private SpriteRenderer fruitDisplay;
    private Collider2D fruitCollider;

    private bool isHoldingFruit = false;
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;

        if (fruitObject == null)
        {
            Debug.LogError(
                "Fruit Object belum diisi pada " + gameObject.name
            );
            enabled = false;
            return;
        }

        fruitDisplay =
            fruitObject.GetComponentInChildren<SpriteRenderer>(true);

        fruitCollider =
            fruitObject.GetComponentInChildren<Collider2D>(true);

        if (fruitDisplay == null)
        {
            Debug.LogError(
                "SpriteRenderer tidak ditemukan pada " +
                fruitObject.name
            );
            enabled = false;
            return;
        }

        if (fruitCollider == null)
        {
            Debug.LogError(
                "Collider2D tidak ditemukan pada " +
                fruitObject.name
            );
            enabled = false;
            return;
        }

        // Buah disembunyikan saat game dimulai.
        HideFruit();
    }

    private void OnMouseDown()
    {
        if (fruitObject == null || isHoldingFruit)
            return;

        // Tampilkan kembali buah jika sebelumnya dimatikan.
        ShowFruit();

        if (fruitObject.activeSelf)
        {
            isHoldingFruit = true;
        }
    }

    private void Update()
    {
        if (!isHoldingFruit || fruitObject == null)
            return;

        if (mainCamera == null)
            mainCamera = Camera.main;

        if (mainCamera == null)
            return;

        Vector3 mousePosition = Input.mousePosition;

        mousePosition.z = Mathf.Abs(
            mainCamera.transform.position.z -
            fruitObject.transform.position.z
        );

        Vector3 worldPosition =
            mainCamera.ScreenToWorldPoint(mousePosition);

        worldPosition.z = fruitObject.transform.position.z;

        fruitObject.transform.position = worldPosition;
    }

    private void OnMouseUp()
    {
        isHoldingFruit = false;
    }

    public void ShowFruit()
    {
        if (fruitObject == null || fruitDisplay == null)
            return;

        Sprite selectedSprite = null;

        // Pilih sprite berdasarkan tag kotak buah.
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
                "Tag kotak buah tidak dikenali: " + gameObject.tag
            );
            return;
        }

        if (selectedSprite == null)
        {
            Debug.LogWarning(
                "Sprite buah belum diisi pada " + gameObject.name
            );
            return;
        }

        // Atur sprite dan aktifkan kembali komponennya.
        fruitDisplay.sprite = selectedSprite;
        fruitDisplay.enabled = true;
        fruitCollider.enabled = true;

        fruitObject.SetActive(true);

        Debug.Log("Buah dimunculkan kembali: " + fruitObject.name);
    }

    public void HideFruit()
    {
        if (fruitObject == null)
            return;

        fruitObject.SetActive(false);
        isHoldingFruit = false;
    }
}