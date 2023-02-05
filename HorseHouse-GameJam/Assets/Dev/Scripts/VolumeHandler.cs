using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer.Internal;
using UnityEngine;

public class VolumeHandler : MonoBehaviour
{
    [SerializeField] private GameObject[] volumes;


    // Start is called before the first frame update
    void Start()
    {
        EventManager.Instance.isTransitioning += Shift;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Shift()
    {
        switch (GameManager.Instance.CurrentEra)
        {
            case Era.Space:
                volumes[0].SetActive(true);
                volumes[1].SetActive(false);
                break;
            case Era.Stone:
                volumes[0].SetActive(false);
                volumes[1].SetActive(true);
                break;
            case Era.Modern:
                volumes[0].SetActive(false);
                volumes[1].SetActive(false);
                break;
        }
    }
}
