using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairBehavior : MonoBehaviour
{
    private void Awake()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.State == GameState.Paused)
        {
            Cursor.visible = true;
        }
        else
        {
            Cursor.visible = false;
            transform.position = Input.mousePosition;

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Cursor.visible = true;
            }
            else if (Input.GetKeyUp(KeyCode.Escape))
            {
                Cursor.visible = false;
            }
        }
    }
}
