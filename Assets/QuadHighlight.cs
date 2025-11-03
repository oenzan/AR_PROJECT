using UnityEngine;

public class QuadHighlight : MonoBehaviour, IHighlighter
{
    [Tooltip("Parent altında 'Outline' adındaki child (MeshRenderer + URP/Unlit, Face=Back).")]
    public GameObject outline;

    void Reset()
    {
        if (!outline)
        {
            var t = transform.Find("Outline");
            if (t) outline = t.gameObject;
        }
        if (outline) outline.SetActive(false);
    }

    public void SetHighlight(bool on)
    {
        if (outline) outline.SetActive(on);
    }
}
