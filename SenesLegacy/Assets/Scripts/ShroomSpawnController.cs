using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShroomSpawnController : MonoBehaviour
{
    public GameObject[] goodShroomPrefabs;
    public GameObject[] badShrommPrefabs;
    public int goodShroomCount = 100;
    public int badShroomCount = 50;
    public Color groundColor1;
    public Color groundColor2;

    public LayerMask groundLayers;

    private void Awake()
    {
        RaycastHit hit;

        int spawnedCount = 0;

        while(spawnedCount < goodShroomCount)
        {
            if (Physics.Raycast(Vector3.up * 50 + new Vector3(Random.insideUnitCircle.x * 100f, 0f, Random.insideUnitCircle.y * 100f), -Vector3.up, out hit, 100f, groundLayers))
            {
                if (hit.collider.gameObject.layer != LayerMask.NameToLayer("Water"))
                {
                    var tex = hit.transform.GetComponent<Renderer>().material.mainTexture as Texture2D;
                    var pixelUV = hit.textureCoord;
                    pixelUV.x *= tex.width;
                    pixelUV.y *= tex.height;
                    var col = tex.GetPixel((int)pixelUV.x, (int)pixelUV.y);
                    Debug.Log(col.ToString());

                    //if (col == groundColor1 || col == groundColor2)
                    {
                        var instance = Instantiate(goodShroomPrefabs[Random.Range(0, goodShroomPrefabs.Length)], hit.point, Quaternion.Euler(-90f, 0f, 0f));
                    }
                    spawnedCount++;
                }
            }
        }
        spawnedCount = 0;
        while (spawnedCount < badShroomCount)
        {
            if (Physics.Raycast(Vector3.up * 50 + new Vector3(Random.insideUnitCircle.x * 100f, 0f, Random.insideUnitCircle.y * 100f), -Vector3.up, out hit, 100f, groundLayers))
            {
                if (hit.collider.gameObject.layer != LayerMask.NameToLayer("Water"))
                {
                    var tex = hit.transform.GetComponent<Renderer>().material.mainTexture as Texture2D;
                    var pixelUV = hit.textureCoord;
                    pixelUV.x *= tex.width;
                    pixelUV.y *= tex.height;
                    var col = tex.GetPixel((int)pixelUV.x, (int)pixelUV.y);
                    Debug.Log(col.ToString());

                    //if (col == groundColor1 || col == groundColor2)
                    {
                        var instance = Instantiate(badShrommPrefabs[Random.Range(0, badShrommPrefabs.Length)], hit.point, Quaternion.Euler(-90f, 0f, 0f));
                    }
                    spawnedCount++;
                }
            }
        }
    }
}
