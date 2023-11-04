using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private void Awake()
    {
        SaveExtension.Override();
    }
}
