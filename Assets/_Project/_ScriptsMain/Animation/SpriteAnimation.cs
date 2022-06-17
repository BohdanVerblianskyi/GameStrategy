using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimation : MonoBehaviour
{
    [SerializeField] private List<Sprite> _sprites;
    [SerializeField] private float _duration;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private float _interval;
    private int _index;

    private void Awake()
    {
        _interval = _duration / _sprites.Count;
        _index = 0;
    }

    private void OnEnable()
    {
        StartCoroutine(ShowAnimation());
    }

    private void OnDisable()
    {
        StopCoroutine(ShowAnimation());
    }

    private IEnumerator ShowAnimation()
    {
        while (true)
        {
            yield return new WaitForSeconds(_interval);
            _spriteRenderer.sprite = _sprites[_index];

            _index++;
            if (_index >= _sprites.Count)
            {
                _index = 0;
            }
        }
    }
}
