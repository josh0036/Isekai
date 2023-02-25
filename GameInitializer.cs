using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    public Terrain terrainPrefab;

    void Start()
    {
        // Create a new terrain object from the prefab
        Terrain terrain = Instantiate(terrainPrefab);
        terrain.transform.position = new Vector3(0, 0, 0); // Set the terrain's position
    }
}
