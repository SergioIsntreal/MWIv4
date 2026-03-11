using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundController : MonoBehaviour
{
    public bool isSwitched = false;
    public Image background1;
    public Image background2;
    public Image background3;
    public Image background4;
    public Animator animator;   

    // Need to enable it so that it cycles through the number of backgrounds, rather than 1 and 2
    // Also needs to register the different Scene Transitions
    public void SwitchImage(Sprite sprite)
    {
        if (!isSwitched)
        {
            background2.sprite = sprite;
            animator.SetTrigger("SceneTransition1");
        }
        else
        {
            background1.sprite = sprite;
            return;
        }

        isSwitched = !isSwitched;
    }

    public void SetImage (Sprite sprite)
    {
        if (!isSwitched)
        {
            background1.sprite = sprite;
        }
        else
        {
            background2.sprite = sprite;
        }
    }
}
