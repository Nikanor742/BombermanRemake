using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public static int FIRE_DAMAGE = 1;
    public static int ENEMY_DAMAGE = 2;

    public int source;

    public GameObject[] BrickDeathEffect;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        { 
            other.GetComponent<Bomberman>().Damage(source);
        }
        if (other.tag == "Enemy")
        {
            other.GetComponent<Enemy>().Damage(this);
        }
        if (other.TryGetComponent(out Brick brick) && source == 1)
        {
            if (brick.active)
            {
                brick.active = false;
                ScoreSystem.Instance.AddScore(EScoreType.brickExplosion, transform.position);
                if (brick.hiddenPowerUp != null)
                    brick.hiddenPowerUp.gameObject.SetActive(true);

                if (brick.hiddenPortal != null)
                    brick.hiddenPortal.rb.simulated = true;

                Destroy(other.gameObject);
                int brickIndex = Mathf.Clamp(SaveExtension.player.level / 10, 0, 1);
                Instantiate(BrickDeathEffect[brickIndex], brick.transform.position, transform.rotation);
            }
            
        }
    }

}
