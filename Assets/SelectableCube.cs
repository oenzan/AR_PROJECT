// Assets/Scripts/Interact/SelectableCube.cs
using UnityEngine;

[RequireComponent(typeof(CubeActions))]
[RequireComponent(typeof(Collider))]
public class SelectableCube : MonoBehaviour, ISelectable
{
    [SerializeField] CubeActions actions;

    void Reset()
    {
        if (!actions) actions = GetComponent<CubeActions>();
        int layer = LayerMask.NameToLayer("ARContent");
        if (layer != -1) gameObject.layer = layer;
    }

    void Awake()
    {
        if (!actions) actions = GetComponent<CubeActions>();
    }

    public void OnSelect()
    {
        if (!actions) { Debug.LogWarning($"[SelectableCube] CubeActions missing on {name}"); return; }
        SelectedContext.Current = actions;
        Debug.Log($"[SelectableCube] Selected: {gameObject.name}");
    }
}
