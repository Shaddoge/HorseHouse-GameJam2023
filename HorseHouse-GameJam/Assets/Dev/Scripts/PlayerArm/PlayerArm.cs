using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PlayerArm : MonoBehaviour
{
    // References
    private Player player;

    public void Setup(Player player)
    {
        this.player = player;
    }

    void Update()
    {
        if (player == null) return;
        transform.right = (player.pointerPos - (Vector2)transform.position).normalized;
    }    
}
