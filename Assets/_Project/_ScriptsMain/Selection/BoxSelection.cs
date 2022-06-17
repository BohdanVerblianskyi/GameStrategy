using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSelection : MonoBehaviour
{
    private List<ISelectable> _selectables;

    private void OnEnable()
    {
        _selectables = new List<ISelectable>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out ISelectable selectable))
        {
            _selectables.Add(selectable);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out ISelectable selectable))
        {
            _selectables.Remove(selectable);
        }
    }

    public void SetScale(Vector2 scale) => transform.localScale = scale;

    public void SetPosition(Vector2 position) => transform.position = position;

    public bool TryGetSelecteds(out List<ISelectable> selectads)
    {
        if (_selectables.Count == 0)
        {
            selectads = null;
            return false;
        }

        selectads = _selectables;
        return true;
    }
}
