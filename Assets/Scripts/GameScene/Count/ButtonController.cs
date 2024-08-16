using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    [SerializeField] private Button incrementButton;
    [SerializeField] private CircleManager circleManager;
    void Start()
    {
        incrementButton.onClick.AddListener(circleManager.IncrementCount);
    }
}
