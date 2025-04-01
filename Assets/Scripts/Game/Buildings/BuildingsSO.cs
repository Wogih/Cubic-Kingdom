using UnityEngine;

[CreateAssetMenu(fileName = "New Building Data", menuName = "Building System/Building Data")]
public class BuildingDataForBuilder : ScriptableObject
{
    public GameObject realPrefab;
    public GhostBuild ghostPrefab;
    public Vector3 buildingSize;
}