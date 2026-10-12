using System.Collections;
using UnityEngine;

public class ObjectSwapOnCollision : MonoBehaviour
{
    [Header("Collision Settings")]
    [SerializeField] private string targetTag = "Knife";

    [Tooltip("Aktifkan jika Collider menggunakan Is Trigger")]
    [SerializeField] private bool useTrigger = true;

    [Header("Cutting Settings")]
    [SerializeField] private int requiredCollisionCount = 3;

    [Tooltip("Jeda minimal antarhit, dalam detik")]
    [SerializeField] private float hitInterval = 0.4f;

    [Header("Object Settings")]
    [SerializeField] private GameObject object1;
    [SerializeField] private GameObject object2;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip cuttingSFX;

    [Header("Behaviour")]
    [SerializeField] private bool triggerOnce = true;

    private int collisionCount = 0;
    private bool hasTriggered = false;
    private float nextHitTime = 0f;

    private void Start()
    {
        ResetSwap();
    }

    // for sfx stop after seconds
    IEnumerator StopAfterSeconds(float t)
    {
        yield return new WaitForSeconds(t);
        audioSource.Stop();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!useTrigger)
            return;

        Debug.Log("Trigger masuk: " + other.name);

        CheckCollision(other.gameObject);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!useTrigger)
            return;

        // Mengizinkan hit berikutnya jika pisau
        // masih bersentuhan dengan strawberry.
        CheckCollision(other.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (useTrigger)
            return;

        CheckCollision(collision.gameObject);
    }

    private void CheckCollision(GameObject otherObject)
    {
        if (triggerOnce && hasTriggered)
            return;

        if (!otherObject.CompareTag(targetTag))
            return;

        if (Time.time < nextHitTime)
            return;

        nextHitTime = Time.time + hitInterval;
        collisionCount++;

        Debug.Log(
            "Strawberry dipotong: " +
            collisionCount + "/" +
            requiredCollisionCount
        );

        if (audioSource != null && cuttingSFX != null)
        {
            audioSource.clip = cuttingSFX;
            audioSource.Play();
            StartCoroutine(StopAfterSeconds(1f));
        }

        if (collisionCount >= requiredCollisionCount)
        {
            SwapObject();
        }
    }

    private void SwapObject()
    {
        if (object1 == null || object2 == null)
        {
            Debug.LogError(
                "Object 1 atau Object 2 belum diisi!"
            );
            return;
        }

        hasTriggered = true;

        // Samakan transform sebelum mengaktifkan object2.
        object2.transform.position = object1.transform.position;
        object2.transform.rotation = object1.transform.rotation;
        object2.transform.localScale = object1.transform.localScale;

        SpriteRenderer renderer1 =
            object1.GetComponentInChildren<SpriteRenderer>();

        SpriteRenderer renderer2 =
            object2.GetComponentInChildren<SpriteRenderer>();

        if (renderer1 != null && renderer2 != null)
        {
            renderer2.sortingLayerID = renderer1.sortingLayerID;
            renderer2.sortingOrder = renderer1.sortingOrder;
        }

        object1.SetActive(false);
        object2.SetActive(true);

        Debug.Log(
            "OBJECT SWAP BERHASIL: " +
            object1.name + " -> " + object2.name
        );
    }

    public void ResetSwap()
    {
        collisionCount = 0;
        hasTriggered = false;
        nextHitTime = 0f;

        if (object1 != null)
            object1.SetActive(true);

        if (object2 != null)
            object2.SetActive(false);

        Debug.Log("Swap di-reset: " + gameObject.name);
    }

    public int GetCollisionCount()
    {
        return collisionCount;
    }
}