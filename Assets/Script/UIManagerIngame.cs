using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManagerIngame : MonoBehaviour
{
    // =====================================================
    // SETTINGS
    // =====================================================

    [Header("Settings")]
    [Tooltip("Panel/Popup Settings")]
    public GameObject settingsPopup;


    // =====================================================
    // ORDER
    // =====================================================

    public GameObject orderPopup;
    [Tooltip("Image checkmark")]
    public Image checkmarkImage;

    private bool isOrderChecked = false;
    // =====================================================
    // RECIPE
    // =====================================================

    [Header("Recipe")]
    [Tooltip("Masukkan semua popup recipe sesuai urutan")]
    public GameObject[] recipePopups;
    public GameObject recipePanel;

    private int currentRecipeIndex = 0;


    // =====================================================
    // SCENE
    // =====================================================

    [Header("Next Scene")]
    [Tooltip("Masukkan nama scene yang ingin dibuka")]
    public string nextSceneName;

    // HOME
    public string homeSceneName;

    // close button
    public GameObject CloseButtonOrder;
    public GameObject CloseButtonRecipe;

    // =====================================================
    // START
    // =====================================================

    private void Start()
    {
        // Tutup Settings
        if (settingsPopup != null)
            settingsPopup.SetActive(false);

        // Tutup Order
        if (orderPopup != null)
            orderPopup.SetActive(false);

       
        // Tutup semua Recipe
        CloseAllRecipes();

        currentRecipeIndex = 0;
    }


    // =====================================================
    // SETTINGS
    // =====================================================

    // Membuka popup Settings
    public void OpenSettings()
    {
        if (settingsPopup != null)
            settingsPopup.SetActive(true);
    }


    // Tombol "Lanjut Game"
    // Menutup popup Settings
    public void ContinueGame()
    {
        if (settingsPopup != null)
            settingsPopup.SetActive(false);
    }


    // =====================================================
    // ORDER
    // =====================================================

    // Membuka popup/canvas Order
    public void OpenOrder()
    {
        if (orderPopup == null)
        {
            Debug.LogWarning("Order Popup belum dimasukkan ke Inspector!");
            return;
        }

        orderPopup.SetActive(true);

        // Reset checkbox ketika Order dibuka
        isOrderChecked = false;

        if (checkmarkImage != null)
            checkmarkImage.enabled = false;

        Debug.Log("Order Popup dibuka.");
    }

    // =====================================================
    // CHECKBOX ORDER
    // =====================================================

    // Menampilkan / menyembunyikan checkmark
    public void ToggleCheckmark()
    {
        if (checkmarkImage == null)
        {
            Debug.LogWarning("Checkmark Image belum dimasukkan ke Inspector!");
            return;
        }

        isOrderChecked = !isOrderChecked;

        checkmarkImage.enabled = isOrderChecked;

        Debug.Log(
            "Toggle Checkmark berhasil. Status: " +
            isOrderChecked
        );
    }

    // =====================================================
    // RECIPE
    // =====================================================

    // Membuka Recipe pertama

    public void OpenRecipe()
    {
        if (recipePanel != null)
            recipePanel.SetActive(true);

        if (recipePopups == null || recipePopups.Length == 0)
            return;

        currentRecipeIndex = 0;

        CloseAllRecipes();

        if (recipePopups[0] == null)
        {
            Debug.LogWarning(
                "Recipe Popups Element 0 masih kosong!"
            );
            return;
        }

        Debug.Log(
            "Membuka recipe: " +
            recipePopups[0].name
        );

        recipePopups[currentRecipeIndex].SetActive(true);
    }


    // Tombol Next Recipe
    public void NextRecipe()
    {
        if (recipePopups == null || recipePopups.Length == 0)
        {
            Debug.LogWarning("Recipe Popup belum dimasukkan ke Inspector!");
            return;
        }

        // Tutup recipe saat ini
        recipePopups[currentRecipeIndex].SetActive(false);

        // Pindah ke recipe berikutnya
        currentRecipeIndex++;

        // Jika masih ada recipe berikutnya
        if (currentRecipeIndex < recipePopups.Length)
        {
            recipePopups[currentRecipeIndex].SetActive(true);
        }
        else
        {
            currentRecipeIndex = 0;
        }
    }


    // Menutup semua popup Recipe
    private void CloseAllRecipes()
    {
        if (recipePopups == null)
            return;

        foreach (GameObject popup in recipePopups)
        {
            if (popup != null)
                popup.SetActive(false);

            Debug.Log("Close");
        }
    }

    // =====================================================
    // CLOSE
    // =====================================================

    // Fungsi Close untuk menutup popup tertentu
    public void CloseOrder()
    {
        if (orderPopup != null)
        {
            orderPopup.SetActive(false);
            CloseButtonOrder.SetActive(false);
            Debug.Log("Order Popup ditutup.");
        }
        else
        {
            Debug.LogWarning("Order Popup belum dimasukkan ke Inspector!");
        }
    }

    public void CloseSettings()
    {
        if (settingsPopup != null)
        {
            settingsPopup.SetActive(false);
            Debug.Log("Settings Popup ditutup.");
        }
        else
        {
            Debug.LogWarning("Settings Popup belum dimasukkan ke Inspector!");
        }
    }

    public void CloseRecipe()
    {
        CloseAllRecipes();

        if (recipePanel != null)
        {
            recipePanel.SetActive(false);
            CloseButtonRecipe.SetActive(false);
        }

       
        currentRecipeIndex = 0;

        Debug.Log("Recipe Popup ditutup.");
    }

    public void CloseAllPopups()
    {
        Debug.Log("CloseAllPopups dipanggil!");

        // Settings
        if (settingsPopup != null)
            settingsPopup.SetActive(false);

        // Order
        if (orderPopup != null)
            orderPopup.SetActive(false);

        // Recipe
        CloseAllRecipes();

        if (recipePanel != null)
            recipePanel.SetActive(false);

        // Reset checkmark
        isOrderChecked = false;

        if (checkmarkImage != null)
            checkmarkImage.enabled = false;

        currentRecipeIndex = 0;
    }

    // =====================================================
    // NEXT SCENE
    // =====================================================

    // Pindah ke scene yang ditentukan di Inspector
    public void NextScene()
    {
        if (string.IsNullOrEmpty(nextSceneName))
        {
            Debug.LogWarning("Next Scene Name belum diisi!");
            return;
        }

        SceneManager.LoadScene(nextSceneName);
    }


    // =====================================================
    // EXIT
    // =====================================================

    // Keluar dari aplikasi
    public void ExitApplication()
    {
        Debug.Log("Exit Application");

        Application.Quit();
    }

    public void BackToHome()
    {
        if (string.IsNullOrEmpty(homeSceneName))
        { 
            Debug.LogWarning("Nama scene Home Page belum diisi!"); return;
        }
    }
  }