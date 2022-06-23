using System.Collections;
using System.Collections.Generic;
using _Project._ScriptsMain.Unit;
using _Project._ScriptsMain.Unit.MovebleUnit;
using UnityEngine;

public abstract class Character : MovableUnit ,IAttacking
{
    [SerializeField] protected float _damage;
    [SerializeField,Range(0.1f,2f)] protected float _attackSpeed;
    [SerializeField] protected float _attackRange;
    [SerializeField] protected SpriteRenderer _spriteRenderer;

    protected bool _charged;
    protected Coroutine _rechargeCoroutine;
    protected Coroutine _arrackCoroutine;
    
    protected override void Start()
    {
        base.Start();
        _charged = true;
    }

    public bool TryAttack(IDestroyable destroyable)
    {
        if (!_charged)
        {
            return false;
        }
        
        destroyable.SetDamage(_damage);
        _charged = false;
        _rechargeCoroutine = StartCoroutine(Recharge());
        return true;
    }
    
    public Direction GetDirection() => _unitMovement.GetDirection();

    public SpriteRenderer GetSpriteRenderer() => _spriteRenderer;

    private IEnumerator Recharge()
    {
        yield return new WaitForSeconds(_attackSpeed);
        _charged = false;
    }

    protected bool TryGetAllCollisionInComponent<T>(out List<T> components, float radius)
    {
         components = new List<T>();

        Collider[] colliders = Physics.OverlapSphere(transform.position, _attackRange);

        foreach (var collider in colliders) 
        {
            if (collider.TryGetComponent(out T tComponent))
            {
                components.Add(tComponent);
            }
        }
        return components.Count > 0;
    }

    private void OnDrawGizmos()
    {
      Gizmos.color = new Color(1f,0f,0f,.2f);
        Gizmos.DrawSphere(transform.position,_attackRange) ;
        
    }
}
