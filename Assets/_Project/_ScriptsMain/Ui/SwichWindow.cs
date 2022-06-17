using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwichWindow : MonoBehaviour
{
    [SerializeField] private List<PopupWindow> _popupWindows;
    [SerializeField] private Button _bachroundButton;

    private void Start()
    {
        _bachroundButton.gameObject.SetActive(false);

        foreach (PopupWindow popupWindow in _popupWindows)
        {
            popupWindow.Button.onClick.AddListener(() => ChangStatButton(popupWindow));
            popupWindow.OnEndMove += PopupWindowEndMove;
        }
    }
        
    private void PopupWindowEndMove()
    {
        foreach (PopupWindow popupWindow  in _popupWindows)
        {
            if (popupWindow.Active)
            {
                _bachroundButton.gameObject.SetActive(true);
                _bachroundButton.onClick.AddListener(() => ChangStatButton(null));
                return;
            }
        }

        _bachroundButton.onClick.RemoveAllListeners();
        _bachroundButton.gameObject.SetActive(false);
    }

    private void ChangStatButton(PopupWindow popupWindow)
    {
        foreach (PopupWindow window in _popupWindows)
        {
            if (window == popupWindow)
            {
                window.ChangeWindowStat();
                continue;
            }

            if (window.Active)
            {
                window.ChangeWindowStat();
            }
        }
    }
}
