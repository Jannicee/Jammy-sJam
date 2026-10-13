using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kompor : MonoBehaviour
{
    [Header("Sprite Settings")]
    public Sprite sprite1;
    public Sprite sprite2;

    [Header("Options")]
    public bool triggerOnce = true;

    private SpriteRenderer spriteRenderer;
    private bool hasChanged = false;

    [Header("Sfx kompor")]
    [SerializeField] private AudioSource audioSource;

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
    private void OnMouseDown()
    {
        ChangeSprite();

        if (audioSource != null)
        {
            audioSource.loop = true;
            audioSource.Play();
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
