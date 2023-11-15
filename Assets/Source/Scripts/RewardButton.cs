using UnityEngine;
using UnityEngine.UI;
using YG;

public class RewardButton : MonoBehaviour
{
    [SerializeField] private int rewardID;

    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(ButtonClick);
    }

    private void ButtonClick()
    {
        YandexGame.RewVideoShow(rewardID);
    }

    private void OnDestroy()
    {
        button.onClick.RemoveAllListeners();
    }
}
