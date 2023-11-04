using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public static int FIRE_DAMAGE = 1;
    public static int ENEMY_DAMAGE = 2;

    public int source;

    public GameObject BrickDeathEffect;
    public GameObject RandomPowerUp;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        { 
            other.GetComponent<Bomberman>().Damage(source);
        }
        if (other.tag == "Enemy")
        {
            other.GetComponent<Enemy>().Damage(source);
        }
        if (other.TryGetComponent(out Brick brick) && source == 1)
        {
            if (brick.hiddenPowerUp != null)
                brick.hiddenPowerUp.gameObject.SetActive(true);

            Destroy(other.gameObject);
            Instantiate(BrickDeathEffect, transform.position, transform.rotation);
        }
    }

}
