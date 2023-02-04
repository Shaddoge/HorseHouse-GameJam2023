using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode()]
public class ProgressBar : MonoBehaviour
{
    public int minimum;
    public int maximum;
    public int current = 0;
    public Image mask;
    public Image fill;
    public Color color;


    private void OnEnable()
    {
        EventManager.Instance.enemyDeath += UpdateProgressBar;
    }
    private void OnDisable()
    {
        EventManager.Instance.enemyDeath -= UpdateProgressBar;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetCurrentFill();
    }

    void GetCurrentFill()
    {
        float currentOffset = current - minimum;
        float maximumOffset = maximum - minimum;
        float fillAmount = currentOffset / maximumOffset;
        mask.fillAmount = fillAmount;

        fill.color = color;
    }
    private void UpdateProgressBar(int progress, Vector2 position)
    {
        if(current == maximum)
        {
            current = 0;
            if(maximum > 0)
            {
                maximum -= 10;
            }
            GameManager.Instance.ChangeEra();
        }
        current += progress;
    }
}
