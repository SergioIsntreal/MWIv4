using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerPatience : MonoBehaviour
{
    [Header("Visual References")]
    public SpriteRenderer customerRenderer;
    public GameObject bubbleObject;
    public SpriteRenderer bubbleRenderer;

    [Header("Bubble Sprites")]
    public Sprite happyBubble;
    public Sprite angryBubble;

    [Header("Food Sprites")]
    public Sprite soupSprite;
    public Sprite burgerSprite;
    public Sprite saladSprite;
    public Sprite icecreamSprite;

    [Header("Settings")]
    public float maxPatience = 20f;
    public float baseScale = 0.5f; // The "Normal" size of your bubble

    [Header("Shake Settings")]
    public float maxShakeIntensity = 0.1f;
    private Vector3 originalLocalPosition;

    private float currentPatience;
    private Customer customerScript;
    private float startTurningRed;
    private float fullyRed;

    void Start()
    {
        currentPatience = maxPatience;
        customerScript = GetComponent<Customer>();

        startTurningRed = maxPatience / 3f;
        fullyRed = maxPatience / 6f;

        if (customerRenderer == null) customerRenderer = GetComponent<SpriteRenderer>();

        bubbleObject.SetActive(false);
        originalLocalPosition = transform.localPosition;
    }

    void Update()
    {
        UpdateBubbleVisibility();

        // Only process patience if Waiting or Seated
        if (customerScript.currentState == Customer.CustomerState.Waiting ||
            customerScript.currentState == Customer.CustomerState.Seated)
        {
            currentPatience -= Time.deltaTime;
            UpdateVisuals();

            if (currentPatience <= 0)
            {
                customerScript.LeaveBistro();
                this.enabled = false;
            }
        }
        else if (customerScript.currentState == Customer.CustomerState.Eating)
        {
            // Reset visuals when they are finally served
            customerRenderer.color = Color.white;
            bubbleObject.transform.localScale = new Vector3(baseScale, baseScale, 1);
        }
    }

    void UpdateVisuals()
    {
        // If IsDragging is true, we check if THIS object is the one moving.
        // If the position has changed since the last frame, it means we are dragging it.
        if (Customer.IsDragging)
        {
            // If the customer moved, update the anchor and stop the shake
            if (transform.localPosition != originalLocalPosition)
            {
                originalLocalPosition = transform.localPosition;
                customerRenderer.color = Color.white; // Reset color while moving
                return;
            }
        }

        if (currentPatience <= startTurningRed)
        {
            float intensity = Mathf.InverseLerp(startTurningRed, fullyRed, currentPatience);
            customerRenderer.color = Color.Lerp(Color.white, Color.red, intensity);

            // Shake logic
            float currentShake = intensity * maxShakeIntensity;
            transform.localPosition = originalLocalPosition + new Vector3(
                Random.Range(-currentShake, currentShake),
                Random.Range(-currentShake, currentShake),
                0
            );
        }
        else
        {
            customerRenderer.color = Color.white;
            transform.localPosition = originalLocalPosition;
        }
    }

    void UpdateBubbleVisibility()
    {
        bool shouldShow = (customerScript.currentState == Customer.CustomerState.Waiting ||
                           customerScript.currentState == Customer.CustomerState.Seated ||
                           customerScript.currentState == Customer.CustomerState.Eating);

        if (bubbleObject.activeSelf != shouldShow)
        {
            bubbleObject.SetActive(shouldShow);
        }
    }

    public void SetOrderVisual(string foodName)
    {
        switch (foodName)
        {
            case "Soup": bubbleRenderer.sprite = soupSprite; break;
            case "Burger": bubbleRenderer.sprite = burgerSprite; break;
            case "Salad": bubbleRenderer.sprite = saladSprite; break;
            case "Ice Cream": bubbleRenderer.sprite = icecreamSprite; break;
            default: Debug.LogWarning("Food sprite not found: " + foodName); break;
        }

        // Fix visuals immediately upon ordering
        customerRenderer.color = Color.white;
        bubbleObject.transform.localScale = new Vector3(baseScale, baseScale, 1);
    }

    public void ResetPatience()
    {
        currentPatience = maxPatience;
        customerRenderer.color = Color.white;
    }

    public void UpdateOriginalPosition()
    {
        originalLocalPosition = transform.localPosition;
    }
}
