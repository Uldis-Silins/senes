using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catana : MonoBehaviour
{
    public ShroomController shroomController;

    public AudioSource hitAudioSource;
    public AudioSource swingAudioSource;

    public AudioClip[] hitClips;
    public AudioClip[] longSwingClips;
    public AudioClip[] shortSwingClips;

    public LayerMask shroomLayer;

    private Rigidbody m_rigidbody;

    private Vector3 m_prevDirection;

    private void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        var vel = OVRInput.GetLocalControllerVelocity(OVRInput.Controller.RTrackedRemote);

        if (!swingAudioSource.isPlaying || (Mathf.Sign(vel.x) != Mathf.Sign(m_prevDirection.x)) || (Mathf.Sign(vel.y) != Mathf.Sign(m_prevDirection.y)))
        {
            if (vel.magnitude > 1f)
            {
                swingAudioSource.clip = longSwingClips[Random.Range(0, longSwingClips.Length)];
                swingAudioSource.Play();
            }
            else if (vel.sqrMagnitude > 0.5f)
            {
                swingAudioSource.clip = shortSwingClips[Random.Range(0, shortSwingClips.Length)];
                swingAudioSource.Play();
            }
        }

        m_prevDirection = vel;

        RaycastHit hit;

        if(Physics.Raycast(transform.position, transform.forward, out hit, 1f, shroomLayer))
        {
            shroomController.HandleShroomHit(hit.collider, hit.point);
            hitAudioSource.clip = hitClips[Random.Range(0, hitClips.Length)];
            hitAudioSource.Play();
        }
    }
}
