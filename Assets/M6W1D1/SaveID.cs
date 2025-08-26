using UnityEngine;

[ExecuteAlways]
public class SaveID : MonoBehaviour
{
    [SerializeField] private string uniqueID;

    public string UniqueID => uniqueID;

    private void Awake()
    {
        if (string.IsNullOrEmpty(uniqueID)) uniqueID = System.Guid.NewGuid().ToString();
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (string.IsNullOrEmpty(uniqueID)) uniqueID = System.Guid.NewGuid().ToString();
    }
#endif
}
