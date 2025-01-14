using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    [Header("Gizmo Settings")]
    public Color gizmoColor = Color.green;
    public Vector3 cubeSize = new Vector3(5f, 5f, 5f);

    [Header("Spawn Settings")]
    public GameObject[] objectsToSpawn;
    public int spawnCount = 10;
    public float angularVelocityRange = 5f;

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawWireCube(transform.position, cubeSize);
    }

    public void SpawnObjects()
    {
        if (objectsToSpawn.Length == 0)
        {
            Debug.LogWarning("No objects assigned to spawn!");
            return;
        }

        for (int i = 0; i < spawnCount; i++)
        {
            Vector3 randomPosition = GetRandomPositionWithinCube();
            GameObject prefab = objectsToSpawn[Random.Range(0, objectsToSpawn.Length)];

            GameObject spawnedObject = Instantiate(prefab, randomPosition, Random.rotation);

            // Add Rigidbody if it doesn't exist and set properties
            Rigidbody rb = spawnedObject.GetComponent<Rigidbody>();
            if (rb == null)
            {
                rb = spawnedObject.AddComponent<Rigidbody>();
            }

            rb.angularVelocity = Random.insideUnitSphere * angularVelocityRange;
            rb.useGravity = false;

            // Disable collisions by setting isTrigger
            Collider collider = spawnedObject.GetComponent<Collider>();
            if (collider != null)
            {
                collider.isTrigger = true;
            }
        }
    }

    private Vector3 GetRandomPositionWithinCube()
    {
        Vector3 halfSize = cubeSize / 2f;
        return new Vector3(
            Random.Range(-halfSize.x, halfSize.x),
            Random.Range(-halfSize.y, halfSize.y),
            Random.Range(-halfSize.z, halfSize.z)) + transform.position;
    }
}
