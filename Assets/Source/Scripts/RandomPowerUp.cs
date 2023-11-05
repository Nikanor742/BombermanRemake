using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPowerUp : MonoBehaviour
{
    public List<GameObject> PowerUps;
    int randItem;
    //[0]-Fire 5%
    //[1]-Bomb 5%
    //[2]-Boots 3%
    //[3]-Detonator 1%
    //[4]-NoClipBombs 3%
    //[5]-NoClipFire 1%
    //[6]-NoClipBrick 3%
    void Start()
    {
        randItem = Random.Range(1, 101);
        
        if (randItem == 100)
        {
            Instantiate(PowerUps[3], transform.position, transform.rotation);
        }
        if (randItem ==99)
        {
            Instantiate(PowerUps[5], transform.position, transform.rotation);
        }
        if(randItem>=93 && randItem <= 97)
        {
            Instantiate(PowerUps[0], transform.position, transform.rotation);
        }
        if (randItem >= 88 && randItem <= 92)
        {
            Instantiate(PowerUps[1], transform.position, transform.rotation);
        }
        if (randItem >= 85 && randItem <= 87)
        {
            Instantiate(PowerUps[4], transform.position, transform.rotation);
        }
        if (randItem >= 82 && randItem <= 84)
        {
            Instantiate(PowerUps[6], transform.position, transform.rotation);
        }
        if (randItem >= 79 && randItem <= 81)
        {
            Instantiate(PowerUps[2], transform.position, transform.rotation);
        }
        Destroy(gameObject);
    }
}
