using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    //0 -Bomb
    //1 -Fire
    //2 -speed
    //3 -noclip wall
    //4 -noclip fire
    //5 -noclip bomb
    //6 -detonator
    public int Type;

    public float invincibilityTime;

    void Update()
    {
        if (invincibilityTime > 0)
        {
            invincibilityTime -= Time.deltaTime;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Fire" && invincibilityTime<=0)
        {
            Destroy(gameObject);
        }
     
    }
}
