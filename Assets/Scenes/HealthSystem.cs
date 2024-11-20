using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Health
{


    public class HealthSystem
    {
        private int health;
        private int healthMax=100;
        private float timer = 0f; // Timer to track elapsed time

        public HealthSystem(int health)
        {
            this.health = healthMax;
            health = healthMax;
        }

        public int getHealth()
        {
            return health;
        }
        public void Damage(int damageAmount)
        {
            health -= damageAmount;
            if(health < 0)
            {
                health = 0;
            }

        }
        public void Heal(int healAmount)
        {
            health += healAmount;
            if(health > healthMax) { health = healthMax; }
        }
    }
}

