using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : CharacterStats
{
    public int playerLevel = 1;
    public SpellBook spellbook;
    public Wand wand;
    public WizardMovement wm;
    // Start is called before the first frame update
    void Start()
    {
        setStats();
    }

    private void Update()
    {
        if (wm.health <= 0)
        {
            Die();
        }
    }


    protected void setStats()
    {
        wm.maxHealth = 100;
        wm.healthRegen = 0.5f;
        attack = 20;
        defense = 0;
    }

    public void hp()
    {
        if (health + 25 > 100)
        {
            health = 100;
        }
        else
        {
            health += 25;
        }
    }

    protected override void LevelUp()
    {
        base.LevelUp();
        playerLevel++;
        wm.maxHealth += 10 * playerLevel;
        wm.healthRegen += 0.2f;
        attack += 5 * playerLevel;
        defense += 2 * playerLevel;
        needed_exp = Mathf.FloorToInt(needed_exp * 1.2f * playerLevel);

    }
    protected override void ResetLevel()
    {
        base.ResetLevel();
        playerLevel = 1;
        needed_exp = 100;
    }
    protected override void Die()
    {
        spellbook.resetSpellLevel();
        if (wand != null)
        {
            wand.addMana(wand.maxMana);
        }
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        ResetLevel();
        setStats();
        SceneManager.LoadScene(2);

    }

}