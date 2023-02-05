using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartSystem : MonoBehaviour
{
    public Image[] hearts;
    public Sprite[] heartAssets;

    private void Start()
    {

    }
    private void OnEnable()
    {
        EventManager.Instance.updateHealth += UpdateHealth;
        EventManager.Instance.changeHealthIcon+= ChangeHealthIcon;
    }
    private void OnDisable()
    {
        EventManager.Instance.updateHealth -= UpdateHealth;
        EventManager.Instance.changeHealthIcon -= ChangeHealthIcon;
    }

    void Update()
    {
    }
    private void ChangeHealthIcon()
    {
        if (GameManager.Instance.CurrentEra == Era.Space)
        {
            for (int i = 0; i < hearts.Length; i++)
            {
                hearts[i].sprite = heartAssets[0];
            }
        }
        else if(GameManager.Instance.CurrentEra == Era.Modern) {
            for(int i = 0; i < hearts.Length; i++)
            {
                hearts[i].sprite = heartAssets[1];
            }
        }
        else if (GameManager.Instance.CurrentEra == Era.Stone)
        {
            for (int i = 0; i < hearts.Length; i++)
            {
                hearts[i].sprite = heartAssets[2];
            }
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
