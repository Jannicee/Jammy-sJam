using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    // =====================================================
    // RECIPE
    // =====================================================

    [Header("Recipe")]
    [Tooltip("Popup recipe pertama")]
    public GameObject[] recipePopups;
    public GameObject nextRecipePopup;
    private int currentRecipeIndex;

    // =====================================================
    // ORDER
    // =====================================================

    [Header("Order")]
    [Tooltip("Popup order")]
    public GameObject orderPopup;


    // =====================================================
    // SETTINGS
    // =====================================================

    [Header("Settings")]
    [Tooltip("Popup settings")]
    public GameObject settingsPopup;


    // =====================================================
    // CHECKBOX
    // =====================================================

    [Header("Checkbox")]
    [Tooltip("Sprite/GameObject centang")]
    public GameObject checkmark;


    // =====================================================
    // MUSIC
    // =====================================================

    [Header("Music")]
    [Tooltip("Audio Source yang memainkan BGM")]
    public AudioSource bgmAudioSource;

    [Tooltip("Image/Button GameObject untuk kondisi Music ON")]
    public GameObject musicOnObject;

    [Tooltip("Image/Button GameObject untuk kondisi Music OFF")]
    public GameObject musicOffObject;

    private bool isMusicOn = true;


    // =====================================================
    // SCENE
    // =====================================================

    [Header("Next Scene")]
    [Tooltip("Nama scene yang akan dibuka")]
    public string nextSceneName;
    private object recipePopup;


    // =====================================================
    // START
    // =====================================================

    private void Start()
    {
        // Semua popup ditutup ketika scene dimulai

        if (nextRecipePopup != null)
            nextRecipePopup.SetActive(false);

        if (orderPopup != null)
            orderPopup.SetActive(false);

        if (settingsPopup != null)
            settingsPopup.SetActive(false);

        // Checkbox belum dicentang
        if (checkmark != null)
            checkmark.SetActive(false);

        // Music mulai dalam kondisi ON
        isMusicOn = true;

        UpdateMusicUI();
    }


    // =====================================================
    // RECIPE
    // =====================================================

    // Membuka resep pertama atau popup saat ini
    // Membuka resep pertama atau popup saat ini
    public void OpenRecipe()
    {
        currentRecipeIndex = 0;
        OpenCurrentRecipe();
    }

    private void OpenCurrentRecipe()
    {
        // Tutup semua dulu
        foreach (var popup in recipePopups)
        {
            if (popup != null)
                popup.SetActive(false);
        }

        if (recipePopups.Length > 0 && currentRecipeIndex < recipePopups.Length)
        {
            recipePopups[currentRecipeIndex].SetActive(true);
        }
    }

    // Tutup resep saat ini dan buka resep berikutnya jika ada
    public void NextRecipe()
    {
        if (recipePopups.Length == 0)
            return;

        // Tutup recipe sekarang
        if (currentRecipeIndex < recipePopups.Length)
        {
            recipePopups[currentRecipeIndex].SetActive(false);
        }

        currentRecipeIndex++;

        if (currentRecipeIndex < recipePopups.Length)
        {
            // Buka resep berikutnya
            recipePopups[currentRecipeIndex].SetActive(true);
        }
        else
        {
            // Sudah sampai yang terakhir, tutup semua
            CloseRecipe();
        }
    }

    // Tutup semua popup resep dan reset index
    public void CloseRecipe()
    {
        foreach (var popup in recipePopups)
        {
            if (popup != null)
                popup.SetActive(false);
        }

        currentRecipeIndex = 0;
    }




    // =====================================================
    // ORDER
    // =====================================================

    public void OpenOrder()
    {
        if (orderPopup != null)
        {
            orderPopup.SetActive(true);
        }
    }


    public void CloseOrder()
    {
        if (orderPopup != null)
            orderPopup.SetActive(false);
    }

    // =====================================================
    // SETTINGS
    // =====================================================

    public void OpenSettings()
    {
        if (settingsPopup != null)
        {
            settingsPopup.SetActive(true);
        }
    }

    public void CloseSettings()
    {
        if (settingsPopup != null)
            settingsPopup.SetActive(false);
    }

    // =====================================================
    // PAUSE / RESUME
    // =====================================================

    public void PauseGame()
    {
        // Menutup settings
        if (settingsPopup != null)
        {
            settingsPopup.SetActive(false);
        }

        // Melanjutkan game
        Time.timeScale = 1f;
    }


    // =====================================================
    // MUSIC
    // =====================================================

    public void ToggleMusic()
    {
        isMusicOn = !isMusicOn;

        if (bgmAudioSource != null)
        {
            bgmAudioSource.mute = !isMusicOn;
        }

        UpdateMusicUI();
    }


    private void UpdateMusicUI()
    {
        if (musicOnObject != null)
        {
            musicOnObject.SetActive(isMusicOn);
        }

        if (musicOffObject != null)
        {
            musicOffObject.SetActive(!isMusicOn);
        }
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

        SceneManager.LoadScene(nextSceneName);
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
