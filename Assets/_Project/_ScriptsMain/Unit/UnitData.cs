using System;
using UnityEngine;

namespace _Project._ScriptsMain.Unit
{
    [Serializable]
    public class UnitData
    {
        [SerializeField] private Rasa _rasa;
        [SerializeField] private string _name;
        [SerializeField] private int _healthPoint;

        public Rasa Rasa {get => _rasa;}
        public string Name { get => _name; }
        public int HealthPoint { get => _healthPoint; }
    }
    
}

public enum Rasa
{
    Red,
    Blue
}