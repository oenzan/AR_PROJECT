using UnityEngine;

public class MoveAlongCircle : MonoBehaviour
{
    public Vector3 localCenter = Vector3.zero;
    public float radius = 0.4f;
    public float angularSpeedDegrees = 90f;
    public Vector3 axis = new Vector3(0, 0, 1); // daire düzleminin normali (local)

    float angle = 0f;
    Vector3 a;                 // normalize eksen
    Vector3 startOffsetLocal;  // a'ya DİK başlangıç ofseti

    public bool enableMovement = false;
    void Start()
    {
        localCenter = transform.localPosition;
        a = axis.normalized; 

        startOffsetLocal = Perpendicular(a) * radius; // doğrudan dikini bul
    }

    void Update()
    {
        if (!enableMovement) return;
        angle = Mathf.Repeat(angle + angularSpeedDegrees * Time.deltaTime, 360f);
        var rot = Quaternion.AngleAxis(angle, a);
        var offset = rot * startOffsetLocal; // eksene dik vektörü eksen etrafında döndür
        transform.localPosition = localCenter + offset;
    }

    static Vector3 Perpendicular(Vector3 a)
    {
        a = a.normalized;
        if (a == Vector3.zero) return Vector3.right;

        if (Mathf.Abs(a.x) <= Mathf.Abs(a.y) && Mathf.Abs(a.x) <= Mathf.Abs(a.z))
            return new Vector3(0f, -a.z, a.y).normalized;
        else if (Mathf.Abs(a.y) <= Mathf.Abs(a.z))
            return new Vector3(-a.z, 0f, a.x).normalized;
        else
            return new Vector3(-a.y, a.x, 0f).normalized;
    }
}
