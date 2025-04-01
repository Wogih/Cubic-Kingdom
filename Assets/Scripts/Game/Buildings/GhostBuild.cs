using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBuild : MonoBehaviour
{
    private bool _buildingInBuilding;
    public GameObject ghostBuildingModel;
    private Renderer _rendererBuildingModel;

    private void Awake()
    {
        _rendererBuildingModel = ghostBuildingModel.GetComponent<Renderer>();
    }

    public void ChangeMaterialColor()
    {
        _rendererBuildingModel.material.color = BuildingSystem.buildAvailable ? new Color(0f, 1f, 0f, 0.8f) : new Color(1f, 0f, 0f, 0.8f);
    }

    private void OnTriggerStay(Collider other)
    {
        _buildingInBuilding = true;
    }

    private void OnTriggerExit(Collider other)
    {
        _buildingInBuilding = false;
    }

    public bool BuildingInBuilding()
    {
        return !_buildingInBuilding;
    }
}
