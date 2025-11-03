// CubeActions.cs
using UnityEngine;
using UnityEngine.Video;

public class CubeActions : BaseActions
{
    public MoveAlongCircle mover;
    public Animator animator;
    public VideoPlayer video;
    public override bool CanMove      => mover   != null;
    public override bool CanAnimate   => animator!= null;
    public override bool CanPlayMedia => video   != null;
    void Awake()
    {
        if (animator) animator.speed = 0f;
        if (mover) mover.enableMovement = false;
    }

    public override void ToggleMove()
    {
        if (!mover) return;
        mover.enableMovement = !mover.enableMovement;
        Debug.Log("Setting mover.enableMovement to " + mover.enableMovement);
        if (mover.enableMovement && animator) animator.speed = 0f;
    }

    public override void ToggleAnimation()
    {
        if (!animator) return;
        animator.speed = animator.speed > 0.1f ? 0f : 1f;
        Debug.Log("Setting animator.speed to " + animator.speed);

        // if (animator.speed > 0f && mover) mover.enabled = false;
    }
    public override void TogglePlayPause()
    {
        if (!video) return;

        if (video.isPlaying)
        {
            video.Pause();
            Debug.Log("[CubeActions] Video -> Pause");
        }
        else
        {
            video.Play();
            Debug.Log("[CubeActions] Video -> Play");
        }
    }
}
