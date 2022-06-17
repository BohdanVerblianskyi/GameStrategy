using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Description : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _descriptioText;
    [SerializeField] private RectTransform _background;

    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
        _background.gameObject.SetActive(false);
    }

    private void Update()
    {
        CheckRaycast();
    }

    private void CheckRaycast()
    {
        RaycastHit2D raycastHit = GetRaycastHit();

        if (raycastHit == false)
        {
            _background.gameObject.SetActive(false);
            return;
        }

        if (raycastHit.collider.TryGetComponent(out IDescription description))
        {
            if (!_background.gameObject.activeInHierarchy)
            {
                _background.gameObject.SetActive(true);
            }


            Debug.Log(description.GetType());
            Vector2 position = _camera.WorldToScreenPoint(description.GetPosition());

            transform.position = position;


            //if (DescriptionStorage.TryGet(description.GetType(), out string text))
            //{
            //    _descriptioText.text = text;    
            //    return;
            //}
        }

        if (_background.gameObject.activeInHierarchy)
        {
            _background.gameObject.SetActive(false);
        }
    }

    private RaycastHit2D GetRaycastHit()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        return Physics2D.GetRayIntersection(ray);
    }
}
