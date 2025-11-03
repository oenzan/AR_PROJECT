// VideoToggleOnSelect.cs
using UnityEngine;
using UnityEngine.Video;

[RequireComponent(typeof(VideoPlayer))]
[RequireComponent(typeof(Collider))]
public class VideoToggleOnSelect : MonoBehaviour, ISelectable
{
    VideoPlayer vp;
    void Awake(){ vp = GetComponent<VideoPlayer>(); }

    public void OnSelect()
    {
        if (vp.isPlaying) vp.Pause(); else vp.Play();
        // İstersen seçimi de güncelle:
        var actions = GetComponent<CubeActions>();
        if (actions) SelectedContext.Current = actions;
    }
}
