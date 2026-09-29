using UnityEngine;

public class ObjectSwapOnCollision : MonoBehaviour
{
    [Header("Collision Settings")]
    [Tooltip("Tag object yang akan memicu perubahan")]
    [SerializeField] private string targetTag;

    [Tooltip("Gunakan Trigger jika Collider menggunakan Is Trigger")]
    [SerializeField] private bool useTrigger = true;

    [Header("Required Collision")]
    [Tooltip("Berapa kali harus terjadi collision sebelum object berubah")]
    [SerializeField] private int requiredCollisionCount = 3;

    [Header("Object Settings")]
    [Tooltip("Object yang akan dimatikan")]
    [SerializeField] private GameObject object1;

    [Tooltip("Object yang akan dinyalakan")]
    [SerializeField] private GameObject object2;

    [Header("Behaviour")]
    [Tooltip("Jika aktif, perubahan hanya terjadi satu kali")]
    [SerializeField] private bool triggerOnce = true;

    private bool hasTriggered = false;

    // Jumlah collision yang sudah terjadi
    private int collisionCount = 0;

    // =====================================================
    // TRIGGER
    // =====================================================

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!useTrigger)
            return;

        CheckCollision(other.gameObject);
    }

    // =====================================================
    // COLLISION
    // =====================================================

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (useTrigger)
            return;

        CheckCollision(collision.gameObject);
    }

    // =====================================================
    // CHECK COLLISION
    // =====================================================

    private void CheckCollision(GameObject otherObject)
    {
        if (triggerOnce && hasTriggered)
            return;

        if (string.IsNullOrEmpty(targetTag))
        {
            Debug.LogWarning(
                "Target Tag belum diisi pada " + gameObject.name
            );

            return;
        }

        // Cek apakah tag sesuai
        if (!otherObject.CompareTag(targetTag))
            return;

        // Tambah jumlah collision
        collisionCount++;

        Debug.Log(
            gameObject.name +
            " terkena " +
            otherObject.name +
            " | Collision Count: " +
            collisionCount +
            "/" +
            requiredCollisionCount
        );

        // Cek apakah sudah mencapai jumlah yang diperlukan
        if (collisionCount >= requiredCollisionCount)
        {
            SwapObject();
        }
    }

    // =====================================================
    // SWAP OBJECT
    // =====================================================

    private void SwapObject()
    {
        if (object1 == null)
        {
            Debug.LogWarning(
                "Object 1 belum dimasukkan ke Inspector!"
            );

            return;
        }

        if (object2 == null)
        {
            Debug.LogWarning(
                "Object 2 belum dimasukkan ke Inspector!"
            );

            return;
        }

        hasTriggered = true;

        // =================================================
        // COPY TRANSFORM
        // =================================================

        object2.transform.position =
            object1.transform.position;

        object2.transform.rotation =
            object1.transform.rotation;

        object2.transform.localScale =
            object1.transform.localScale;

        // =================================================
        // COPY SORTING LAYER
        // =================================================

        SpriteRenderer object1Renderer =
            object1.GetComponentInChildren<SpriteRenderer>();

        SpriteRenderer object2Renderer =
            object2.GetComponentInChildren<SpriteRenderer>();

        if (object1Renderer != null && object2Renderer != null)
        {
            object2Renderer.sortingLayerID =
                object1Renderer.sortingLayerID;

            object2Renderer.sortingOrder =
                object1Renderer.sortingOrder;
        }

        // =================================================
        // SWAP
        // =================================================

        object1.SetActive(false);
        object2.SetActive(true);

        Debug.Log(
            "Object berubah: " +
            object1.name +
            " → " +
            object2.name
        );
    }

    // =====================================================
    // GET COLLISION COUNT
    // =====================================================

    public int GetCollisionCount()
    {
        return collisionCount;
    }

    // =====================================================
    // RESET
    // =====================================================

    public void ResetSwap()
    {
        hasTriggered = false;
        collisionCount = 0;

        if (object1 != null)
            object1.SetActive(true);

        if (object2 != null)
            object2.SetActive(false);

        Debug.Log("Collision count di-reset.");
    }
}