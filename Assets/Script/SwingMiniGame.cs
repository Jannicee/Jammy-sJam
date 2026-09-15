using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingMiniGame : MonoBehaviour
{
    [Header("Game Elements")]
    [SerializeField] private Transform pivot; // Pivot point of rotation
    [SerializeField] private Transform line; // Rotating line
    [SerializeField] private Transform coin; // Rotating target
    [SerializeField] private GameObject coinGlow; // Visual indicator for the active zone

    [Header("Game Settings")]
    [SerializeField] private float speed = 100f; // Initial rotation speed
    [SerializeField] private float maxSpeed = 200f; // Maximum rotation speed
    [SerializeField] private float speedIncrement = 2f; // Speed increase after each success

    private bool isPlaying = false; // Game state
    private int direction = 1; // Initial rotation direction
    private int score = 0; // Player's score
    private bool inZone = false; // If the line is in the target zone

    void Start()
    {
        if (line == null || coin == null || coinGlow == null || pivot == null)
        {
            Debug.LogError("Required references are not assigned!");
            enabled = false;
            return;
        }

        // Initialize rotation direction
        direction = 1;
    }

    void Update()
    {
        if (isPlaying)
        {
            // Rotate the line continuously
            line.RotateAround(pivot.position, Vector3.forward, speed * direction * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
        {
            // Entering the target zone
            inZone = true;
            coinGlow.SetActive(true);
            Debug.Log("Entered target zone!");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
        {
            // Leaving the target zone
            inZone = false;
            coinGlow.SetActive(false);
            Debug.Log("Exited target zone!");
        }
    }

    public void OnClick()
    {
        if (!isPlaying) return;

        if (inZone)
        {
            // Successful click
            score++;
            speed = Mathf.Min(speed + speedIncrement, maxSpeed); // Increase speed, cap at maxSpeed
            direction *= 1; // Reverse direction
            Debug.Log($"Success! Score: {score}, Speed: {speed}");
            RepositionCoin();
        }
        else
        {
            // Missed click
            Debug.Log("Missed! Try again.");
        }
    }

    private void RepositionCoin()
    {
        // Randomize the coin's position
        float randomAngle = Random.Range(30f, 330f);
        coin.localRotation = Quaternion.Euler(0, 0, randomAngle);
        Debug.Log("Coin repositioned!");
    }

    public void StartGame()
    {
        isPlaying = true;
        score = 0;
        speed = 100f;
        direction = Random.Range(0, 2) == 0 ? -1 : 1;
        Debug.Log("Game started!");
    }

    public void StopGame()
    {
        isPlaying = false;
        Debug.Log($"Game over! Final Score: {score}");
    }
}
