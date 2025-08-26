using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

[System.Serializable]
public class SavableVector3
{
    public float x, y, z;

    public SavableVector3(Vector3 vector3)
    {
        x = vector3.x; y = vector3.y; z = vector3.z;
    }
    public Vector3 ToVector3() => new Vector3(x, y, z);
}

[System.Serializable]
public class SavableQuaternion
{
    public float x, y, z;

    public SavableQuaternion(Quaternion quaternion)
    {
        Vector3 euler = quaternion.eulerAngles;
        x = euler.x; y = euler.y; z = euler.z;
    }

    public Quaternion ToQuaternion() => Quaternion.Euler(x, y, z);
}

[System.Serializable]
public class SaveData
{
    public SavableVector3 posistion;

    public SavableQuaternion rotation;
}
