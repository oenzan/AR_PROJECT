using UnityEngine;

public class KyleAnimationEvents : MonoBehaviour
{
    public Animator animator;
    [SerializeField] string jumpTrigger = "Jump";

    void Awake()
    {
        if (!animator) animator = GetComponent<Animator>();
    }

    // Bu metod animasyon klibindeki OnLand event'inden otomatik çağrılır
    public void OnLand()
    {
        Debug.Log("[KyleAnimationEvents] OnLand event -> reset trigger");
        if (animator)
            animator.ResetTrigger(jumpTrigger);
    }
}
