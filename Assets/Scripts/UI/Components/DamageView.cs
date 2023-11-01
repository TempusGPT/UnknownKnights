using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class DamageView : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text;

    [SerializeField]
    private float duration;

    [SerializeField]
    private Vector2 offset;

    [SerializeField]
    private Vector2 randomness;

    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = transform as RectTransform;
    }

    public async UniTask Show(Transform target, float damage)
    {
        text.text = damage.ToString();

        var position =
            (Vector2)Camera.main.WorldToScreenPoint(target.position)
            + Random.insideUnitCircle * randomness;
        rectTransform.anchoredPosition = position;

        await rectTransform
            .DOAnchorPos(position + offset, duration)
            .SetEase(Ease.OutExpo)
            .SetUpdate(true)
            .AsyncWaitForCompletion();

        Destroy(gameObject);
    }
}
