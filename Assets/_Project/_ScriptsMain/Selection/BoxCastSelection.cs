using System.Collections.Generic;
using System.Linq;
using _Project._ScriptsMain.Extensions;
using UnityEngine;

namespace _Project._ScriptsMain.Selection
{
    public class BoxCastSelection
    {
        private LayerMask _selectionLayer;
        private Selector _selector;
        private Vector3 _startPosition;
        private Vector3 _center;
        private Vector3 _scale;
        private List<ISelectable> _currentSelectables;

        public BoxCastSelection(Selector selector, LayerMask selectionLayer)
        {
            _selector = selector;
            _selectionLayer = selectionLayer;
            _currentSelectables = new List<ISelectable>();
        }

        public void Start(Vector3 mousePositionWorldPoint)
        {
            _startPosition = mousePositionWorldPoint;

            CheckBoxCast(mousePositionWorldPoint);
        }

        public void Tick(Vector3 mousePositionWorldPoint)
        {
            CheckBoxCast(mousePositionWorldPoint);
        }

        private void CheckBoxCast(Vector3 mousePositionWorldPoint)
        {
            MyMethods.BoxStretching(_startPosition, mousePositionWorldPoint, out _center, out _scale);

            RaycastHit[] raycasts = Physics.BoxCastAll(
                _center,
                _scale / 2f,
                Vector3.forward,
                Quaternion.identity,
                100f,
                _selectionLayer
            );

            List<ISelectable> selectables = new List<ISelectable>();

            foreach (RaycastHit raycastHit in raycasts)
            {
                if (raycastHit.collider.TryGetComponent(out ISelectable selectable))
                {
                    selectables.Add(selectable);
                }
            }

            SortSelectables(selectables);
        }

        private void SortSelectables(List<ISelectable> selectables)
        {
            List<ISelectable> selectSelectables = new List<ISelectable>();
            List<ISelectable> ignoreSelectables = new List<ISelectable>();

            foreach (var selectable in selectables)
            {
                if (_currentSelectables.Contains(selectable))
                {
                    ignoreSelectables.Add(selectable);
                }
                else
                {
                    selectSelectables.Add(selectable);
                }
            }

            foreach (var currentSelectable in _currentSelectables)
            {
                if (ignoreSelectables.Contains(currentSelectable))
                {
                    continue;
                }

                currentSelectable.Deselect();
            }
            
            _currentSelectables = ignoreSelectables;
            
            foreach (var selectSelectable in selectSelectables)
            {
                _currentSelectables.Add(selectSelectable);
                selectSelectable.Select();
            }
        }

        public List<ISelectable> GetSelected() => _currentSelectables;
    }
}