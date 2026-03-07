using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InputFieldGrabber : MonoBehaviour
{
    // Records the player's name
    [Header("The value we got from the input field")]
    [SerializeField] private string inputText;

    [Header("Showing the reaction to the player")]
    [SerializeField] private GameController gameController;

    public void GrabFromInputField (string nameInput)
    {
        inputText = nameInput;
        //gameController.Resume();
        Debug.Log(nameInput);
    }
}
