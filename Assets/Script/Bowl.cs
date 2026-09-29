using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Bowl : MonoBehaviour
{
    [Header("Fruit Count")]
    public int strawberryCount = 0;

    [Header("Maximum Fruit")]
    public int maxStrawberry = 20;

    [Header("Count UI")]
    public int StrawberryCount = 0;

    [Header("Popup UI wrong fruit")]
    public GameObject popupUIwrong;

    [Tooltip("Berapa detik popup ditampilkan")]
    public float popupDuration = 1.5f;

    private Coroutine popupCoroutine;



    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(
            "Bowl terkena object: " +
            other.gameObject.name
        );

        FruitObject fruit = other.GetComponentInParent<FruitObject>();


        // Pastikan FruitObject ditemukan terlebih dahulu
        if (fruit == null)
        {
            Debug.LogWarning(
                "Tidak menemukan FruitObject pada: " +
                other.gameObject.name
            );

            return;
        }

        StrawberryCount++;

        Debug.Log(
                    "Strawberry masuk basket. Jumlah: " +
                    strawberryCount
                );

        if (strawberryCount >= maxStrawberry)
        {
            // change to bowl 2 sprite, setActive(false) for bowl 1, setActive(true) for bowl 2
        }
    }


        // Sembunyikan buah setelah berhasil masuk basket
        //fruit.HideFruit();
    }


//    private void ShowPopup()
//    {
//        if (popupUI == null)
//            return;

//        popupUI.SetActive(true);

//        if (popupCoroutine != null)
//        {
//            StopCoroutine(popupCoroutine);
//        }

//        popupCoroutine =
//            StartCoroutine(HidePopupAfterDelay());
//    }


//    private IEnumerator HidePopupAfterDelay()
//    {
//        yield return new WaitForSeconds(popupDuration);

//        if (popupUI != null)
//        {
//            popupUI.SetActive(false);
//        }

//        popupCoroutine = null;
//    }
//}