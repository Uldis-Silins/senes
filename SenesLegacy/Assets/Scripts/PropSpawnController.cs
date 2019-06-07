using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropSpawnController : MonoBehaviour
{
    [System.Serializable]
    public class SpawnPoint
    {
        public Transform positionTransform;
        public float radius;
    }
    public SpawnPoint[] spawnPoints;
    public GameObject[] prefabs;
    public int count = 100;
    public LayerMask hitMask;

    private int GROUND_LAYER;

    [ExecuteInEditMode]
    public void SpawnStuff()
    {
        GROUND_LAYER = LayerMask.NameToLayer("Ground");
        for (int i = 0; i < count; i++)
        {
            var point = spawnPoints[Random.Range(0, spawnPoints.Length)];
            var pos = GetLegitSpawnPosition(point.positionTransform.position, point.radius);

            if (pos != Vector3.zero)
            {
                var instance = Instantiate(prefabs[Random.Range(0, prefabs.Length)], pos, Quaternion.Euler(-90f, 0f, 0f));
            }
        }
    }

    private Vector3 GetLegitSpawnPosition(Vector3 centerPosition, float radius)
    {
        RaycastHit hit;
        if(Physics.Raycast(new Ray(centerPosition + new Vector3(Random.insideUnitCircle.x * radius, 0f, Random.insideUnitCircle.y * 100f), -Vector3.up), out hit, 100f, hitMask))
        {
            return hit.point;
        }

        return Vector3.zero;
    }
}
