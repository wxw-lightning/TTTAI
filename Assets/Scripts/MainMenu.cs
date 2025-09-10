using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public Button StartButton;
    public Button QuitButton;
    public Toggle goFirst;
    public Toggle easy;
    public Toggle normal;
    public Toggle hard;

    // Start is called before the first frame update
    void Start()
    {
        StartButton.onClick.AddListener(() =>
        {
            GameManager.Instance.InitBoard();
            GameManager.Instance.StartNewGame();
        });
        QuitButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });
        goFirst.onValueChanged.AddListener(isOn =>
        {
            if (isOn)
            {
                GameManager.Instance.playerFirst = true;
            }
            else
            {
                GameManager.Instance.playerFirst = false;
            }
        });
        easy.onValueChanged.AddListener(isOn =>
        {
            if (isOn)
            {
                GameManager.Instance.aiLevel = GameManager.AILevel.Easy;
            }
        });
        normal.onValueChanged.AddListener(isOn =>
        {
            if (isOn)
            {
                GameManager.Instance.aiLevel = GameManager.AILevel.Normal;
            }
        });
        hard.onValueChanged.AddListener(isOn =>
        {
            if (isOn)
            {
                GameManager.Instance.aiLevel = GameManager.AILevel.Hard;
            }
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
