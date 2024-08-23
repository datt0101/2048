using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using Unity;
using UnityEngine;
using UnityEngine.UIElements;


public class UIManager: MonoBehaviour
{
    static public UIManager instance;
    [SerializeField] private List<BoxSlot> boxes;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TMP_Text score;
    [SerializeField] private TMP_Text best;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(gameObject);
    }


    public void UpdateTableUI()
    {
        int k = 0;
        for (int i = 0; i < Table.instance.Row; ++i)
        {
            for (int j = 0; j < Table.instance.Column; ++j)
            {
                if (Table.instance.NumberTable[i][j] == 0)
                {
                    boxes[k].EmptyBox();
                }
                else
                {
                    boxes[k].UpdateBox(Table.instance.NumberTable[i][j].ToString());
                }
                k++;
            }
        }
        UpdateScore();
        
    }
    public void TurnOn(GameObject panel)
    {
        //blackPanel.SetActive(true);
        panel.SetActive(true);
        panel.transform.localScale = Vector3.zero;
        panel.transform.DOScale(1f, .5f)
            .OnComplete(() =>
            {
                Time.timeScale = 0f;
            });
      
    }
    public void TurnOff(GameObject panel)
    {
        Time.timeScale = 1f;
        panel.transform.DOScale(0f, .5f).
            OnComplete(() =>
            {
                panel.SetActive(false);
            });
    
    }
    public void UpdateScore()
    {
        score.text = Table.instance.score.ToString();
        best.text = Table.instance.bestScore.ToString();
    }

}