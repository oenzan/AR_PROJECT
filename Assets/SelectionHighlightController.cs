// Assets/Scripts/FX/SelectionHighlightController.cs
using UnityEngine;

public class SelectionHighlightController : MonoBehaviour
{
    IHighlighter last;

    void OnEnable()  { SelectedContext.OnChanged += HandleChanged; }
    void OnDisable() { SelectedContext.OnChanged -= HandleChanged; }

    void HandleChanged(IActions acts)
    {
        var mb = acts as MonoBehaviour;
        Debug.Log($"[SelectionHighlightController] Selection changed to: {(mb != null ? mb.name : "(none)")}");
        // Eski highlight'ı kapat
        if (last != null) last.SetHighlight(false);
        last = null;

        if (mb == null) return;

        // Objede IHighlighter uygulayan component var mı?
        last = FindHighlighterOn(mb.gameObject);

        // Yoksa default olarak GlowHighlighter ekleyelim (veya hiç ekleme istersen bu satırı kaldır)
        if (last == null)
            return;

        last.SetHighlight(true);
    }

    static IHighlighter FindHighlighterOn(GameObject go)
    {
        // Interface implement eden ilk MonoBehaviour'u bul
        var mbs = go.GetComponents<MonoBehaviour>();
        foreach (var mb in mbs)
            if (mb is IHighlighter ih) return ih;
        return null;
    }
}
