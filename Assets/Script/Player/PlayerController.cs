using EventManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventManager.Model;
using UnityEngine.InputSystem;
using System.Runtime.InteropServices;


public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private GameObject m_hitEffect;

    private Player m_player;
    private bool Disable;

    bool IsGround
    {
        get
        {
            return transform.position.y <= -1;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        m_player = new Player
        {
            Animator = GetComponent<Animator>(),
            Position = transform.position,
            Velocity = Vector3.zero,
            Gravity = new Vector3(0, -0.1f, 0),
            OriginalPos = transform.position,
            PlayerHeight = transform.GetComponent<SpriteRenderer>().bounds.size.y,
            HitEffect = m_hitEffect
        };
        DisableControl();
        HideHitEffect();

        EventBus.Instance.StartListening(EventBusType.RESET_POSITION_PLAYER, ResetPosition);
        EventBus.Instance.StartListening(EventBusType.RESET_ANIMATION_PLAYER, ResetAnimation);
        EventBus.Instance.StartListening(EventBusType.PLAY_HURT_ANIMATION, PlayHurtAnimation);
        EventBus.Instance.StartListening(EventBusType.ENABLE_CONTROL_PLAYER, EnableControl);
        EventBus.Instance.StartListening(EventBusType.DISABLE_CONTROL_PLAYER, DisableControl);
        EventBus.Instance.StartListening(EventBusType.SHOW_HIT_EFFECT_PLAYER, ShowHitEffect);
        EventBus.Instance.StartListening(EventBusType.HIDE_HIT_EFFECT_PLAYER, HideHitEffect);
    }
    public bool GetMouseButtonUp(int button)
    {
        switch (button)
        {
            case 0:
                return Mouse.current.leftButton.wasReleasedThisFrame;
            case 1:
                return Mouse.current.rightButton.wasReleasedThisFrame;
            case 2:
                return Mouse.current.middleButton.wasReleasedThisFrame;
        }

        return false;
    }
    // Update is called once per frame
    void Update()
    {
        if(Disable)
        {
            return;
        }
        
        if (IsGround)
        {
            EventBus.Instance.FireEvent(EventBusType.RESTART_GAME);
            EventBus.Instance.FireEvent(EventBusType.SHOW_HIT_EFFECT_PLAYER);
            SoundManager.Instance.Play(SoundType.HIT);
            m_player.Hurt();
            m_player.Velocity = Vector3.zero;
        }

        if (GetMouseButtonUp(0))
        {
            SoundManager.Instance.Play(SoundType.FLAP);
            m_player.Fly();
        }

        transform.position += m_player.Velocity;
        m_player.Velocity += m_player.Gravity * Time.deltaTime;
    }

    public void ResetPosition()
    {
        transform.position = m_player.OriginalPos;
    }

    public void ResetAnimation()
    {
        m_player.ResetAnimation();
    }

    public void PlayHurtAnimation()
    {
        m_player.HurtAnimation();
    }

    private void DisableControl()
    {
        Disable = true;
    }

    private void EnableControl()
    {
        Disable = false;
    }

    private void ShowHitEffect()
    {
        m_player.ShowHitEffect();
    }

    private void HideHitEffect()
    {
        m_player.HideHitEffect();
    }
}