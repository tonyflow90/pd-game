using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 
public class Item : MonoBehaviour
{
    public GameManager gameManager;
    public Rigidbody2D rigidbody2D;

    void OnTriggerEnter2D (Collider2D hitInfo)
    {
        PlayerController2D player = hitInfo.GetComponent<PlayerController2D>();
        if(hitInfo.name == "PlayerPlaceholder")
        {
            // Debug.Log(gameObject.name);
            // Debug.Log(hitInfo.name);

            if(gameObject.name.Contains("Pizza")) {
                gameManager.IncreasePizzaCount();
            }
            
            if(gameObject.name.Contains("Beverage")) {
                gameManager.IncreaseBeverageCount();
            }

            gameObject.GetComponent<Renderer>().enabled = false;

            // Sound
            AudioSource audio = gameObject.GetComponent<AudioSource>();
            audio.Play();
            
            Destroy(gameObject, audio.clip.length);
        }
    }
}