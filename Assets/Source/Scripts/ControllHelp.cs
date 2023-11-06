using System.Threading.Tasks;
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

    public async void ShowBasicHelp()
    {
        foreach (var b in basicElements)
        {
            b.SetActive(true);
        }
        await Task.Delay(15000);
        foreach (var b in basicElements)
        {
            b.SetActive(false);
        }
    }

    public async void ShowDetonateButton()
    {
        detonateButton.SetActive(true);
        await Task.Delay(15000);
        detonateButton.SetActive(false);
    }
}
