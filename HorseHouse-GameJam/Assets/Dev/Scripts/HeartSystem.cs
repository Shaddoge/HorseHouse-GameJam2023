using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartSystem : MonoBehaviour
{
    public Image[] hearts;
    public bool dead;

    private void Start()
    {

    }
    private void OnEnable()
    {
        EventManager.Instance.updateHealth += UpdateHealth;
    }
    private void OnDisable()
    {
        EventManager.Instance.updateHealth -= UpdateHealth;
    }

    void Update()
    {
        if (dead == true)
        {
            // add dead condition
        }
    }
    
    public void UpdateHealth(int health)
    {
        Debug.Log(health);
        for(int i = 0; i < hearts.Length; i++)
        {
            if(i < health)
            {
                hearts[i].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            }
            else
            {
                hearts[i].GetComponent<Image>().color = new Color32(255, 255, 255, 0);
            }
        }
    }
}
