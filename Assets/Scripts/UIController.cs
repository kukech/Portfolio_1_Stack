using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    private SceneController sceneController;
    private void Start()
    {
        sceneController = GetComponent<SceneController>();
    }
    public void OnPlayGame()
    {
        mainMenu.SetActive(false);
        sceneController.NewTile();
    }
    public void OnDropTile()
    {
        sceneController.DropTile();
    }
}
