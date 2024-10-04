using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public int health;
    public int attack;
    public int defense;//to be deleted

    public int curr_exp;
    public int needed_exp = 100;
    public void TakeDamage(int dmg)
    {
        int damage = dmg - defense;
        //damage = Mathf.Max(dmg, 0);
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
        
    }

    public void GainExp(int xp)
    {
        curr_exp += xp;
        checkLvlUp();
    }
    protected virtual void checkLvlUp()
    {
        while(curr_exp >= needed_exp)
        {
            curr_exp -= needed_exp;
            LevelUp();
        }
    }
    protected virtual void LevelUp()
    {
        Debug.Log(transform.name + " leveled up!");
        // Level up logic to be implemented by derived classes

    }
    protected virtual void ResetLevel()
    {
        Debug.Log(transform.name + " leveled level reset!");
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }

}