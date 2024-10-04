using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* code based n tutorial 
"Health Bar Unity Tutorial in 7 Minutes | DevLog 5
by  BlackCitaldel Studios on youtube
*/
public class manabar : MonoBehaviour
{

    public Slider manaSlider;
    public Slider smoothManaSlider;
    public Wand wand;
    private float maxMana;
    public float mana;
    public float manaRegen;

    private float drainSpeed = 0.05f;

    void Start()
    {

        wand.mana = wand.maxMana;
        maxMana = wand.maxMana;
        mana = wand.maxMana;
    }

    // Update is called once per frame
    void Update()
    {

        mana = wand.mana;

        if (manaSlider.value != mana) 
        {
            manaSlider.value = mana;
        }

        if (manaSlider.value != smoothManaSlider.value) 
        {
            smoothManaSlider.value = Mathf.Lerp(smoothManaSlider.value, mana, drainSpeed);
        }
    }
}
