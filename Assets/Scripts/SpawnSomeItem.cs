using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSomeItem : MonoBehaviour
{
    public GameObject itemPrefab;  // Drag your prefab here in the inspector
    public Transform spawnPoint;   // Assign a transform as a spawn point

    void Update()
    {
        // Check if the index trigger is pressed down on the right controller
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
        {
            SpawnItem();
        }
    }

    void SpawnItem()
    {
        if (itemPrefab != null && spawnPoint != null)
        {
            Instantiate(itemPrefab, spawnPoint.position, spawnPoint.rotation);
        }
    }

}