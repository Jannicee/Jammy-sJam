using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectswap : MonoBehaviour
{
    [Header("Sprite Settings")]
    public Sprite sprite1;
    public Sprite sprite2;

    [Header("Collision Settings")]
    public string targetTag = "Strawberry";
    public bool useTrigger = true;

    [Header("Options")]
    public bool triggerOnce = true;

    private SpriteRenderer spriteRenderer;
    private bool hasChanged = false;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer == null)
        {
            Debug.LogError(
                "SpriteRenderer tidak ditemukan pada " + gameObject.name
            );
            enabled = false;
            return;
        }

        // Sprite pertama ditampilkan saat awal.
        spriteRenderer.sprite = sprite1;
    }

    // Pemicu 1: objek diklik dengan mouse.
    //private void OnMouseDown()
    //{
    //    ChangeSprite();
    //}

    // Pemicu 2: objek terkena collider dengan tag khusus.
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!useTrigger)
            return;

        if (other.CompareTag(targetTag))
        {
            ChangeSprite();
        }
    }

    private void ChangeSprite()
    {
        if (triggerOnce && hasChanged)
            return;

        if (spriteRenderer == null || sprite2 == null)
        {
            Debug.LogWarning(
                "SpriteRenderer atau Sprite 2 belum diisi!"
            );
            return;
        }

        // Ganti sprite tanpa mengubah posisi GameObject.
        spriteRenderer.sprite = sprite2;

        hasChanged = true;

        Debug.Log("Sprite berubah menjadi sprite 2: " + gameObject.name);
    }
}
