using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Rasa _rasa;
    [SerializeField] private Selector _selector;

    private void Update()
    {
        CheckInputRightMouseButton();
        CheckInputLeftMouseButton();
    }

    private void CheckInputLeftMouseButton()
    {

    }

    private void CheckInputRightMouseButton()
    {

    }
}
