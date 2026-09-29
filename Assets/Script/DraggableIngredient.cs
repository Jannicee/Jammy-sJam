using UnityEngine;

public class DraggableIngredient : MonoBehaviour
{
    private Camera mainCamera;

    private bool isDragging;

    // Selisih antara posisi mouse dan posisi object
    private Vector3 dragOffset;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void OnMouseDown()
    {
        if (mainCamera == null)
        {
            Debug.LogWarning("Main Camera tidak ditemukan!");
            return;
        }

        isDragging = true;

        // Tentukan posisi mouse di dunia
        Vector3 mouseWorldPosition = GetMouseWorldPosition();

        // Simpan jarak posisi object terhadap mouse
        dragOffset = transform.position - mouseWorldPosition;

        Debug.Log("Mulai drag: " + gameObject.name);
    }

    private void OnMouseDrag()
    {
        if (!isDragging)
            return;

        Vector3 mouseWorldPosition = GetMouseWorldPosition();

        // Object mengikuti mouse + mempertahankan offset
        transform.position = mouseWorldPosition + dragOffset;
    }

    private void OnMouseUp()
    {
        isDragging = false;

        Debug.Log("Selesai drag: " + gameObject.name);
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePosition = Input.mousePosition;

        // Untuk 2D, gunakan jarak antara camera dan object
        mousePosition.z =
            Mathf.Abs(mainCamera.transform.position.z - transform.position.z);

        Vector3 worldPosition =
            mainCamera.ScreenToWorldPoint(mousePosition);

        worldPosition.z = transform.position.z;

        return worldPosition;
    }
}