using UnityEngine;

public class ScenarioManager : MonoBehaviour
{
    [SerializeField] Transform anchorRoot;
    [SerializeField] Transform scenario1Root;
    [SerializeField] Transform scenario2Root;
    public Transform Current { get; private set; }   // 🔹 Getter eklendi

    void Awake()
    {
        Current = scenario1Root;
        Debug.Log("[ScenarioManager] Awake: Initial scenario set to " + (Current ? Current.gameObject.name : "null"));
    }
    public void SelectScenario1() { if (scenario1Root) Apply(scenario1Root); }
    public void SelectScenario2() { if (scenario2Root) Apply(scenario2Root); }
    public void Toggle()
    {
        Debug.Log("[ScenarioManager] Toggling scenarios.");
        if (!scenario1Root || !scenario2Root) return;
        Apply(Current == scenario1Root ? scenario2Root : scenario1Root);
    }

    static void SetActiveDeep(Transform root, bool on)
    {
        if (!root) return;
        root.gameObject.SetActive(on);
        foreach (Transform child in root)
            SetActiveDeep(child, on);
    }

    void Apply(Transform active)
    {
        if (!active || active == Current) return;

        SetActiveDeep(scenario1Root, active == scenario1Root);
        SetActiveDeep(scenario2Root, active == scenario2Root);

        Current = active;
    }

    public void VisibleCurrentScenario()
    {
        if (!Current) return;
        SetActiveDeep(anchorRoot, true);
        if (scenario1Root == Current)
        {
            SetActiveDeep(scenario2Root, false);
            SetActiveDeep(scenario1Root, true);
        }
        else if (scenario2Root == Current)
        {
            SetActiveDeep(scenario1Root, false);
            SetActiveDeep(scenario2Root, true);
        }
    }
}
