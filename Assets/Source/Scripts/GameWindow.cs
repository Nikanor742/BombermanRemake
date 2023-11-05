using DG.Tweening;
using UnityEngine;

public abstract class GameWindow : MonoBehaviour
{
    [HideInInspector] public RectTransform rect;
    [HideInInspector] public Vector3 showPosition;
    public Vector3 hidePosition;
    public float showAndHideTime = 0.3f;
    public bool show = false;

    public void ShowWindow()
    {
        if (!show)
        {
            show = true;
            DOTween.Kill(rect);
            rect.DOLocalMove(showPosition, showAndHideTime).OnComplete(() =>
            {
                rect.DOPunchPosition(Vector2.up * 50, 0.3f, 10);
            });
        }

    }
    public virtual void HideWindow()
    {
        if (show)
        {
            show = false;
            DOTween.Kill(rect);
            rect.DOLocalMove(hidePosition, showAndHideTime).OnComplete(() =>
            {

            });
        }
    }
}