using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    public GameObject hatPrefab;
    public GameObject manapackPrefab;
    public GameObject scrollPrefab;
    public GameObject medpackPrefab;
    public int experienceValue = 25;
    // Start is called before the first frame update
    void Start()
    {
        /*health = 50; //* whatever level number it is
        attack = 15; //same
        defense = 5; //same*/

    }
    public int getExp()
    {
        return Mathf.FloorToInt(experienceValue * 1.5f); // * level
    }
    protected override void Die()
    {

        base.Die();
        int rn = Random.Range(0, 101); //random number
        Vector3 pos = transform.position;
        pos.y = 1;
        if (rn == 1 || rn == 2)
        {
            Instantiate(hatPrefab, pos, Quaternion.identity);
        }
        else if (rn > 10 && rn < 15)
        {
            Instantiate(scrollPrefab, pos, Quaternion.identity);
        }
        else if (rn >= 15 && rn < 25)
        {
            Instantiate(medpackPrefab, pos, Quaternion.identity);
        }
        else if (rn >= 25 && rn < 35)
        {
            Instantiate(manapackPrefab, pos, Quaternion.identity);
        }
        PlayerStats player = GameObject.FindObjectOfType<PlayerStats>();
        if(player != null)
        {
            player.GainExp(getExp());
        }
        // Add additional death logic specific to the enemy here
        Debug.Log("Enemy has been defeated.");
    }
}
