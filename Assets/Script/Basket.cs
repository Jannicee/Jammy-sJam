using UnityEngine;
using TMPro;

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

    private void Start()
    {
        UpdateCountUI();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        FruitObject fruit =
            other.GetComponent<FruitObject>();

        if (fruit == null)
            return;

        switch (fruit.fruitType)
        {
            case FruitObject.FruitType.Apple:

                if (appleCount >= maxApple)
                {
                    ShowPopup();
                    return;
                }

                appleCount++;
                break;

            case FruitObject.FruitType.Blueberry:

                if (blueberryCount >= maxBlueberry)
                {
                    ShowPopup();
                    return;
                }

                blueberryCount++;
                break;

            case FruitObject.FruitType.Strawberry:

                if (strawberryCount >= maxStrawberry)
                {
                    ShowPopup();
                    return;
                }

                strawberryCount++;
                break;
        }

        fruit.HideFruit();

        UpdateCountUI();
    }

    private void AddApple(GameObject fruit)
    {
        if (appleCount >= maxApple)
        {
            ShowPopup();
            return;
        }

        appleCount++;

        Debug.Log("Apple masuk basket. Jumlah: " + appleCount);

        HideFruitSprite(fruit);

        UpdateCountUI();
    }

    private void AddBlueberry(GameObject fruit)
    {
        if (blueberryCount >= maxBlueberry)
        {
            ShowPopup();
            return;
        }

        blueberryCount++;

        Debug.Log("Blueberry masuk basket. Jumlah: " + blueberryCount);

        HideFruitSprite(fruit);

        UpdateCountUI();
    }

    private void AddStrawberry(GameObject fruit)
    {
        if (strawberryCount >= maxStrawberry)
        {
            ShowPopup();
            return;
        }

        strawberryCount++;

        Debug.Log("Strawberry masuk basket. Jumlah: " + strawberryCount);

        HideFruitSprite(fruit);

        UpdateCountUI();
    }

    private void HideFruitSprite(GameObject fruit)
    {
        SpriteRenderer spriteRenderer =
            fruit.GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = false;
        }
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
        if (popupUI != null)
        {
            popupUI.SetActive(true);
        }
    }
}
