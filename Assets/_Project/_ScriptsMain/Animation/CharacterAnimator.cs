using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacterAnimator : MonoBehaviour
{
    [SerializeField] private Character _character;
    [SerializeField] private List<CharacterAnimationCaders> _characterAnimationCadersList;
    [SerializeField] private CharacterAnimationCaders _characterAnimationCadersDefoult;

    private CharacterAnimationCaders _currentCharacterAnimation;
    private SpriteRenderer _spriteRenderer;
    public event Action onEndAnimationEvent;


    private void Start()
    {        
        _spriteRenderer = _character.GetSpriteRenderer();
        Play(_characterAnimationCadersDefoult);
    }

    public void Play<T>() where T : CharacterAnimationCaders
    {
        foreach (CharacterAnimationCaders characterAnimationCaders  in _characterAnimationCadersList)
        {
            if (characterAnimationCaders is T)
            {
                Play(characterAnimationCaders);
                return;
            }
        }

        Debug.LogError("Not this animation");
        Play(_characterAnimationCadersDefoult);
    }

    private void Play(CharacterAnimationCaders characterAnimationCaders)
    {
        if (characterAnimationCaders == _currentCharacterAnimation)
        {
            return;
        }

        StopAllCoroutines();

        StartCoroutine(ShowAnimation( characterAnimationCaders));
        _currentCharacterAnimation = characterAnimationCaders;
    }

    private IEnumerator ShowAnimation(CharacterAnimationCaders characterAnimationCaders)
    {
        int index = 0;
        while (true)
        {
            _spriteRenderer.sprite = characterAnimationCaders.GetSprite(_character.GetDirection(), index);

            index++;
            if (index >= characterAnimationCaders.Count)
            {
                onEndAnimationEvent?.Invoke();
                index = 0;
            }

            yield return new WaitForSeconds(characterAnimationCaders.Interfal);
        }
    }
}
