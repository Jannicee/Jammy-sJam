using UnityEngine;

public class DraggableIngredient : MonoBehaviour
{
    private Camera mainCamera;

    private bool isDragging;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void OnMouseDown()
    {
        isDragging = true;
    }

    private void OnMouseDrag()
    {
        if (!isDragging)
            return;

        Vector3 mousePosition = Input.mousePosition;

        Vector3 worldPosition =
            mainCamera.ScreenToWorldPoint(mousePosition);

        worldPosition.z = transform.position.z;

        transform.position = worldPosition;
    }

    private void OnMouseUp()
    {
        isDragging = false;
    }
}