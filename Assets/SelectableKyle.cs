// Assets/Scripts/Interact/SelectableKyle.cs
using UnityEngine;

[RequireComponent(typeof(KyleActions))]
[RequireComponent(typeof(Collider))]
public class SelectableKyle : MonoBehaviour, ISelectable
{
    [SerializeField] KyleActions actions;

    void Reset()
    {
        if (!actions) actions = GetComponent<KyleActions>();
        int layer = LayerMask.NameToLayer("ARContent");
        if (layer != -1) gameObject.layer = layer;
    }

    void Awake()
    {
        if (!actions) actions = GetComponent<KyleActions>();
    }

    public void OnSelect()
    {
        if (!actions) { Debug.LogWarning($"[SelectableKyle] KyleActions missing on {name}"); return; }
        SelectedContext.Current = actions;   // SelectedContext: IActions olacak şekilde refactor edildi.
        Debug.Log($"[SelectableKyle] Selected: {gameObject.name}");
    }
}
