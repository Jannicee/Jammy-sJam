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

    [Header("Order")]
    [Tooltip("Canvas/Popup Order")]
    public GameObject orderPopup;

    [Tooltip("Image checkmark pada Order")]
    public Image checkmarkImage;

    private bool isOrderChecked = false;


    // =====================================================
    // RECIPE
    // =====================================================

    [Header("Recipe")]
    [Tooltip("Masukkan semua popup recipe sesuai urutan")]
    public GameObject[] recipePopups;

    [Tooltip("Panel utama Recipe")]
    public GameObject recipePanel;

    [Tooltip("Tombol Close Recipe")]
    public GameObject closeButtonRecipe;

    private int currentRecipeIndex = 0;


    // =====================================================
    // SCENE
    // =====================================================

    [Header("Next Scene")]
    [Tooltip("Nama scene yang ingin dibuka")]
    public string nextSceneName;

    [Header("Home")]
    [Tooltip("Nama scene Home Page")]
    public string homeSceneName;


    // =====================================================
    // START
    // =====================================================

    private void Start()
    {
        // -------------------------
        // SETTINGS
        // -------------------------

        if (settingsPopup != null)
        {
            settingsPopup.SetActive(false);
        }


        // -------------------------
        // ORDER
        // -------------------------

        if (orderPopup != null)
        {
            orderPopup.SetActive(false);
        }

        isOrderChecked = false;

        if (checkmarkImage != null)
        {
            checkmarkImage.enabled = false;
        }


        // -------------------------
        // RECIPE
        // -------------------------

        if (recipePanel != null)
        {
            recipePanel.SetActive(false);
        }

        if (closeButtonRecipe != null)
        {
            closeButtonRecipe.SetActive(false);
        }

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
        {
            settingsPopup.SetActive(true);
        }
    }


    // Tombol "Lanjut Game"
    // Menutup popup Settings
    public void ContinueGame()
    {
        if (settingsPopup != null)
        {
            settingsPopup.SetActive(false);
        }
    }


    // =====================================================
    // ORDER
    // =====================================================

    // Membuka popup Order
    public void OpenOrder()
    {
        if (orderPopup == null)
        {
            Debug.LogWarning(
                "Order Popup belum dimasukkan ke Inspector!"
            );

            return;
        }

        // Buka Order Popup
        orderPopup.SetActive(true);

        // Reset checkbox setiap kali Order dibuka
        isOrderChecked = false;

        if (checkmarkImage != null)
        {
            checkmarkImage.enabled = false;
        }

        Debug.Log("Order Popup dibuka.");
    }


    // =====================================================
    // CHECKMARK
    // =====================================================

    // Menampilkan / menyembunyikan checkmark
    public void ToggleCheckmark()
    {
        if (checkmarkImage == null)
        {
            Debug.LogWarning(
                "Checkmark Image belum dimasukkan ke Inspector!"
            );

            return;
        }

        isOrderChecked = !isOrderChecked;

        checkmarkImage.enabled = isOrderChecked;

        Debug.Log(
            "Checkmark status: " +
            isOrderChecked
        );
    }


    // =====================================================
    // CLOSE ORDER
    // =====================================================

    // Menutup seluruh Order Popup
    // Close Button berada di dalam Order Popup
    public void CloseOrder()
    {
        if (orderPopup == null)
        {
            Debug.LogWarning(
                "Order Popup belum dimasukkan ke Inspector!"
            );

            return;
        }

        // Menutup seluruh popup
        // termasuk Close Button yang merupakan child
        orderPopup.SetActive(false);

        // Reset checkbox
        isOrderChecked = false;

        if (checkmarkImage != null)
        {
            checkmarkImage.enabled = false;
        }

        Debug.Log("Order Popup ditutup.");
    }


    // =====================================================
    // RECIPE
    // =====================================================

    // Membuka Recipe pertama
    public void OpenRecipe()
    {
        if (recipePanel == null)
        {
            Debug.LogWarning(
                "Recipe Panel belum dimasukkan ke Inspector!"
            );

            return;
        }

        if (recipePopups == null ||
            recipePopups.Length == 0)
        {
            Debug.LogWarning(
                "Recipe Popup belum dimasukkan ke Inspector!"
            );

            return;
        }

        // Buka panel utama Recipe
        recipePanel.SetActive(true);

        // Tampilkan tombol Close Recipe
        if (closeButtonRecipe != null)
        {
            closeButtonRecipe.SetActive(true);
        }

        // Mulai dari recipe pertama
        currentRecipeIndex = 0;

        // Tutup semua recipe terlebih dahulu
        CloseAllRecipes();

        // Pastikan recipe pertama tersedia
        if (recipePopups[0] == null)
        {
            Debug.LogWarning(
                "Recipe Popups Element 0 masih kosong!"
            );

            return;
        }

        // Tampilkan recipe pertama
        recipePopups[0].SetActive(true);

        Debug.Log(
            "Membuka recipe: " +
            recipePopups[0].name
        );
    }


    // =====================================================
    // NEXT RECIPE
    // =====================================================

    // Membuka recipe berikutnya
    public void NextRecipe()
    {
        if (recipePopups == null ||
            recipePopups.Length == 0)
        {
            Debug.LogWarning(
                "Recipe Popup belum dimasukkan ke Inspector!"
            );

            return;
        }

        // Tutup recipe saat ini
        if (currentRecipeIndex >= 0 &&
            currentRecipeIndex < recipePopups.Length)
        {
            if (recipePopups[currentRecipeIndex] != null)
            {
                recipePopups[currentRecipeIndex]
                    .SetActive(false);
            }
        }

        // Pindah ke recipe berikutnya
        currentRecipeIndex++;

        // Jika masih ada recipe
        if (currentRecipeIndex < recipePopups.Length)
        {
            if (recipePopups[currentRecipeIndex] != null)
            {
                recipePopups[currentRecipeIndex]
                    .SetActive(true);

                Debug.Log(
                    "Membuka recipe: " +
                    recipePopups[currentRecipeIndex].name
                );
            }
        }
        else
        {
            // Sudah sampai recipe terakhir
            CloseRecipe();
        }
    }


    // =====================================================
    // CLOSE ALL RECIPES
    // =====================================================

    private void CloseAllRecipes()
    {
        if (recipePopups == null)
        {
            return;
        }

        foreach (GameObject popup in recipePopups)
        {
            if (popup != null)
            {
                popup.SetActive(false);
            }
        }
    }


    // =====================================================
    // CLOSE RECIPE
    // =====================================================

    public void CloseRecipe()
    {
        // Tutup semua recipe
        CloseAllRecipes();

        // Tutup panel utama
        if (recipePanel != null)
        {
            recipePanel.SetActive(false);
        }

        // Tutup Close Button Recipe
        if (closeButtonRecipe != null)
        {
            closeButtonRecipe.SetActive(false);
        }

        // Reset index
        currentRecipeIndex = 0;

        Debug.Log("Recipe Popup ditutup.");
    }


    // =====================================================
    // CLOSE SETTINGS
    // =====================================================

    public void CloseSettings()
    {
        if (settingsPopup != null)
        {
            settingsPopup.SetActive(false);
        }

        Debug.Log("Settings Popup ditutup.");
    }


    // =====================================================
    // CLOSE ALL POPUPS
    // =====================================================

    public void CloseAllPopups()
    {
        Debug.Log("CloseAllPopups dipanggil!");

        // Settings
        if (settingsPopup != null)
        {
            settingsPopup.SetActive(false);
        }

        // Order
        CloseOrder();

        // Recipe
        CloseRecipe();
    }


    // =====================================================
    // NEXT SCENE
    // =====================================================

    public void NextScene()
    {
        if (string.IsNullOrEmpty(nextSceneName))
        {
            Debug.LogWarning(
                "Next Scene Name belum diisi!"
            );

            return;
        }

        Time.timeScale = 1f;

        SceneManager.LoadScene(nextSceneName);
    }


    // =====================================================
    // BACK TO HOME
    // =====================================================

    public void BackToHome()
    {
        if (string.IsNullOrEmpty(homeSceneName))
        {
            Debug.LogWarning(
                "Nama scene Home Page belum diisi!"
            );

            return;
        }

        Time.timeScale = 1f;

        SceneManager.LoadScene(homeSceneName);
    }


    // =====================================================
    // EXIT
    // =====================================================

    public void ExitApplication()
    {
        Debug.Log("Exit Application");

        Application.Quit();
    }
}