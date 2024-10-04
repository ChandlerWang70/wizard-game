using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellBook : MonoBehaviour
{
    // Start is called before the first frame update

    public Spell magicMissile;
    public GameObject magicMissileEquip;
    public Spell fireball;
    public GameObject fireballEquip;
    public Spell iceShard;
    public GameObject iceShardEquip;
    public GameObject equippedSpell;
    public Transform equippedSpellSpawnPoint;
    public Wand wand;


    void Start()
    {
        magicMissileEquip.SetActive(true);
        fireballEquip.SetActive(false);
        iceShardEquip.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) 
        {
            wand.equipSpell(magicMissile);
            equippedSpell = magicMissileEquip;

            magicMissileEquip.SetActive(true);
            fireballEquip.SetActive(false);
            iceShardEquip.SetActive(false);
        } 
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            wand.equipSpell(fireball);
            equippedSpell = fireballEquip;

            magicMissileEquip.SetActive(false);
            fireballEquip.SetActive(true);
            iceShardEquip.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            wand.equipSpell(iceShard);
            equippedSpell = iceShardEquip;

            magicMissileEquip.SetActive(false);
            fireballEquip.SetActive(false);
            iceShardEquip.SetActive(true);
        }

        
        
    }
    public void resetSpellLevel()
    {
        magicMissile.ResetSpell();
        fireball.ResetSpell();
        iceShard.ResetSpell();
    }
}
