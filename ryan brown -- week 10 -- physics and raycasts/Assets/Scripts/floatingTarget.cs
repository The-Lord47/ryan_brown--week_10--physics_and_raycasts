using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class floatingTarget : MonoBehaviour
{
    //The box's current health point total
    public float maxHealth = 3;
    public float currentHealth = 3;
    Slider healthBar;

    private void Start()
    {
        healthBar = transform.GetChild(0).transform.GetChild(0).GetComponent<Slider>();
    }

    public void Damage(float damageAmount)
    {

        //subtract damage amount when Damage function is called
        currentHealth -= damageAmount;

        healthBar.value = (currentHealth / maxHealth);

        //Check if health has fallen below zero
        if (currentHealth <= 0)
        {
            //if health has fallen below zero, deactivate it 
            Destroy(transform.parent.gameObject);
        }
    }


}
