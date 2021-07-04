using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 
public class Enemy : MonoBehaviour
{
    public GameManager gameManager;

    void OnTriggerEnter2D (Collider2D hitInfo)
    {
        PlayerController2D player = hitInfo.GetComponent<PlayerController2D>();
        if(hitInfo.name == "PlayerPlaceholder")
        {
            // Debug.Log(gameObject.name);
            // Debug.Log(hitInfo.name);
            // gameManager.GameEnd();
        }
    }
}