using UnityEngine;
using TMPro;
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
        }
    }
}
