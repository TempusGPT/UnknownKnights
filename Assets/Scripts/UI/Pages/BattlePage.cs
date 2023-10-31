using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattlePage : Page
{
    private readonly State<int> timeLeft = new();
    private readonly State<float> greenTeamScore = new(1f);
    private readonly State<float> redTeamScore = new(1f);

    [SerializeField]
    private TextMeshProUGUI timer;

    [SerializeField]
    private Image greenTeamHealth;

    [SerializeField]
    private Image redTeamHealth;

    [SerializeField]
    private CharacterSpawner characterSpawner;

    [SerializeField]
    private BattleReferee referee;

    private void Awake()
    {
        timeLeft.OnValueChanged += (time) =>
        {
            var minutes = time / 60;
            var seconds = time % 60;
            timer.text = $"{minutes:00}:{seconds:00}";
        };

        greenTeamScore.OnValueChanged += (score) => greenTeamHealth.fillAmount = score;
        redTeamScore.OnValueChanged += (score) => redTeamHealth.fillAmount = score;
        referee.OnTimeChanged += (time) => timeLeft.Value = time;
    }

    public override UniTask Hide()
    {
        gameObject.SetActive(false);
        return UniTask.CompletedTask;
    }

    public override UniTask Show()
    {
        gameObject.SetActive(true);
        return UniTask.CompletedTask;
    }
}
