using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;
using UnityEngine.InputSystem; // yeni sistem

[RequireComponent(typeof(ARRaycastManager))]
public class AnchorExistingContent : MonoBehaviour
{
    [SerializeField] ARRaycastManager raycastManager;
    [SerializeField] ARAnchorManager  anchorManager;
    [SerializeField] ARPlaneManager   planeManager;
    [SerializeField] Transform        anchorRoot;
    [SerializeField] ScenarioManager        scenarioManager;

    public UnityEvent onPlaced;
    static readonly List<ARRaycastHit> hits = new();
    ARAnchor anchor;
    bool armed, placed = false;
    int frames;

    void SetAnchorRootVisible(bool on)
    {
        if (!anchorRoot) return;
        // Root’u açık tut, sadece çocukları aç/kapat
        foreach (Transform child in anchorRoot)
            child.gameObject.SetActive(on);
    }
    void setGameObjectVisible(bool on, Transform transform)
    {
        if (!transform) return;
        // Root’u açık tut, sadece çocukları aç/kapat
        foreach (Transform child in transform)
            child.gameObject.SetActive(on);
    }
    void Awake(){
        if (!raycastManager) raycastManager = GetComponent<ARRaycastManager>();
        enabled = false;
        setGameObjectVisible(false, anchorRoot);
    }

    public void Arm()
    {
        Debug.Log("[AnchorExistingContent] Arm() called.");
        if (placed) return;
        armed = true; 
        frames = 0;
        enabled = true;
    }

    void Update()
    {
        if (!armed || placed) return;
        frames++;
        if (frames < 8) return;

        if (!InputUtils.TryGetScreenTap(out var screenPos)) return;

        if (!raycastManager || !anchorManager || !anchorRoot) return;
        if (!raycastManager.Raycast(screenPos, hits, TrackableType.PlaneWithinPolygon)) return;

        var hit   = hits[0];
        var plane = hit.trackable as ARPlane;
        if (!plane) return;

        anchor = anchorManager.AttachAnchor(plane, hit.pose);
        if (!anchor) return;

        anchorRoot.SetParent(anchor.transform, worldPositionStays:false);
        anchorRoot.localPosition = Vector3.zero;
        anchorRoot.localRotation = Quaternion.identity;
        anchorRoot.localScale    = Vector3.one;

        if (planeManager)
        {
            planeManager.requestedDetectionMode = PlaneDetectionMode.None;
            foreach (var p in planeManager.trackables) p.gameObject.SetActive(false);
        }
        scenarioManager.VisibleCurrentScenario();
        placed = true;
        armed  = false;
        enabled = false;
        onPlaced?.Invoke();
    }
}
