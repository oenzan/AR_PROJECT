using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class MyAnimateMenu : MonoBehaviour
{
    [Header("UI")]
    public GameObject panel; // Paneli Inspector’dan bağla
    [SerializeField] Button moveBtn, playAnimBtn, playPauseBtn; // Inspector’dan ver
    void OnEnable()  { SelectedContext.OnChanged += HandleSelectionChanged; }
    void OnDisable() { SelectedContext.OnChanged -= HandleSelectionChanged; }
    void HandleSelectionChanged(IActions acts)
    {
        // Yeni seçim geldiğinde buton durumlarını güncelle
        RefreshButtons(acts);
    }
    void RefreshButtons(IActions acts = null)
    {
        acts ??= SelectedContext.Current;

        bool hasSel = acts != null;
        if (moveBtn)      moveBtn.interactable      = hasSel && acts.CanMove;
        if (playAnimBtn)  playAnimBtn.interactable  = hasSel && acts.CanAnimate;
        if (playPauseBtn) playPauseBtn.interactable = hasSel && acts.CanPlayMedia;
    }
    // Animate butonu
    public void ShowMenu()
    {
        if (panel) panel.SetActive(!panel.activeSelf);
    }

    public void HideMenu()
    {
        if (panel) panel.SetActive(false);
    }

    // Paneldeki butonlar
    public void OnClick_Move()
    {
        var target = SelectedContext.Current;
        if (target == null) { Debug.LogWarning("[AnimateMenu] No selection"); return; }
        target.ToggleMove();
        var mb = target as MonoBehaviour;
        Debug.Log($"[AnimateMenu] Toggled Move on {mb.name}");
    }

    public void OnClick_PlayAnim()
    {
        var target = SelectedContext.Current;
        if (target == null) { Debug.LogWarning("[AnimateMenu] No selection"); return; }
        target.ToggleAnimation();
        var mb = target as MonoBehaviour;
        Debug.Log($"[AnimateMenu] Toggled Animation on {mb.name}");
    }

    public void OnClick_PlayPause()
    {
        var target = SelectedContext.Current;
        if (target == null) { Debug.LogWarning("[AnimateMenu] No selection"); return; }
        target.TogglePlayPause();
        var mb = target as MonoBehaviour;
        Debug.Log($"[AnimateMenu] Toggled Play/Pause on {mb.name}");
    }
}
