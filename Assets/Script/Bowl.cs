using TMPro;
using UnityEngine;

public class Bowl : MonoBehaviour
{
    // =====================================================
    // FRUIT SETTINGS
    // =====================================================

    [Header("Fruit Settings")]
    [Tooltip("Tag buah yang boleh masuk ke Bowl")]
    [SerializeField] private string targetFruitTag = "StrawberryCut";

    [Tooltip("Jumlah buah yang sudah masuk")]
    [SerializeField] private int fruitCount = 0;

    [Tooltip("Jumlah buah yang dibutuhkan untuk menyelesaikan Bowl")]
    [SerializeField] private int maxFruit = 3;


    // =====================================================
    // COUNT UI
    // =====================================================

    [Header("Count UI")]
    [Tooltip("Text UI untuk menampilkan jumlah buah")]
    [SerializeField] private TMP_Text fruitCountText;


    // =====================================================
    // BOWL STATE
    // =====================================================

    [Header("Bowl State")]
    [Tooltip("Bowl sebelum mencapai jumlah maksimal")]
    [SerializeField] private GameObject bowlObject1;

    [Tooltip("Bowl setelah mencapai jumlah maksimal")]
    [SerializeField] private GameObject bowlObject2;


    // =====================================================
    // PARTICLE
    // =====================================================

    [Header("Particle Effect")]
    [Tooltip("Particle yang muncul ketika Bowl selesai")]
    [SerializeField] private ParticleSystem particleEffect;


    // =====================================================
    // SOUND
    // =====================================================

    [Header("Sound Effect")]
    [Tooltip("Audio Source untuk memainkan SFX")]
    [SerializeField] private AudioSource audioSource;

    [Tooltip("SFX ketika Bowl selesai")]
    [SerializeField] private AudioClip completeSFX;


    // =====================================================
    // BEHAVIOUR
    // =====================================================

    [Header("Behaviour")]
    [Tooltip("Bowl hanya dapat berubah satu kali")]
    [SerializeField] private bool changeOnce = true;

    private bool bowlChanged = false;


    // =====================================================
    // START
    // =====================================================

    private void Start()
    {
        // Kondisi awal Bowl

        if (bowlObject1 != null)
            bowlObject1.SetActive(true);

        if (bowlObject2 != null)
            bowlObject2.SetActive(false);

        // Matikan particle saat awal
        if (particleEffect != null)
            particleEffect.Stop();

        // Update UI
        UpdateCountUI();
    }


    // =====================================================
    // TRIGGER
    // =====================================================

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(
            "Bowl mendeteksi object: " +
            other.gameObject.name
        );


        // -------------------------------------------------
        // Cari FruitObject pada object / parent
        // -------------------------------------------------

        FruitObject fruit = other.GetComponentInParent<FruitObject>();


        if (fruit == null)
        {
            Debug.LogWarning(
                "Object " +
                other.gameObject.name +
                " tidak memiliki FruitObject!"
            );

            return;
        }


        // -------------------------------------------------
        // Cek tag
        // -------------------------------------------------

        bool correctTag =
            other.CompareTag(targetFruitTag) ||
            fruit.gameObject.CompareTag(targetFruitTag);


        if (!correctTag)
        {
            Debug.Log(
                "Buah ditolak. Tag object: " +
                other.gameObject.tag +
                " | Tag FruitObject: " +
                fruit.gameObject.tag
            );

            return;
        }


        // -------------------------------------------------
        // Cek apakah Bowl sudah selesai
        // -------------------------------------------------

        if (changeOnce && bowlChanged)
        {
            Debug.Log("Bowl sudah selesai.");
            return;
        }


        // -------------------------------------------------
        // Cek maximum
        // -------------------------------------------------

        if (fruitCount >= maxFruit)
        {
            Debug.Log("Jumlah buah sudah maksimum.");
            return;
        }


        // -------------------------------------------------
        // TAMBAH COUNT
        // -------------------------------------------------

        fruitCount++;


        Debug.Log(
            "STRAWBERRY BERHASIL MASUK BOWL! " +
            "Jumlah: " +
            fruitCount +
            "/" +
            maxFruit
        );


        // -------------------------------------------------
        // UPDATE UI
        // -------------------------------------------------

        UpdateCountUI();


        // -------------------------------------------------
        // HILANGKAN BUAH
        // -------------------------------------------------

        fruit.gameObject.SetActive(false);


        // -------------------------------------------------
        // CEK SELESAI
        // -------------------------------------------------

        if (fruitCount >= maxFruit)
        {
            CompleteBowl();
        }
    }


    // =====================================================
    // UPDATE COUNT UI
    // =====================================================

    private void UpdateCountUI()
    {
        if (fruitCountText == null)
        {
            Debug.LogWarning(
                "Fruit Count Text belum dimasukkan ke Inspector!"
            );

            return;
        }

        fruitCountText.text =
            fruitCount + " / " + maxFruit;
    }


    // =====================================================
    // COMPLETE BOWL
    // =====================================================

    private void CompleteBowl()
    {
        if (bowlChanged)
            return;

        bowlChanged = true;


        Debug.Log(
            "BOWL SELESAI! " +
            fruitCount +
            "/" +
            maxFruit
        );


        // -------------------------------------------------
        // MATIKAN BOWL 1
        // -------------------------------------------------

        if (bowlObject1 != null)
        {
            bowlObject1.SetActive(false);
        }


        // -------------------------------------------------
        // NYALAKAN BOWL 2
        // -------------------------------------------------

        if (bowlObject2 != null)
        {
            bowlObject2.SetActive(true);

            // Ikuti posisi Bowl 1
            if (bowlObject1 != null)
            {
                bowlObject2.transform.position =
                    bowlObject1.transform.position;

                bowlObject2.transform.rotation =
                    bowlObject1.transform.rotation;

                bowlObject2.transform.localScale =
                    bowlObject1.transform.localScale;
            }
        }


        // -------------------------------------------------
        // PARTICLE
        // -------------------------------------------------

        if (particleEffect != null)
        {
            particleEffect.Play();
        }


        // -------------------------------------------------
        // SOUND
        // -------------------------------------------------

        if (audioSource != null &&
            completeSFX != null)
        {
            audioSource.PlayOneShot(completeSFX);
        }
    }


    // =====================================================
    // RESET
    // =====================================================

    public void ResetBowl()
    {
        fruitCount = 0;
        bowlChanged = false;


        // Bowl 1 ON
        if (bowlObject1 != null)
            bowlObject1.SetActive(true);


        // Bowl 2 OFF
        if (bowlObject2 != null)
            bowlObject2.SetActive(false);


        // Particle OFF
        if (particleEffect != null)
            particleEffect.Stop();


        // Update UI
        UpdateCountUI();


        Debug.Log("Bowl berhasil di-reset.");
    }


    // =====================================================
    // GET COUNT
    // =====================================================

    public int GetFruitCount()
    {
        return fruitCount;
    }
}