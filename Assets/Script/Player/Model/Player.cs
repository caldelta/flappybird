using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public Animator Animator { get; set; }
    public Vector3 Position { get; set; }
    public Vector3 Velocity { get; set; }
    public Vector3 Gravity { get; set; }
    public float PlayerHeight { get; set; }
    public Vector3 OriginalPos { get; set; }
    public GameObject HitEffect;

    public void Fly()
    {
        Velocity = new Vector3(0, 0.025f, 0);
        FlyAnimation();
    }

    public void Hurt()
    {
        HurtAnimation();
        Position = new Vector3(Position.x, -PlayerHeight, Position.z);
    }

    public void HurtAnimation()
    {
        Animator.SetTrigger(PlayerConst.HURT_ANIMATION);
    }

    public void FlyAnimation()
    {
        Animator.SetTrigger(PlayerConst.FLY_ANIMATION);
    }

    public void ResetAnimation()
    {
        Animator.Play("Base Layer.Idle", 0, 0.25f);
    }

    public void ShowHitEffect()
    {
        HitEffect.SetActive(true);
    }

    public void HideHitEffect()
    {
        HitEffect.SetActive(false);
    }
}
