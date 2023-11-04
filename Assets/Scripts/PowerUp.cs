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
    public EBonusType Type;

    private BoxCollider2D boxCollider;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.enabled = false;
    }
    private IEnumerator ActivateBonus()
    {
        boxCollider.enabled = false;
        yield return new WaitForSeconds(1f);
        boxCollider.enabled = true;
    }

    private void OnEnable()
    {
        StartCoroutine(ActivateBonus());
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Fire")
        {
            Destroy(gameObject);
        }
     
    }
}
