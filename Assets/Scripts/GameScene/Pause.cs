using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Pause : MonoBehaviour, IPointerDownHandler
{
    [SerializeField]private GameObject pause;
    [SerializeField] private bool autoPause = true;

    private void Update()
    {
        Out();
    }
    public void BtnPause()
    {
        PauseGame();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        ResumeGame();
    }

    void PauseGame()
    {
        pause.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
    }

    void ResumeGame()
    {
        pause.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Confined;
    }

    void Out()
    {
        if(!Application.isFocused && autoPause)
        {
            PauseGame();
        }
    }
}
