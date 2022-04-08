using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour, IObserver
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private TextMeshProUGUI score;
    [SerializeField] private TextMeshProUGUI crystall;
    [SerializeField] private Button backgroundButton;
    [SerializeField] private BackTexture backTexture;

    private Subject _subjects;
    
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
            crystall.text = PlayerPrefs.GetInt("Crystall").ToString();
        }
        else crystall.text = "0";
    }
    public void OnPlayGame()
    {
        mainMenu.SetActive(false);
        _subjects.state = GameEvent.TILE_NEW;
        _subjects.Notify();
        OnScoreChanged();
    }
    public void OnDropTile()
    {
        _subjects.state = GameEvent.TILE_DROP;
        _subjects.Notify();
    }
    public void OnScoreChanged() => score.text = MainManager.Score.ToString();
    private void GameOver()
    {
        mainMenu.SetActive(true);
        backgroundButton.onClick.RemoveAllListeners();
        backgroundButton.onClick.AddListener(Restart);
        int crys = int.Parse(crystall.text) + MainManager.Score / 10;
        crystall.text = crys.ToString();
        PlayerPrefs.SetInt("Crystall", crys);
    }
    public void Restart()
    {
        MainManager.Score = 0;
        SceneManager.LoadScene(0);
    }
    public void UpdateData(GameEvent state)
    {
        if(state == GameEvent.GAME_OVER)
            GameOver();
    }
}
