using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode]
public class RandomMeshChanger : MonoBehaviour
{
    public Mesh[] meshes; // Array of meshes to choose from
    private MeshFilter meshFilter; // Reference to the MeshFilter component

    private void Awake()
    {
        // Initialize the MeshFilter reference
        meshFilter = GetComponent<MeshFilter>();
        if (meshFilter == null)
        {
            Debug.LogError("No MeshFilter found on the GameObject.");
        }
    }

    private void OnValidate()
    {
        if (meshes == null || meshes.Length == 0)
        {
            Debug.LogWarning("No meshes assigned to RandomMeshChanger.");
            return;
        }

#if UNITY_EDITOR
        // Delay mesh assignment to avoid triggering restricted calls
        EditorApplication.delayCall += SafeAssignRandomMeshEditor;
#endif
    }

    private void Start()
    {
        if (Application.isPlaying && meshes.Length > 0)
        {
            // Assign a random mesh at runtime
            AssignRandomMeshRuntime();
        }
    }

#if UNITY_EDITOR
    private void SafeAssignRandomMeshEditor()
    {
        // Ensure the object is still valid before proceeding
        if (this == null || meshFilter == null)
        {
            return;
        }

        AssignRandomMeshEditor();
    }

    private void AssignRandomMeshEditor()
    {
        // Ensure the MeshFilter is initialized
        if (meshFilter == null)
        {
            meshFilter = GetComponent<MeshFilter>();
        }

        if (meshFilter != null && meshes.Length > 0)
        {
            // Assign a random mesh in editor mode safely
            int randomIndex = Random.Range(0, meshes.Length);
            meshFilter.sharedMesh = meshes[randomIndex]; // Use sharedMesh for editor changes
        }
    }
#endif

    private void AssignRandomMeshRuntime()
    {
        if (meshFilter != null && meshes.Length > 0)
        {
            // Assign a random mesh at runtime
            int randomIndex = Random.Range(0, meshes.Length);
            meshFilter.mesh = meshes[randomIndex]; // Use mesh for runtime changes
        }
    }
}
