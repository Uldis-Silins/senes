using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShroomSpawnController : MonoBehaviour
{
    public GameObject[] goodShroomPrefabs;
    public GameObject[] badShrommPrefabs;
    public int goodShroomCount = 100;
    public int badShroomCount = 50;

    public LayerMask groundLayers;

    public Color groundColor1;
    public Color groundColor2;

    [ExecuteInEditMode]
    public void SpawnShrooms()
    {
        ClearShrooms();

        RaycastHit hit;

        int spawnedCount = 0;
        float breaker = 0;

        while(spawnedCount < goodShroomCount && breaker < 100000)
        {
            breaker++;
            if (Physics.Raycast(Vector3.up * 50 + new Vector3(Random.insideUnitCircle.x * 100f, 0f, Random.insideUnitCircle.y * 100f), -Vector3.up, out hit, 100f, groundLayers))
            {
                Debug.Log("hit " + hit.collider.gameObject.layer);
                if (hit.collider.gameObject.layer != LayerMask.NameToLayer("Water"))
                {
                    var tex = hit.transform.GetComponent<Renderer>().material.mainTexture as Texture2D;
                    var pixelUV = hit.textureCoord;
                    pixelUV.x *= tex.width;
                    pixelUV.y *= tex.height;
                    var col = tex.GetPixel((int)pixelUV.x, (int)pixelUV.y);

                    if (Mathf.Abs(col.g - groundColor1.g) < 0.1f || Mathf.Abs(col.g - groundColor2.g) < 0.1f)
                    {
                        var instance = Instantiate(goodShroomPrefabs[Random.Range(0, goodShroomPrefabs.Length)], hit.point, Quaternion.Euler(-90f, 0f, 0f));
                        instance.transform.parent = transform;
                        spawnedCount++;
                    }
                }
            }
        }
    }

    public void ClearShrooms()
    {
        Stack<GameObject> childs = new Stack<GameObject>();

        if(transform.childCount > 0)
        {
            foreach (Transform shroom in transform)
            {
                childs.Push(shroom.gameObject);        
            }
        }

        while(childs.Count > 0)
        {
            DestroyImmediate(childs.Pop());
        }
    }
}
