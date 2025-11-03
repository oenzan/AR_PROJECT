using UnityEngine;

[RequireComponent(typeof(Animator))]
public class KyleActions : BaseActions {
    public Animator animator;
    MoveAlongCircle mover;
    public override bool CanMove      => true;               // (hareket sistemi yoksa)
    public override bool CanAnimate   => animator != null;
    public override bool CanPlayMedia => false;
    private static readonly int jumpTrigger = Animator.StringToHash("Jump");
    void Awake()
    {
        if (!animator) animator = GetComponent<Animator>();
        if (!mover) mover = GetComponent<MoveAlongCircle>();
    }
    public override void ToggleMove() {if (mover) mover.enableMovement = !mover.enableMovement; }
    public override void ToggleAnimation()
    {
        if (!animator) return;
        Debug.Log("[KyleActions] Triggering Jump animation");
        // Her seferinde sıfırla ve baştan başlat
        animator.ResetTrigger("Jump");
        animator.Play("JumpStart", 0, 0f);  // animasyonu baştan çal
    }
    public override void TogglePlayPause() {}
}
