using EventManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventManager.Model;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Button btnStart;

    [SerializeField]
    private GameObject m_middleGround;

    [SerializeField]
    private GameObject m_foreground;

    // Start is called before the first frame update
    void Awake()
    {
        btnStart.onClick.AddListener(StartGame);
        InitGame();
        EventBus.Instance.StartListening(EventBusType.HIDE_IGM, HideIGM);
        EventBus.Instance.StartListening(EventBusType.DISPLAY_IGM, DisplayIGM);
        EventBus.Instance.StartListening(EventBusType.RESTART_GAME, InitGame);
    }

    void InitGame()
    {
        DisplayIGM();
        EventBus.Instance.FireEvent(EventBusType.DISABLE_CONTROL_PLAYER);
        EventBus.Instance.FireEvent(EventBusType.STOP_SCROLL);
        EventBus.Instance.FireEvent(EventBusType.DISABLE_COLLISION);
    }

    void StartGame()
    {
        DisplayEnvironment();
        HideIGM();
        ScoreManager.Instance.Reset();
        EventBus.Instance.FireEvent(EventBusType.RESET_POSITION_PLAYER);
        EventBus.Instance.FireEvent(EventBusType.RESET_ANIMATION_PLAYER);
        EventBus.Instance.FireEvent(EventBusType.ENABLE_CONTROL_PLAYER);
        EventBus.Instance.FireEvent(EventBusType.RESET_SPAWN_BLOCK);
        EventBus.Instance.FireEvent(EventBusType.START_SCROLL);
        EventBus.Instance.FireEvent(EventBusType.ENABLE_COLLISION);
        EventBus.Instance.FireEvent(EventBusType.HIDE_HIT_EFFECT_PLAYER);
    }

    void DisplayIGM()
    {
        gameObject.SetActive(true);
    }

    void HideIGM()
    {
        gameObject.SetActive(false);
    }

    void DisplayEnvironment()
    {
        m_middleGround.SetActive(true);
        m_foreground.SetActive(true);
    }

    void HideEnvironment()
    {
        m_middleGround.SetActive(false);
        m_foreground.SetActive(false);
    }
}
