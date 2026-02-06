using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerPatience : MonoBehaviour
{
    [Header("Bubble Settings")]
    public GameObject bubbleObject; // The child GameObject
    public SpriteRenderer bubbleRenderer;
    public Sprite happyBubble;  // Normal rounded bubble
    public Sprite angryBubble;  // Spiky/Red bubble

    [Header("Timer Settings")]
    public float maxPatience = 20f;
    private float currentPatience;
    private Customer customerScript;

    [Header("Food Visuals")]
    public Sprite soupSprite;
    public Sprite burgerSprite;
    public Sprite saladSprite;
    public Sprite icecreamSprite;

    [Header("Visual Tweaks")]
    public float baseScale = 0.5f; // Set this to the size that looked "correct" in your inspector

    void Start()
    {
        currentPatience = maxPatience;
        customerScript = GetComponent<Customer>();
        bubbleObject.SetActive(false); // Hide at start
    }

    void Update()
    {
        // DEBUG: Print status if the bubble is surprisingly hidden
        if (bubbleObject.activeSelf == false &&
           (customerScript.currentState == Customer.CustomerState.Waiting || customerScript.currentState == Customer.CustomerState.Seated))
        {
            Debug.Log($"[Patience] ACTIVATING BUBBLE for {gameObject.name}. State is {customerScript.currentState}");
            bubbleObject.SetActive(true);
        }
        else if (bubbleObject.activeSelf == true &&
                customerScript.currentState != Customer.CustomerState.Waiting &&
                customerScript.currentState != Customer.CustomerState.Seated)
        {
            Debug.Log($"[Patience] HIDING BUBBLE. State is {customerScript.currentState}");
            bubbleObject.SetActive(false);
        }

        // Only lose patience if they are Waiting or Seated
        if (customerScript.currentState == Customer.CustomerState.Waiting ||
            customerScript.currentState == Customer.CustomerState.Seated)
        {
            currentPatience -= Time.deltaTime;

            // DEBUG: Print timer every 5 seconds to avoid spam
            if (Time.frameCount % 300 == 0)
            {
                Debug.Log($"[Patience] {gameObject.name} Timer: {currentPatience:F1}s / {maxPatience}s");
            }

            UpdateBubbleVisuals();

            if (currentPatience <= 0)
            {
                Debug.LogWarning($"[Patience] {gameObject.name} ran out of patience! Calling LeaveBistro.");

                // Call the method in Customer.cs
                customerScript.LeaveBistro();

                // Disable this script immediately so we don't spam the Leave command
                this.enabled = false;
            }
        }
    }

    public void SetOrderVisual(string foodName)
    {
        // 1. Stop the bubble from pulsing/changing colors if you want it static
        // (Or keep it pulsing if they are still impatient for food!)

        // 2. Map the string name to the correct sprite
        switch (foodName)
        {
            case "Soup":
                bubbleRenderer.sprite = soupSprite;
                break;
            case "Burger":
                bubbleRenderer.sprite = burgerSprite;
                break;
            case "Salad":
                bubbleRenderer.sprite = saladSprite;
                break;
            case "Ice Cream":
                bubbleRenderer.sprite = icecreamSprite;
                break;
            default:
                Debug.LogWarning("Food sprite not found for: " + foodName);
                break;
        }

        // 3. Ensure the bubble is visible
        bubbleObject.SetActive(true);
    }

    void UpdateBubbleVisuals()
    {
        // 1/3 of the timer threshold
        if (currentPatience <= maxPatience / 3f)
        {
            bubbleRenderer.sprite = angryBubble;
            // Optional: Make it wiggle or pulse
            float pulse = 1f + Mathf.Sin(Time.time * 10f) * 0.1f;
            bubbleObject.transform.localScale = new Vector3(baseScale * pulse, baseScale * pulse, 1);
        }
        else
        {
            bubbleRenderer.sprite = happyBubble;
            bubbleObject.transform.localScale = new Vector3(baseScale, baseScale, 1);
        }
    }

    public void ResetPatience()
    {
        currentPatience = maxPatience;
    }
}
