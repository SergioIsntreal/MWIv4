using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipToLevel : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            GoToLevel();
        }
    }

    public void GoToLevel()
    {
        SceneManager.LoadScene("Level Test");
    }
}
