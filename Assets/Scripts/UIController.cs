using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour, IObserver
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI crystallText;
    [SerializeField] private Button backgroundButton;
    [SerializeField] private BackTexture backTexture;

    private Subject _subjects;
    public static int Score { get; private set; }

    private void Awake()
    {
        _subjects = new Subject();
        _subjects.Attach(GetComponent<SceneController>());
        _subjects.Attach(backTexture);
    }
    private void Start()
    {
        if (PlayerPrefs.HasKey("Crystall"))
        {
            crystallText.text = PlayerPrefs.GetInt("Crystall").ToString();
        }
        else crystallText.text = "0";
        Score = 0;
    }
    public void OnPlayGame()
    {
        mainMenu.SetActive(false);
        _subjects.state = GameEvent.TILE_NEW;
        _subjects.Notify();
    }
    public void OnDropTile()
    {
        _subjects.state = GameEvent.TILE_DROP;
        _subjects.Notify();
    }
    private void OnScoreChange() => scoreText.text = Score.ToString();
    private void ScoreAdd() => Score++;
    private void GameOver()
    {
        mainMenu.SetActive(true);
        backgroundButton.onClick.RemoveAllListeners();
        backgroundButton.onClick.AddListener(Restart);

        int crys = int.Parse(crystallText.text) + Score / 10;
        crystallText.text = crys.ToString();

        PlayerPrefs.SetInt("Crystall", crys);
    }
    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
    public void UpdateData(GameEvent state)
    {
        if (state == GameEvent.GAME_OVER)
            GameOver();
        if (state == GameEvent.SCORE_CHANGED)
        {
            ScoreAdd();
            OnScoreChange();
        }
    }
}
