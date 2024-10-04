using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Spell", menuName = "Spells")]

public class ScriptableSpell : ScriptableObject
{
   public float ID;
   public float level;
   public float manaCost;
   public float lifetime;
   public float speed;
   public float damage;
   public float cooldown;
   public float spellRadius;
}
