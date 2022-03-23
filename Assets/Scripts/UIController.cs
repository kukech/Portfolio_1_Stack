using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private TextMeshProUGUI score;
    [SerializeField] private TextMeshProUGUI crystall;
    [SerializeField] private Button backgroundButton;
    private void Awake()
    {
        Messenger.AddListener(GameEvent.GAME_OVER, GameOver);
    }
    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.GAME_OVER, GameOver);
    }
    private void Start()
    {
        MainManager.Instance.LoadCrystall();
        crystall.text = MainManager.Crystall.ToString();
    }
    public void OnPlayGame()
    {
        mainMenu.SetActive(false);
        Messenger.Broadcast(GameEvent.TILE_NEW);
        OnScoreChanged();
    }
    public void OnDropTile()
    {
        Messenger<Action>.Broadcast(GameEvent.TILE_DROP, OnScoreChanged);
    }
    public void OnScoreChanged()
    {
        score.text = MainManager.Score.ToString();
    }
    public void GameOver()
    {
        mainMenu.SetActive(true);
        backgroundButton.onClick.RemoveAllListeners();
        backgroundButton.onClick.AddListener(Restart);
        crystall.text = MainManager.Crystall.ToString();
    }
    public void Restart()
    {
        MainManager.Score = 0;
        MainManager.Instance.SaveCrystall();
        SceneManager.LoadScene(0);
    }
}
