using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudController : MonoBehaviour
{
    public Canvas hudCanvas;
    public Transform mainCam;

    public float hudDistance = 50f;

    public RectTransform[] hudTransforms;

    private Vector3 m_hudOffset;

    private void Awake()
    {
        m_hudOffset = hudCanvas.transform.position - mainCam.transform.position;
    }

    private void LateUpdate()
    {
        hudCanvas.transform.position = Vector3.Lerp(hudCanvas.transform.position, mainCam.position, 10f * Time.deltaTime);
        hudCanvas.transform.rotation = Quaternion.Slerp(hudCanvas.transform.rotation, mainCam.transform.rotation, 10f * Time.deltaTime);
    }
}
