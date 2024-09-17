using EventManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : SingletonMonoBehaviour<ScoreManager>
{
    [SerializeField]
    private Image[] m_scores;

    [SerializeField]
    private Sprite[] m_scoreSprites;

    private int m_playerScore;

    // Start is called before the first frame update
    void Start()
    {
        m_playerScore = 0;

        foreach(Image s in m_scores)
        {
            s.gameObject.SetActive(false);
        }
        m_scores[0].gameObject.SetActive(true);
        ChangeDigitScoreLength(1);
    }

    public void Add()
    {
        m_playerScore++;
    }

    public void Reset()
    {
        m_playerScore = 0;
        Display();
        ChangeDigitScoreLength(1);
    }

    public void Display()
    {
        var digit1 = m_playerScore / 10;
        var digit2 = m_playerScore % 10;

        if (digit1 == 0)
        {
            m_scores[0].sprite = m_scoreSprites[digit2];
        }
        else
        {
            ChangeDigitScoreLength(2);
            m_scores[0].sprite = m_scoreSprites[digit1];
            m_scores[1].sprite = m_scoreSprites[digit2];
        }
    }

    public void ChangeDigitScoreLength(int length)
    {
        if(length == 1)
        {
            foreach (Image s in m_scores)
            {
                s.gameObject.SetActive(false);
            }

            m_scores[0].gameObject.SetActive(true);
            var pos = m_scores[0].rectTransform.anchoredPosition;
            pos = new Vector2(0, pos.y);
            m_scores[0].rectTransform.anchoredPosition = pos;
        }
        else
        {
            foreach (Image s in m_scores)
            {
                s.gameObject.SetActive(true);
            }

            var halfWidth = m_scores[0].rectTransform.rect.width / 2;

            var pos1 = m_scores[0].rectTransform.anchoredPosition;
            pos1 = new Vector2(-halfWidth, pos1.y);
            m_scores[0].rectTransform.anchoredPosition = pos1;

            var pos2 = m_scores[1].rectTransform.anchoredPosition;
            pos2 = new Vector2(halfWidth, pos2.y);
            m_scores[1].rectTransform.anchoredPosition = pos2;
        }
    }
}