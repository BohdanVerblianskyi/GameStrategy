using System;

namespace _Project._ScriptsMain.Unit.MovebleUnit
{
    public interface IAttacking
    {
        public bool TryAttack(IDestroyable destroyable);
    }

}