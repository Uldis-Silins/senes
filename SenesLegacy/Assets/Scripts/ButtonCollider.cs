using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonCollider : MonoBehaviour
{
    public UnityEngine.UI.Button button;

    private float m_activationTime;

    private void OnEnable()
    {
        m_activationTime = Time.time;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Time.time - m_activationTime > 3f)
        {
            if (other.CompareTag("Player"))
            {
                button.onClick.Invoke();
            }
        }
    }
}
