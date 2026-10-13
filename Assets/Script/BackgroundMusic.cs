using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    private static BackgroundMusic backgroundMusic;

    [Header("Background Music")]
    [SerializeField] private AudioSource audioSource;

    private void Awake()
    {
        // Mencegah background music duplikat.
        if (backgroundMusic == null)
        {
            backgroundMusic = this;
            DontDestroyOnLoad(gameObject);

            // Cari AudioSource jika belum diisi.
            if (audioSource == null)
            {
                audioSource = GetComponent<AudioSource>();
            }

            if (audioSource != null)
            {
                audioSource.loop = true;
                audioSource.Play();
            }
            else
            {
                Debug.LogWarning("AudioSource belum dipasang pada BackgroundMusic!");
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}