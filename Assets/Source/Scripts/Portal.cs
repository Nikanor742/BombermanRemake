using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public Rigidbody2D rb;
    private bool active = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private IEnumerator NextLevel()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(1);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !active)
        {
            active = true;
            AudioPlayer.Instance.PlaySound(ESoundType.portal);
            var player = FindObjectOfType<Bomberman>();
            player.NextLevelState(transform.position);
            SaveExtension.player.level++;
            SaveExtension.Save();
            StartCoroutine(NextLevel());
        }
    }
}
