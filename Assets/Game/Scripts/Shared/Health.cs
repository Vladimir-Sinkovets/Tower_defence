using System;

namespace Assets.Game.Scripts.Shared
{
    public class Health
    {
        public event Action<int, int> OnHpChanged;
        public event Action<int> OnDamaged;
        public event Action OnDied;

        public int StartHp { get; private set; }
        public int CurrentHp { get; private set; }

        public bool IsDead { get; private set; }

        public Health(int hp)
        {
            StartHp = hp;
            CurrentHp = hp;
            
            IsDead = false;
        }

        public void ApplyDamage(int damage)
        {
            if (IsDead)
                return;

            CurrentHp -= damage;

            OnHpChanged?.Invoke(CurrentHp, StartHp);

            OnDamaged?.Invoke(damage);

            if (CurrentHp <= 0)
            {
                IsDead = true;
                OnDied?.Invoke();
            }
        }

        public void Reset()
        {
            IsDead = false;
            
            CurrentHp = StartHp;
            
            OnHpChanged?.Invoke(CurrentHp, StartHp);
        }

        public void IncreaseHp(int hp)
        {
            StartHp += hp;
            CurrentHp += hp;

            OnHpChanged?.Invoke(CurrentHp, StartHp);
        }

        public void ApplyHeal(int hp)
        {
            CurrentHp += hp;
            
            if (CurrentHp > StartHp)
                CurrentHp = StartHp;
            
            OnHpChanged?.Invoke(CurrentHp, StartHp);
        }

        public void Set(int currentHp, int startHp)
        {
            CurrentHp = currentHp;
            StartHp = startHp;
            
            OnHpChanged?.Invoke(CurrentHp, StartHp);
        }
    }
}