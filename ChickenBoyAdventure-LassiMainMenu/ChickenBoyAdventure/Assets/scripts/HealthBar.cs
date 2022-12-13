using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
   // public TMP_Text healthBarText;
    public Slider HealthSlider;

    Damageable playerDamageable;
    

    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player == null)
        {
            Debug.Log("No player found in the scene. Make sure it has tag 'Player'");
        }

        playerDamageable = player.GetComponent<Damageable>();
    }

    void Start()
    {
        HealthSlider.value = CalculateSliderPercentage(playerDamageable.Health, playerDamageable.MaxHealth);
        //HealthBar.text = " HP " + playerDamageable.Health + " / " + playerDamageable.MaxHealth;
    }

    private void OnEnable(){
        playerDamageable.healthChanged.AddListener(OnPlayerHealthChanged);
    }

    //if not using health bar
    private void OnDisable(){
        playerDamageable.healthChanged.RemoveListener(OnPlayerHealthChanged);
    }


    private float CalculateSliderPercentage(float currentHealth, float maxHealth)
    {
        return currentHealth / maxHealth;
    }

    private void OnPlayerHealthChanged (int newHealth, int maxHealth)
    {
        HealthSlider.value = CalculateSliderPercentage(newHealth, maxHealth);
        //HealthBar.text = " HP " + newHealth + " / " + maxHealth;
    }}