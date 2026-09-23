using UnityEngine;
using TMPro;
using System.Collections;

public class IngredientBasket : MonoBehaviour
{
    [Header("Fruit Count")]
    public int appleCount = 0;
    public int blueberryCount = 0;
    public int strawberryCount = 0;

    [Header("Maximum Fruit")]
    public int maxApple = 3;
    public int maxBlueberry = 4;
    public int maxStrawberry = 20;

    [Header("Count UI")]
    public TMP_Text appleCountText;
    public TMP_Text blueberryCountText;
    public TMP_Text strawberryCountText;

    [Header("Popup UI")]
    public GameObject popupUI;

    [Tooltip("Berapa detik popup ditampilkan")]
    public float popupDuration = 1.5f;

    private Coroutine popupCoroutine;


    private void Start()
    {
        UpdateCountUI();

        if (popupUI != null)
        {
            popupUI.SetActive(false);
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(
            "Basket terkena object: " +
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


        Debug.Log(
            "Fruit detected: " +
            fruit.fruitType
        );


        // Cek jenis buah
        switch (fruit.fruitType)
        {
            case FruitObject.FruitType.Apple:

                if (appleCount >= maxApple)
                {
                    ShowPopup();
                    return;
                }

                appleCount++;

                Debug.Log(
                    "Apple masuk basket. Jumlah: " +
                    appleCount
                );

                break;


            case FruitObject.FruitType.Blueberry:

                if (blueberryCount >= maxBlueberry)
                {
                    ShowPopup();
                    return;
                }

                blueberryCount++;

                Debug.Log(
                    "Blueberry masuk basket. Jumlah: " +
                    blueberryCount
                );

                break;


            case FruitObject.FruitType.Strawberry:

                if (strawberryCount >= maxStrawberry)
                {
                    ShowPopup();
                    return;
                }

                strawberryCount++;

                Debug.Log(
                    "Strawberry masuk basket. Jumlah: " +
                    strawberryCount
                );

                break;
        }


        // Sembunyikan buah setelah berhasil masuk basket
        fruit.HideFruit();

        // Update angka UI
        UpdateCountUI();
    }


    private void UpdateCountUI()
    {
        if (appleCountText != null)
        {
            appleCountText.text =
                appleCount + " / " + maxApple;
        }

        if (blueberryCountText != null)
        {
            blueberryCountText.text =
                blueberryCount + " / " + maxBlueberry;
        }

        if (strawberryCountText != null)
        {
            strawberryCountText.text =
                strawberryCount + " / " + maxStrawberry;
        }
    }


    private void ShowPopup()
    {
        if (popupUI == null)
            return;

        popupUI.SetActive(true);
        
        if (popupCoroutine != null)
        {
            StopCoroutine(popupCoroutine);
        }

        popupCoroutine =
            StartCoroutine(HidePopupAfterDelay());
    }


    private IEnumerator HidePopupAfterDelay()
    {
        yield return new WaitForSeconds(popupDuration);

        if (popupUI != null)
        {
            popupUI.SetActive(false);
        }

        popupCoroutine = null;
    }
}