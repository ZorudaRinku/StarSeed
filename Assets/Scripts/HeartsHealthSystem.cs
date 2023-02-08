using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartsHealthSystem
{
    public const int MAX_FRAGMENT_AMOUNT = 2;
    public event EventHandler OnDamaged;
    public event EventHandler OnHeal;
    public event EventHandler OnDeath;
    private List<Heart> _hearts;
    
    public HeartsHealthSystem(int heartAmount)
    {
        _hearts = new List<Heart>();
        for (int i = 0; i < heartAmount; i++)
        {
            Heart heart = new Heart(2);
            _hearts.Add(heart);
        }
    }
    
    public List<Heart> GetHeartList()
    {
        return _hearts;
    }
    
    public int GetHealth()
    {
        int health = 0;
        foreach (var heart in _hearts)
        {
            health += heart.GetFragmentAmount();
        }

        return health;
    }
    
    public void Damage(int damageAmount)
    {
        for (int i = _hearts.Count - 1; i >= 0; i--)
        {
            Heart heart = _hearts[i];
            if (damageAmount > heart.GetFragmentAmount())
            {
                damageAmount -= heart.GetFragmentAmount();
                heart.Damage(heart.GetFragmentAmount());
            }
            else
            {
                heart.Damage(damageAmount);
                break;
            }
        }

        if (OnDamaged != null)
            OnDamaged(this, EventArgs.Empty);
        
        if (IsDead())
            if (OnDeath != null)
                OnDeath(this, EventArgs.Empty);
    }

    public void Heal(int healAmount)
    {
        for (int i = 0; i < _hearts.Count; i++)
        {
            Heart heart = _hearts[i];
            int missingFragments = MAX_FRAGMENT_AMOUNT - heart.GetFragmentAmount();
            if (healAmount > missingFragments)
            {
                healAmount -= missingFragments;
                heart.Heal(missingFragments);
            }
            else
            {
                heart.Heal(healAmount);
                break;
            }
        }

        if (OnHeal != null)
            OnHeal(this, EventArgs.Empty);
    }

    public bool IsDead()
    {
        return _hearts[0].GetFragmentAmount() == 0;
    }
    public class Heart
    {
        private int _fragments;
        
        public Heart(int fragments)
        {
            _fragments = fragments;
        }

        public int GetFragmentAmount()
        {
            return _fragments;
        }

        public void SetFragments(int fragments)
        {
            _fragments = fragments;
        }

        public void Damage(int damageAmount)
        {
            if (damageAmount > _fragments)
            {
                _fragments = 0;
            }
            else
            {
                _fragments -= damageAmount;
            }
        }

        public void Heal(int healAmount)
        {
            if (_fragments + healAmount > MAX_FRAGMENT_AMOUNT)
            {
                _fragments = MAX_FRAGMENT_AMOUNT;
            }
            else
            {
                _fragments += healAmount;
            }
        }
    }
}
