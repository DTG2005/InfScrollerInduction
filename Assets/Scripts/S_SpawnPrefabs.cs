using System;
using UnityEngine;

public class S_SpawnPrefabs : MonoBehaviour
{
    public GameObject[] easyPrefabs;
    public GameObject[] mediumPrefabs;
    public GameObject player;
    public GameObject[] hardPrefabs;
    public GameObject blankPrefab;

    public GameObject currentFloor;
    public GameObject lastFloor;

    public float obstacleSpacing = 7.5f;
    public float generationDistance = 30.0f;
    public float generationOffset = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int counter = 0;
        if (player.transform.position.z >= currentFloor.transform.position.z){
            currentFloor = FindObjectsInRadius(currentFloor.transform, 15);
            GenerateObstacles(currentFloor, counter, lastFloor);
            counter++;
        }
    }

    //TODO: Make the spawner start spawning after it reaches the middle of the current prefab.
    void GenerateObstacles(GameObject floor, int counter, GameObject last)
    {
        // Calculate the player's position ahead to trigger generation
        float generationPoint = player.transform.position.z + generationDistance;

        // Calculate the point where old content should be removed
        float removalPoint = player.transform.position.z - generationOffset;

        // Generate obstacles if the player has moved past the generation point
        GameObject obstaclePrefab;
        if (counter != 3){
            obstaclePrefab = easyPrefabs[UnityEngine.Random.Range(0, easyPrefabs.Length)];
        } else {
            obstaclePrefab = blankPrefab;
            counter = 0;
        }
        GameObject object1 = Instantiate(obstaclePrefab, new Vector3(0, -0.01f, floor.transform.position.z + obstacleSpacing), Quaternion.identity);
        transform.position = new Vector3(0, 0, floor.transform.position.z + obstacleSpacing);
        floor = object1;
        
        try {    // You might want to deactivate or destroy old obstacles here
            // For simplicity, we'll just move the generator position back
            GameObject lastTemp = FindObjectsInRadius(last.transform, 15);
            Destroy(last);
            Debug.Log("Destroyed"+ last.name + "Object");
            lastTemp = last;
        } catch {

        }
    }
    GameObject FindObjectsInRadius(Transform center, float radius)
    {
        // Clear the console to make it easier to read
        Debug.ClearDeveloperConsole();

        // Find all colliders within the specified radius
        Collider[] hitColliders = Physics.OverlapSphere(center.position, radius);

        // Loop through the colliders and print their names
        foreach (Collider collider in hitColliders)
        {
            if (collider.transform.position.z > center.transform.position.z){
                return collider.gameObject;
            }
        }
        return center.gameObject;
    }
}