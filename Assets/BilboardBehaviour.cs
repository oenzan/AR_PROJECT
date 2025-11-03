using UnityEngine;

public class BilboardBehaviour : MonoBehaviour
{
    Camera cam;
    [SerializeField] bool lockVertical = true;   // yazı düz kalsın
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (!cam) { cam = Camera.main; if(!cam) return; }
        var forward = cam.transform.rotation * Vector3.forward;
        var up = lockVertical ? Vector3.up : cam.transform.rotation * Vector3.up;
        transform.rotation = Quaternion.LookRotation(forward, up);
    }

}