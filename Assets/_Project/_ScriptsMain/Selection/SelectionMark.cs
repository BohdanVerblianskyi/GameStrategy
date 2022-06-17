using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionMark : MonoBehaviour
{
    [SerializeField] private List<SelectionMarkPiece> _selectionMarkPieces;
    [SerializeField] private float _offset;

    private void OnValidate()
    {
        foreach (SelectionMarkPiece selectionMarkPiece in _selectionMarkPieces)
        {
            selectionMarkPiece.Offset(_offset);
        }
    }
}
