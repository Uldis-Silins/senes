using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonCollider : MonoBehaviour
{
    public UnityEngine.UI.Button button;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            button.onClick.Invoke();
        }
    }
}
