using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saves_PosRot : MonoBehaviour
{
    private List<Rigidbody> rigidbodies = new List<Rigidbody>();
    private SaveData saveData = new SaveData();

    private void Start()
    {
        rigidbodies.AddRange(GetComponentsInChildren<Rigidbody>());

        LoadPositions();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) SavePositions();
        if (Input.GetKeyDown(KeyCode.E)) StartCoroutine(LoadPositionsRoutione(1));
    }

    private void SavePositions()
    {
        saveData.obj.Clear();

        foreach(Rigidbody rigidbody in rigidbodies)
        {
            var id = rigidbody.GetComponent<SaveID>()?.UniqueID;
            if (string.IsNullOrEmpty(id)) continue;

            saveData.obj.Add(new SavebleTransform(id,false,rigidbody.transform.position, rigidbody.transform.rotation));
        }

        SaveSystem.Save(saveData);
    }

    private void LoadPositions()
    {
        saveData = SaveSystem.Load();
        if(saveData.obj.Count == 0)  return;

        foreach (Rigidbody rigidbody in rigidbodies)
        {
            var id = rigidbody.GetComponent<SaveID>()?.UniqueID;
            if (string.IsNullOrEmpty(id)) continue;

            var saved = saveData.obj.Find(o => o.id == id);
            if(saved != null)
            {
                rigidbody.position = saved.position.ToVector3();
                rigidbody.rotation = saved.rotation.ToQuaternion();

                rigidbody.velocity = Vector3.zero;
                rigidbody.angularVelocity = Vector3.zero;
            }
        }
    }

    private IEnumerator LoadPositionsRoutione(float duration)
    {
        if (saveData.obj.Count == 0) yield break;

        float progress = 0;

        Dictionary<Rigidbody, Vector3> startPosition = new Dictionary<Rigidbody,Vector3>();
        Dictionary<Rigidbody, Quaternion> startRotation = new Dictionary<Rigidbody, Quaternion>();

        Dictionary<Rigidbody, Vector3> targetPosition = new Dictionary<Rigidbody, Vector3>();
        Dictionary<Rigidbody, Quaternion> targetRotation = new Dictionary<Rigidbody, Quaternion>();

        foreach (Rigidbody rigidbody in rigidbodies)
        {
            var id = rigidbody.GetComponent<SaveID>()?.UniqueID;
            if (string.IsNullOrEmpty(id)) continue;

            var saved = saveData.obj.Find(o => o.id == id);
            if (saved != null)
            {
                startPosition[rigidbody] = rigidbody.position;
                startRotation[rigidbody] = rigidbody.rotation;

                targetPosition[rigidbody] = saved.position.ToVector3();
                targetRotation[rigidbody] = saved.rotation.ToQuaternion();

            }
        }
        while (progress < 1)
        {
            progress += Time.deltaTime / duration;

            foreach (Rigidbody rb in startPosition.Keys)
            {
                rb.position = Vector3.Lerp(startPosition[rb], targetPosition[rb], progress);

                rb.rotation = Quaternion.Slerp(startRotation[rb], targetRotation[rb], progress);
            }

            yield return null;
        }
    }
}
