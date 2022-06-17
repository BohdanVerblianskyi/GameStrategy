using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterAnimationCaders : ScriptableObject
{
    [SerializeField] private List<Sprite> _spritesUp;
    [SerializeField] private List<Sprite> _spritesDown;
    [SerializeField] private List<Sprite> _spritesLeft;
    [SerializeField] private List<Sprite> _spritesRight;
    [SerializeField] private float _duration;

    private int _count;
    private float _interfal;
    
    public float Duration { get => _duration; private set => _duration = value; }
    public int Count { get => _count;private set => _count = value; }
    public float Interfal { get => _interfal; private set => _interfal = value; }

    private void OnEnable()
    {
        if (_duration == 0f)
        {
            Debug.LogError($"_duration == 0");
        }

        Count = GetCount();
        Interfal = GetInterval();
    }

    public float GetInterval()
    {
        return _duration / GetCount();
    }

    private int GetCount()
    {
        if (_spritesUp.Count == _spritesLeft.Count && _spritesUp.Count == _spritesRight.Count && _spritesUp.Count == _spritesDown.Count)
        {
            return _spritesUp.Count;
        }
        else
        {
            Debug.LogError("Unequal number of sprites");
            return 0;
        }
    }

    public Sprite GetSprite(Direction direction, int index)
    {
        switch (direction)
        {
            case Direction.Up:
                return _spritesUp[index];
            case Direction.Down:
                return _spritesDown[index];
            case Direction.Left:
                return _spritesLeft[index];
            case Direction.Right:
                return _spritesRight[index];
        }
        return _spritesDown[index];
    }
}
