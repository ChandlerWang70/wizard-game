using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* code based n tutorial 
"Health Bar Unity Tutorial in 7 Minutes | DevLog 5
by  BlackCitaldel Studios on youtube
*/
public class HealthBar : MonoBehaviour
{

    public Slider healthSlider;
    public Slider smoothHealthSlider;
    public WizardMovement player;
    private float maxHealth;
    public float health;
    public float healthRegen;

    private float drainSpeed = 0.05f;

    void Start()
    {

        player.health = player.maxHealth;
        maxHealth = player.maxHealth;
        health = player.maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

        health = player.health;

        if (healthSlider.value != health) 
        {
            healthSlider.value = health;
        }

        if (healthSlider.value != smoothHealthSlider.value) 
        {
            smoothHealthSlider.value = Mathf.Lerp(smoothHealthSlider.value, health, drainSpeed);
        }
    }
}
