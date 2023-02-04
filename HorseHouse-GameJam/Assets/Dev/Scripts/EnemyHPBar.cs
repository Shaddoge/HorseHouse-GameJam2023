using UnityEngine;
using UnityEngine.UI;
public class EnemyHPBar : MonoBehaviour
{
    public Slider slider;
    public Color low;
    public Color high;
    public Vector3 offset;

    public void SetHealth(float currHp, float maxHp)
    {
        slider.gameObject.SetActive(currHp < maxHp);
        slider.value = currHp;
        slider.maxValue = maxHp;

        slider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(low, high, slider.normalizedValue);
    }
    void Update()
    {
        //slider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + offset);
    }
}
