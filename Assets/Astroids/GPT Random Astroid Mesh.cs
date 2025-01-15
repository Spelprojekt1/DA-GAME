//Written by AI
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode]
public class RandomMeshChanger : MonoBehaviour
{
    public Mesh[] meshes; // Array of meshes to choose from
    private MeshFilter meshFilter; // Reference to the MeshFilter component

    // Track assignment counts
    private static int[] meshAssignmentCounts;
    private static int totalMeshes;

    private void Awake()
    {
        // Initialize the MeshFilter reference
        meshFilter = GetComponent<MeshFilter>();
        if (meshFilter == null)
        {
            Debug.LogError("No MeshFilter found on the GameObject.");
        }

        if (meshes != null && meshAssignmentCounts == null)
        {
            InitializeMeshAssignmentCounts();
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
        EditorApplication.delayCall += SafeAssignBalancedMeshEditor;
#endif
    }

    private void Start()
    {
        if (Application.isPlaying && meshes.Length > 0)
        {
            if (meshAssignmentCounts == null)
            {
                InitializeMeshAssignmentCounts();
            }

            AssignBalancedMeshRuntime();
        }
    }

#if UNITY_EDITOR
    private void SafeAssignBalancedMeshEditor()
    {
        // Ensure the object is still valid before proceeding
        if (this == null || meshFilter == null)
        {
            return;
        }

        AssignBalancedMeshEditor();
    }

    private void AssignBalancedMeshEditor()
    {
        // Ensure the MeshFilter is initialized
        if (meshFilter == null)
        {
            meshFilter = GetComponent<MeshFilter>();
        }

        if (meshFilter != null && meshes.Length > 0)
        {
            int balancedIndex = GetBalancedMeshIndex();
            meshFilter.sharedMesh = meshes[balancedIndex]; // Use sharedMesh for editor changes
        }
    }
#endif

    private void AssignBalancedMeshRuntime()
    {
        if (meshFilter != null && meshes.Length > 0)
        {
            int balancedIndex = GetBalancedMeshIndex();
            meshFilter.mesh = meshes[balancedIndex]; // Use mesh for runtime changes
        }
    }

    private int GetBalancedMeshIndex()
    {
        int minCount = int.MaxValue;
        int balancedIndex = 0;

        // Find the mesh with the least assignments
        for (int i = 0; i < meshes.Length; i++)
        {
            if (meshAssignmentCounts[i] < minCount)
            {
                minCount = meshAssignmentCounts[i];
                balancedIndex = i;
            }
        }

        // Increment the count for the selected mesh
        meshAssignmentCounts[balancedIndex]++;
        totalMeshes++;

        return balancedIndex;
    }

    private void InitializeMeshAssignmentCounts()
    {
        meshAssignmentCounts = new int[meshes.Length];
        totalMeshes = 0;
    }
}
