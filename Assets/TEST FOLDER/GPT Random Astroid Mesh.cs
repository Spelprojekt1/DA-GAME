using UnityEngine;

[ExecuteInEditMode] // This makes the script run in the editor as well
public class RandomMeshChanger : MonoBehaviour
{
    public Mesh[] meshes; // Array of meshes to choose from
    private MeshFilter meshFilter; // Reference to the MeshFilter component

    void Awake()
    {
        // Get the MeshFilter component attached to the same GameObject
        meshFilter = GetComponent<MeshFilter>();
        if (meshFilter == null)
        {
            Debug.LogError("No MeshFilter found on the GameObject.");
        }
    }

    // This method is called when values are modified in the editor
    void OnValidate()
    {
        if (meshFilter != null && meshes.Length > 0)
        {
            // Choose a random mesh from the list and assign it to the MeshFilter
            int randomIndex = Random.Range(0, meshes.Length);
            meshFilter.mesh = meshes[randomIndex];
        }
    }

    // Optional: You can also use this in the start if you want the random mesh to apply when the game starts
    void Start()
    {
        OnValidate();
    }
}