using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public Button StartButton;
    public Toggle goFirst;

    // Start is called before the first frame update
    void Start()
    {
        StartButton.onClick.AddListener(() =>
        {
            GameManager.Instance.InitBoard();
            GameManager.Instance.StartNewGame();
            this.gameObject.SetActive(false);
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
