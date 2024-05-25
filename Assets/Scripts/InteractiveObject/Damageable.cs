using System;
using UnityEngine;

namespace KittyFarm.InteractiveObject
{
    public class Damageable : MonoBehaviour
    {
        public int Health { get; private set; } = 100;
        private int MaxHealth => 100;

        public event Action Dead;

        public void Recover()
        {
            Health = MaxHealth;
        }

        public void TakeDamage(int damage)
        {
            if (Health <= 0)
            {
                return;
            }
            
            Health -= damage;
            if (Health <= 0)
            {
                Dead?.Invoke();
            }
        }
    }
}