using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Highlighter : MonoBehaviour, IHighlighter
{
    [Header("Glow")]
    public Color glowColor = Color.cyan;
    public float baseIntensity = 2.0f;     // mobilde daha belirgin dursun
    public float pulseIntensity = 1.5f;
    public float pulseSpeed = 4f;

    Renderer rend;
    Material[] mats;
    bool active;
    float t;

    static readonly int EmissionColorID = Shader.PropertyToID("_EmissionColor");

    void Awake()
    {
        rend = GetComponent<Renderer>();
        // materials: her obje için instance kopya alır (bizim için OK)
        mats = rend.materials;

        // Emission varyantını zorla aç
        foreach (var m in mats)
        {
            if (!m) continue;
            m.EnableKeyword("_EMISSION");
            // Başlangıçta kapalı tut
            m.SetColor(EmissionColorID, Color.black);
        }
    }

    void OnDisable() => SetEmission(0f);

    void Update()
    {
        if (!active) return;
        t += Time.deltaTime * pulseSpeed;
        float k = baseIntensity + Mathf.Abs(Mathf.Sin(t)) * pulseIntensity;
        SetEmission(k);
    }

    void SetEmission(float intensity)
    {
        var col = glowColor * intensity; // HDR renk için intensity 2–5 iyi çalışır
        foreach (var m in mats)
        {
            if (!m) continue;
            m.SetColor(EmissionColorID, col);
        }
    }

    public void SetHighlight(bool on)
    {
        if (on == active) return;
        active = on;
        t = 0f;
        if (!active) SetEmission(0f);
    }
}
