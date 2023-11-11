using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private GameObject heart;
    private List<Transform> hearts;


    public void InitHearts(int h)
    {
        if (h > 1)
        {
            hearts = new List<Transform>();
            for (int i = 0; i < h; i++)
            {
                hearts.Add(Instantiate(heart, transform).transform);
            }
        }
    }

    public void DecreaseHeart()
    {
        if (hearts != null)
        {
            if (hearts?.Count != 0)
            {
                Destroy(hearts[hearts.Count - 1].GetChild(0).gameObject);
                hearts.RemoveAt(hearts.Count - 1);
            }
        }
    }
}
