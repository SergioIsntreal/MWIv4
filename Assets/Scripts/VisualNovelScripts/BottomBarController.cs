using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BottomBarController : MonoBehaviour
{
    // Displaying the text Character-by-Character

    public TextMeshProUGUI barText;
    public TextMeshProUGUI personNameText;

    private int sentenceIndex = -1;
    private StoryScene currentScene;
    private State state = State.COMPLETED;
    private Animator animator;
    private bool isHidden = false;

    private enum State
    {
        PLAYING, COMPLETED
    }

    private void Start()
    {

        animator = GetComponent<Animator>();
    }

    public void Hide()
    {
        if(!isHidden)
        {
            animator.SetTrigger("Hide");
            isHidden = true;
        }
    }

    public void Show()
    {
        if(isHidden)
        {
            animator.SetTrigger("Show");
            isHidden = false;
        }
        
    }

    public void PlayScene(StoryScene scene)
    {
        Show();
        currentScene = scene;
        sentenceIndex = -1;
        PlayNextSentence();
    }

    public void ClearText()
    {
        barText.text = "";
    }

    public void PlayNextSentence()
    {
        // Receives an error about the 'index being out of range'
        StartCoroutine(TypeText(currentScene.sentences[++sentenceIndex].text));
        personNameText.text = currentScene.sentences[sentenceIndex].speaker.speakerName;
        personNameText.color = currentScene.sentences[sentenceIndex].speaker.textColor;
    }

    public bool IsLastSentence()
    {
        return sentenceIndex + 2 == currentScene.sentences.Count;
    }

    public bool IsCompleted()
    {
        return state == State.COMPLETED;
    }

    private IEnumerator TypeText(string text)
    {
        barText.text = "";
        state = State.PLAYING;
        int wordIndex = 0;

        while (state != State.COMPLETED)
        {
            barText.text += text[wordIndex];
            yield return new WaitForSeconds(0.02f);
            if (++wordIndex == text.Length)
            {
                state = State.COMPLETED;
                break;
            }
        }
    }
}
