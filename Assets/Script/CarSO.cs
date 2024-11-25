using UnityEngine;

[CreateAssetMenu(fileName = "CarSO", menuName = "Scriptable Objects/CarSO")]

public class CarSO : ScriptableObject
{
    public int id;
    public string modelManufacturer;
    public string modelName;
    public int horsePower;
    public float modelWeight;
}
