using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitButton : MonoBehaviour
{
    [SerializeField] private GameObject UnsavedPrompt;

    public void OnPress()
    {
        if (CardDataManager.Instance.IsProgressSaved()) { Application.Quit(); }
        else { UnsavedPrompt.SetActive(true); }
    }

    public void Confirm()
    {
        Application.Quit();
    }

    public void Cancel()
    {
        UnsavedPrompt.SetActive(false);
    }

}
