//Written by AI
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CylindricalGizmo : MonoBehaviour
{
    [Header("Cylinder Dimensions")]
    public float radius = 1f; // Radius of the cylinder
    public float height = 2f; // Height of the cylinder

    [Header("Gizmo Settings")]
    public Color enabledColor = Color.green; // Gizmo color when the script is enabled
    public Color disabledColor = Color.red;  // Gizmo color when the script is disabled

    [Header("Object Spawning")]
    public GameObject objectToSpawn; // Prefab to spawn
    public int spawnCount = 1; // Number of objects to spawn
    public Transform spawnParent; // Optional parent for the spawned objects
    public Vector3 objectScale = Vector3.one; // Scale for the spawned objects
    public float minSpacing = 0.5f; // Minimum spacing between spawned objects

    private bool needsUpdate = false;
    private List<Vector3> spawnedPositions = new List<Vector3>(); // List to track spawned object positions

    private void OnEnable()
    {
        if (!Application.isPlaying)
        {
            ClearSpawnedObjects();
            SpawnObjects(true);
        }
    }

    private void Start()
    {
        if (Application.isPlaying)
        {
            ClearSpawnedObjects();
            SpawnObjects(false);
        }
    }

    private void OnValidate()
    {
        if (!Application.isPlaying)
        {
            needsUpdate = true;
        }
    }

    private void Update()
    {
        if (!Application.isPlaying && needsUpdate)
        {
            ClearSpawnedObjects();
            SpawnObjects(true);
            needsUpdate = false;
        }
    }

    private void OnDrawGizmos()
    {
        // Set the Gizmo color based on whether the script is enabled or disabled
        Gizmos.color = enabled ? enabledColor : disabledColor;

        // Apply rotation and draw the cylinder
        Matrix4x4 originalMatrix = Gizmos.matrix;
        Gizmos.matrix = transform.localToWorldMatrix;

        DrawCylinder(Vector3.zero, radius, height);

        // Reset Gizmo matrix to avoid affecting other Gizmos
        Gizmos.matrix = originalMatrix;
    }

    private void DrawCylinder(Vector3 pivotPosition, float radius, float height)
    {
        const int segments = 36; // Number of segments for the cylinder's base
        float angleIncrement = 360f / segments;

        Vector3 bottomCenter = pivotPosition; // Base starts at the pivot point
        Vector3 topCenter = pivotPosition + Vector3.up * height; // Extends upward

        // Draw the circular bases
        for (int i = 0; i < segments; i++)
        {
            float angleA = Mathf.Deg2Rad * (i * angleIncrement);
            float angleB = Mathf.Deg2Rad * ((i + 1) * angleIncrement);

            Vector3 pointA = new Vector3(Mathf.Cos(angleA), 0, Mathf.Sin(angleA)) * radius;
            Vector3 pointB = new Vector3(Mathf.Cos(angleB), 0, Mathf.Sin(angleB)) * radius;

            // Bottom circle (pivot point)
            Gizmos.DrawLine(bottomCenter + pointA, bottomCenter + pointB);

            // Top circle (above pivot point)
            Gizmos.DrawLine(topCenter + pointA, topCenter + pointB);

            // Vertical lines connecting the circles
            Gizmos.DrawLine(bottomCenter + pointA, topCenter + pointA);
        }
    }

    private void SpawnObjects(bool isEditMode)
    {
        if (objectToSpawn == null)
        {
            Debug.LogError("objectToSpawn is null. Please assign a prefab in the Inspector.");
            return;
        }

        // Ensure there is a parent for the spawned objects
        Transform parent = spawnParent != null ? spawnParent : this.transform;
        spawnedPositions.Clear(); // Reset the list of positions

        for (int i = 0; i < spawnCount; i++)
        {
            Vector3 localPosition;
            int attempts = 0;

            // Try to find a valid position that doesn't collide with other objects
            do
            {
                if (attempts > 100)
                {
                    Debug.LogWarning("Could not find a valid position for the object. Consider reducing spawn count or increasing cylinder size.");
                    return;
                }

                float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
                float verticalPosition = Random.Range(0.1f, 0.9f) * height; // Exclude bottom and top edges

                localPosition = new Vector3(Mathf.Cos(angle) * radius, verticalPosition, Mathf.Sin(angle) * radius);
                attempts++;
            } while (IsTooClose(localPosition));

            // Add the position to the list of spawned positions
            spawnedPositions.Add(localPosition);

            // Spawn the object
            SpawnObject(localPosition, parent, isEditMode);
        }
    }

    private bool IsTooClose(Vector3 position)
    {
        foreach (var spawnedPosition in spawnedPositions)
        {
            if (Vector3.Distance(position, spawnedPosition) < minSpacing)
            {
                return true; // Position is too close to an existing object
            }
        }
        return false; // Position is valid
    }

    private void SpawnObject(Vector3 localPosition, Transform parent, bool isEditMode)
    {
        GameObject spawnedObject;

#if UNITY_EDITOR
        if (isEditMode)
        {
            spawnedObject = Instantiate(objectToSpawn, parent);
        }
        else
#endif
        {
            spawnedObject = Instantiate(objectToSpawn, parent);
        }

        // Set the local position, rotation, and scale relative to the parent
        spawnedObject.transform.localPosition = localPosition;
        spawnedObject.transform.localRotation = Quaternion.identity;
        spawnedObject.transform.localScale = objectScale;
    }

    private void ClearSpawnedObjects()
    {
        Transform parent = spawnParent != null ? spawnParent : this.transform;

        foreach (Transform child in parent)
        {
#if UNITY_EDITOR
            if (!Application.isPlaying)
            {
                DestroyImmediate(child.gameObject);
            }
            else
#endif
            {
                Destroy(child.gameObject);
            }
        }
    }
}
