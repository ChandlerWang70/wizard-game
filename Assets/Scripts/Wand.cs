using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wand : MonoBehaviour
{

    public Spell equippedSpell;

    private ScriptableSpell spellData;
    public Transform spellSpawnPoint;
    public GameObject spellPrefab;
    public float spellSpeed;
    private float spellCooldown = 3f;
    private float spellManaCost;
    public float maxMana;
    public float mana;
    public float manaRegen;

    private bool readyToCast = true;
    private float castTimer;

    // Start is called before the first frame update
    void Start()
    {
        spellData = equippedSpell.GetComponent<Spell>().GetComponent<Spell>().spell;
        spellSpeed = spellData.speed;
        spellCooldown = spellData.cooldown;
        spellManaCost = spellData.manaCost;
    }

    // Update is called once per frame
    void Update()
    {
        if (readyToCast)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && (mana >= spellManaCost))
            {
                CastSpell();
            }
        }

        if (!readyToCast) 
        {
            castTimer += Time.deltaTime;

            if (castTimer > spellCooldown) { 
                readyToCast = true; 
            }
        }

        if (mana < 100)
        {
            mana += manaRegen * Time.deltaTime;
        }

    }

    void CastSpell()
    {

        readyToCast = false;
        castTimer = 0;

        var spell = Instantiate(equippedSpell, spellSpawnPoint.position, spellSpawnPoint.rotation);
        spell.GetComponent<Rigidbody>().velocity = spellSpawnPoint.forward * spellSpeed;

        mana -= spellManaCost;
    }

    public float getMana(){
        return mana;
    }

    public void addMana(float set) {
        if (mana + set > maxMana) {
            mana = maxMana;
        } else {
            mana = mana + set; 
        }
    }

    public void equipSpell(Spell spellToEquip) {
        equippedSpell = spellToEquip;
        spellData = equippedSpell.GetComponent<Spell>().GetComponent<Spell>().spell;
        spellSpeed = spellData.speed;
        spellCooldown = spellData.cooldown;
        spellManaCost = spellData.manaCost;
    }

    public void LevelUpEquippedSpell() {
        equippedSpell.LevelUp();
    }
}
