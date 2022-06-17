using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class HealthBar : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _maxValue;
    [SerializeField] private TextMeshProUGUI _currentValue;
    [SerializeField] private Slider _slider;
    [SerializeField] private Image _sliderFill;
    [SerializeField] private Color _manyHealthColor;
    [SerializeField] private Color _mediumHealthColor;
    [SerializeField] private Color _littleHealthColor;

    private const float THRESHOLD_MEDIUM_DEALTH = 0.7f;
    private const float THRESHOLD_LITTLE_DEALTH = 0.3f;

    public void Change(int maxValeu,int currentValue)
    {
        _slider.maxValue = maxValeu;
        _slider.value = currentValue;

        _maxValue.text = maxValeu.ToString();
        _currentValue.text = currentValue.ToString();

        if (maxValeu * THRESHOLD_LITTLE_DEALTH >  currentValue)
        {
            _sliderFill.color = _littleHealthColor;
        }
        else if (maxValeu * THRESHOLD_MEDIUM_DEALTH  > currentValue)
        {
            _sliderFill.color = _mediumHealthColor;
        }
        else
        {
            _sliderFill.color = _manyHealthColor;
        }
    }
}
