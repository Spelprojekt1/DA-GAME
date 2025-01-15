using UnityEngine;

public class RandomPlacementAndRotation : MonoBehaviour
{
    [Header("Sphere Parameters")]
    public float sphereRadius = 5f; // Radius of the sphere
    public Color gizmoColor = Color.green; // Color of the sphere gizmo

    [Header("Placement and Rotation Settings")]
    public GameObject[] objectsToPlace; // Prefabs to be placed
    public int numberOfObjects = 10; // Total number of objects to place
    public int rotationSpeed = 50; // Speed of the angular rotation

    private readonly System.Collections.Generic.List<GameObject> spawnedObjects = new(); // Track spawned objects

    void OnValidate()
    {
        if (sphereRadius < 0) sphereRadius = 0;
        if (numberOfObjects < 0) numberOfObjects = 0;
        if (rotationSpeed < 0) rotationSpeed = 0;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawWireSphere(transform.position, sphereRadius);
    }

    private void OnEnable()
    {
        PlaceObjects();
    }

    private void OnDisable()
    {
        ClearObjects();
    }

    private void PlaceObjects()
    {
        if (objectsToPlace == null || objectsToPlace.Length == 0)
        {
            Debug.LogWarning("No objects assigned to place.");
            return;
        }

        int objectCount = objectsToPlace.Length;

        for (int i = 0; i < numberOfObjects; i++)
        {
            GameObject prefabToPlace = objectsToPlace[i % objectCount]; // Select objects evenly
            if (prefabToPlace != null)
            {
                // Calculate a random position within the sphere volume
                Vector3 randomPosition = transform.position + Random.insideUnitSphere * sphereRadius;

                // Instantiate the object
                GameObject spawnedObject = Instantiate(prefabToPlace, randomPosition, Quaternion.identity);

                // Add RandomRotation component for angular movement
                RandomRotation randomRotation = spawnedObject.AddComponent<RandomRotation>();
                randomRotation.rotationSpeed = rotationSpeed;

                // Assign a random rotation direction
                randomRotation.angularDirection = Random.onUnitSphere;

                spawnedObjects.Add(spawnedObject);
            }
        }

        Debug.Log($"Placed {numberOfObjects} objects randomly with angular rotation within the sphere volume.");
    }

    private void ClearObjects()
    {
        foreach (GameObject obj in spawnedObjects)
        {
            if (obj != null)
            {
                DestroyImmediate(obj);
            }
        }

        spawnedObjects.Clear();
        Debug.Log("Cleared all spawned objects.");
    }
}

public class RandomRotation : MonoBehaviour
{
    [HideInInspector] public Vector3 angularDirection; // Random direction for rotation
    [HideInInspector] public int rotationSpeed; // Speed of rotation

    void Update()
    {
        transform.Rotate(angularDirection * rotationSpeed * Time.deltaTime);
    }
}
