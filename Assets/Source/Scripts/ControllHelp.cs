using System.Collections;
using UnityEngine;

public class ControllHelp : MonoBehaviour
{
    [SerializeField] private GameObject[] basicElements;
    [SerializeField] private GameObject detonateButton;

    public static ControllHelp Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private IEnumerator BasicHelp()
    {
        foreach (var b in basicElements)
        {
            b.SetActive(true);
        }
        yield return new WaitForSeconds(15);
        foreach (var b in basicElements)
        {
            b.SetActive(false);
        }
    }

    private IEnumerator DetonateButton()
    {
        detonateButton.SetActive(true);
        yield return new WaitForSeconds(15f);
        detonateButton.SetActive(false);
    }

    public void ShowBasicHelp()
    {
        StartCoroutine(BasicHelp());
    }

    public void ShowDetonateButton()
    {
        StartCoroutine(DetonateButton());
    }
}
