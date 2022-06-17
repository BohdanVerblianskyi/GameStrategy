using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class UIAnimation 
{
    [SerializeField] private List<Sprite> _sprites;
    [SerializeField] private Image _image;
    [SerializeField] private float _intarval;

    public UIAnimation(List<Sprite> sprites,Image image,float duretion)
    {
        _sprites = sprites;
        _image = image;
        _intarval = duretion / sprites.Count;
    }

    public IEnumerator Animation()
    {
        int i = 0;
        while (i < _sprites.Count)
        {
            yield return new WaitForSeconds(_intarval);
            _image.sprite = _sprites[i];
            i++;
        }
    }
}
