using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    public ScriptableSpell spell;
    private string spellType;

    void Start () {
        spellType = spell.GetType().ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter(Collision collision) 
    {
        if (collision.collider.tag == "Enemy")
        {
            DealDamage(collision.collider);
        }
        Destroy(gameObject);
    }

    public void OnTriggerEnter(Collider other) 
    {
        if (other.tag == "Enemy") 
        {
            DealDamage(other);
        }
    }

    public void DealDamage(Collider target) 
    {
        target.GetComponent<EnemyStats>().TakeDamage(Mathf.RoundToInt(spell.damage));
    }

    public void LevelUp() 
    {
        spell.level += 1;

        if (spell.ID == 1f)
        {
            spell.cooldown -= .15f;
        }
        else if (spell.ID == 2f)
        {
            spell.damage += 15f;
        }
        else if (spell.ID == 3f)
        {
            spell.speed += 3f;
        }
    }

    public void ResetSpell() 
    {
        spell.level = 0;

        if (spell.ID == 1f)
        {
            spell.cooldown = 1f;
        }
        else if (spell.ID == 2f)
        {
            spell.damage = 40f;
        }
        else if (spell.ID == 3f)
        {
            spell.speed = 20f;
        }
    }
}
