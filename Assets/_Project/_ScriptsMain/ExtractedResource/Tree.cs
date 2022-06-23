using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Project._ScriptsMain.ExtractedResource
{
    public class Tree : ExtractedResource
    {
    
        [SerializeField] private string _name;
        [SerializeField] private int _healthPosint;

        [SerializeField] private Transform _stump;

        private float _currentHealthPoint;
    
        private void Start()
        {
            _stump.gameObject.SetActive(false);
            _currentHealthPoint = _healthPosint;
        }

        public string GetName() => _name;

        public override void SetDamage(float damage)
        {
            _currentHealthPoint -= damage;

             Debug.Log(_currentHealthPoint);
            
            if (_currentHealthPoint<=0)
            {
                _stump.gameObject.SetActive(true);
                _stump.parent = null;
                Destroy(gameObject);
            }
        }

        protected override void ResourceDrop()
        {
            throw new System.NotImplementedException();
        }
    }
}