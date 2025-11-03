using UnityEngine;
using UnityEngine.EventSystems;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] Camera cam;                // XR Origin altındaki kamera
    [SerializeField] LayerMask selectionMask;   // "ARContent" layer'ı (seçilecek objelerin layer'ı)

    void Awake()
    {
        if (!cam) cam = Camera.main;
    }

    void Update()
    {
        // UI üstü tıklamaları yoksay
        if (EventSystem.current && EventSystem.current.IsPointerOverGameObject())
            return;

        // Ekrana dokunma/mouse click al
        if (!InputUtils.TryGetScreenTap(out var screenPos))
            return;
        Debug.Log($"Screen tap position: {screenPos}");
        // Raycast yap
        var ray = cam.ScreenPointToRay(screenPos);
        if (Physics.Raycast(ray, out var hit, 100f, selectionMask))
        {
            Debug.Log($"[SelectionManager] Selected: {hit.collider.name}");
            var selectable = hit.collider.GetComponentInParent<ISelectable>();
            if (selectable != null)
            {
                selectable.OnSelect();
                Debug.Log($"[SelectionManager] selectable.OnSelect()");
            }
            else
            {
                Debug.LogWarning($"[SelectionManager] No ISelectable on {hit.collider.name}");
            }
        }
    }
}
