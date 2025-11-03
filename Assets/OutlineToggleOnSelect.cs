using UnityEngine;

[RequireComponent(typeof(CubeActions))]
public class OutlineToggleOnSelect : MonoBehaviour, ISelectable
{
    public GameObject outline; // Outline child'ını buraya sürükle

    CubeActions actions;

    void Awake() { actions = GetComponent<CubeActions>(); if (outline) outline.SetActive(false); }

    public void OnSelect()
    {
        // Seçimi güncelle
        Debug.Log("[OutlineToggleOnSelect]: OnSelect");
        SelectedContext.Current = actions;
        outline.SetActive(true);
    }
}
