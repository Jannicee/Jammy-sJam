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
    [Tooltip("Object sebelum berubah")]
    [SerializeField] private GameObject object1;

    [Tooltip("Object setelah berubah")]
    [SerializeField] private GameObject object2;

    [Header("Behaviour")]
    [SerializeField] private bool triggerOnce = true;

    private bool hasTriggered = false;
    private int collisionCount = 0;

    [Header("Sound effect")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip cuttingSFX;

    [Tooltip("Durasi SFX yang dimainkan (detik)")]
    [SerializeField] private float cuttingSFXDuration = 0.2f;

    // =====================================================
    // START
    // =====================================================

    private void Start()
    {
        ResetSwap();
    }


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
    // STOP SFX
    // =====================================================
    private void StopCuttingSFX()
    {
        if (audioSource != null)
        {
            audioSource.Stop();
        }
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
                "Target Tag belum diisi pada " +
                gameObject.name
            );

            return;
        }

        if (!otherObject.CompareTag(targetTag))
            return;

        // sfx play
        if (audioSource != null &&
     cuttingSFX != null)
        {
            audioSource.PlayOneShot(
                cuttingSFX,
                1f
            );

            CancelInvoke(nameof(StopCuttingSFX));
            Invoke(
                nameof(StopCuttingSFX),
                cuttingSFXDuration
            );
        }
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
        if (object1 == null || object2 == null)
        {
            Debug.LogWarning(
                "Object 1 atau Object 2 belum diisi!"
            );

            return;
        }

        hasTriggered = true;

        // -------------------------------------------------
        // Samakan Transform
        // -------------------------------------------------

        object2.transform.position =
            object1.transform.position;

        object2.transform.rotation =
            object1.transform.rotation;

        object2.transform.localScale =
            object1.transform.localScale;


        // -------------------------------------------------
        // Samakan Sorting Layer
        // -------------------------------------------------

        SpriteRenderer renderer1 =
            object1.GetComponentInChildren<SpriteRenderer>();

        SpriteRenderer renderer2 =
            object2.GetComponentInChildren<SpriteRenderer>();

        if (renderer1 != null && renderer2 != null)
        {
            renderer2.sortingLayerID =
                renderer1.sortingLayerID;

            renderer2.sortingOrder =
                renderer1.sortingOrder;
        }


        // -------------------------------------------------
        // Swap
        // -------------------------------------------------

        object1.SetActive(false);
        object2.SetActive(true);

        Debug.Log(
            "OBJECT SWAP: " +
            object1.name +
            " → " +
            object2.name
        );
    }


    // =====================================================
    // RESET SWAP
    // =====================================================

    public void ResetSwap()
    {
        hasTriggered = false;
        collisionCount = 0;

        if (object1 != null)
            object1.SetActive(true);

        if (object2 != null)
            object2.SetActive(false);

        Debug.Log(
            "Swap di-reset untuk: " +
            gameObject.name
        );
    }


    // =====================================================
    // GET COUNT
    // =====================================================

    public int GetCollisionCount()
    {
        return collisionCount;
    }
}