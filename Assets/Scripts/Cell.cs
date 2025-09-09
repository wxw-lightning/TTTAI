using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{

    public int row;
    public int col;

    [HideInInspector] public Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    private void OnMouseDown()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.PlayerMove(row, col);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(() =>
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.PlayerMove(row, col);
            }
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
