using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSystem : MonoBehaviour
{
    public static bool buildAvailable;
    private BuildingDataForBuilder _buildingData;
    private GhostBuild _flyBuilding;
    private Camera _camera;
    [SerializeField] private int _buildAreaSizeX, _buildAreaSizeY;

    private void Start()
    {
        _camera = Camera.main;
    }

    public void StartBuild(BuildingDataForBuilder buildingData)
    {
        if (_flyBuilding != null) Destroy(_flyBuilding.gameObject);

        _buildingData = buildingData;
        _flyBuilding = Instantiate(_buildingData.ghostPrefab, new Vector3(0f, 5f, 0f), Quaternion.identity);
    }

    private void Update() 
    {
        if (_flyBuilding != null)
        {
            var groundPlane = new Plane(Vector3.up, Vector3.zero);
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (groundPlane.Raycast(ray, out float distance))
            {
                Vector3 hitPoint = ray.GetPoint(distance);

                _flyBuilding.transform.position = new Vector3(Mathf.Round(hitPoint.x) + (_buildingData.buildingSize.x % 2 == 0 ? 0.5f : 0f), 0, Mathf.Round(hitPoint.z) + (_buildingData.buildingSize.z % 2 == 0 ? 0.5f : 0f));

                buildAvailable = _flyBuilding.BuildingInBuilding();
                if (buildAvailable) buildAvailable = BuildingInBuildArea(_flyBuilding.transform.position, _buildingData.buildingSize);

                _flyBuilding.ChangeMaterialColor();
            }


            if (buildAvailable && Input.GetMouseButtonDown(0) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
            {
                Instantiate(_buildingData.realPrefab, _flyBuilding.transform.position, Quaternion.identity);
            }

            if (Input.GetMouseButtonDown(1))
            {
                Destroy(_flyBuilding.gameObject);
                _flyBuilding = null;
                _buildingData = null;
            }
        }
    }

    public bool BuildingInBuildArea(Vector3 buildingPosition, Vector3 buildingSize)
    {
        float halfAreaX = _buildAreaSizeX / 2f;
        float halfAreaY = _buildAreaSizeY / 2f;

        float halfBuildingX = buildingSize.x / 2f;
        float halfBuildingZ = buildingSize.z / 2f;

        float buildingMinX = buildingPosition.x - halfBuildingX;
        float buildingMaxX = buildingPosition.x + halfBuildingX;
        float buildingMinZ = buildingPosition.z - halfBuildingZ;
        float buildingMaxZ = buildingPosition.z + halfBuildingZ;

        bool isWithinX = buildingMinX >= -halfAreaX && buildingMaxX <= halfAreaX;
        bool isWithinZ = buildingMinZ >= -halfAreaY && buildingMaxZ <= halfAreaY;

        return isWithinX && isWithinZ;
    }
}
