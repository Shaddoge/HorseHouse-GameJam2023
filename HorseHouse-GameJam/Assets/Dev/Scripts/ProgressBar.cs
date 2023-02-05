using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public int maximum;
    public int current = 0;
    public Color startColor;
    public Color fillColor;
    public Sprite[] barBorder;
    private int barCounter = 0;
    [SerializeField] GameObject transition;
    [SerializeField] Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnEnable()
    {
        EventManager.Instance.enemyDeath += UpdateProgressBar;
    }
    private void OnDisable()
    {
        EventManager.Instance.enemyDeath -= UpdateProgressBar;
    }
    // Update is called once per frame
    void Update()
    {
        GetCurrentFill();
    }

    void GetCurrentFill()
    {
        slider.maxValue = maximum;
        slider.value = current;

        slider.fillRect.GetComponent<Image>().color = Color.Lerp(startColor, fillColor, slider.normalizedValue);
    }
    private void UpdateProgressBar(int progress, Vector2 position)
    {
        Debug.Log("Entered Progress Bar");
        if(current == maximum)
        {
            current = 0;
            if(maximum > 0)
            {
                maximum -= 10;
            }
            transition.SetActive(true);
            barCounter++;
            //EventManager.Instance.ChangePlayerUI(barCounter);
            StartCoroutine(ResetTransition());
            GameManager.Instance.ChangeEra();

        }
        current += progress;
    }

    IEnumerator ResetTransition()
    {
        yield return new WaitForSeconds(5.0f);
        transition.SetActive(false);
        this.gameObject.GetComponentInChildren<Image>().sprite = barBorder[barCounter];
        EventManager.Instance.ChangeHealthIcon();
    }
}
