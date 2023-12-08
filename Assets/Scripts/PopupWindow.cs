using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopupWindow : MonoBehaviour
{
    private TMP_Text popupText;

    public GameObject window;
    private Animator popupAnimator;

    private Queue<string> popupQueue;
    private bool isActive;

    private Coroutine queueChecker;

    private void Start()
    {
        
        popupAnimator = window.GetComponent<Animator>();
        popupText = window.GetComponent<TMP_Text>();
        window.SetActive(false);
        popupQueue = new Queue<string>();
    }

    public void AddToQueue(string text)
    {
        popupQueue.Enqueue(text);
        if (queueChecker==null)
        {
            queueChecker = StartCoroutine(CheckQueue());
        }
    }

    private void ShowPopup(string text)
    {
        isActive = true;
        window.SetActive(true);
        popupText.text = text;
        popupAnimator.Play("popup_show");
    }

    private IEnumerator CheckQueue()
    {
        do
        {
            ShowPopup(popupQueue.Dequeue());
            do
            {
                yield return null;
            } while (!popupAnimator.GetCurrentAnimatorStateInfo(0).IsTag("idle"));
        } while (popupQueue.Count > 0);
        isActive = false;
        window.SetActive(false);
        queueChecker = null;
    }

}
