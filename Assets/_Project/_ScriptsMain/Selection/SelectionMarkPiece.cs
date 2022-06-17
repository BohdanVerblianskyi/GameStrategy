using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionMarkPiece : MonoBehaviour
{
    [SerializeField] private Vector2 _directionOffset;
    
    private const float CORECTION_OFFSET = 0.7f;

    public void Offset(float distance)
    {
        transform.localPosition = _directionOffset * distance * CORECTION_OFFSET;
    }
}
