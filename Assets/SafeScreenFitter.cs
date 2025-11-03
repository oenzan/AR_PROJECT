// Assets/Scripts/UI/SafeAreaFitter.cs
using UnityEngine;

[ExecuteAlways]
public class SafeScreenFitter : MonoBehaviour
{
    [SerializeField] RectTransform target; // genelde UI/Root Panel
    Rect lastArea; Vector2 lastRes; ScreenOrientation lastOrient;

    void Reset() { target = GetComponent<RectTransform>(); }
    void OnEnable() => Apply();
    void Update()
    {
        if (Screen.safeArea != lastArea || Screen.width != lastRes.x || 
            Screen.height != lastRes.y || Screen.orientation != lastOrient)
            Apply();
    }

    void Apply()
    {
        if (!target) return;
        var sa = Screen.safeArea;
        var canvas = target.GetComponentInParent<Canvas>();
        if (!canvas || canvas.renderMode == RenderMode.WorldSpace) return;

        Rect r = sa;
        Vector2 anchorMin = r.position;
        Vector2 anchorMax = r.position + r.size;
        anchorMin.x /= Screen.width;  anchorMin.y /= Screen.height;
        anchorMax.x /= Screen.width;  anchorMax.y /= Screen.height;

        target.anchorMin = anchorMin;
        target.anchorMax = anchorMax;
        target.offsetMin = target.offsetMax = Vector2.zero;

        lastArea = sa; lastRes = new Vector2(Screen.width, Screen.height); lastOrient = Screen.orientation;
    }
}
