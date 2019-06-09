using UnityEngine;
using System.Collections;

public class DeadShroom : MonoBehaviour
{
    public ParticleSystem particles;

    private Vector3[] m_partPositions;

    private float m_aliveTimer = 3f;

    private bool m_spawned;
    private int m_spawnedFrame;

    private Vector3 m_hitPos;

    private void Awake()
    {
        m_partPositions = new Vector3[transform.childCount];

        for (int i = 0; i < m_partPositions.Length; i++)
        {
            m_partPositions[i] = transform.GetChild(i).localPosition;
        }
    }

    [ExecuteInEditMode]
    public void Spawn(Vector3 position, Quaternion rotation, Vector3 hitPos)
    {
        gameObject.SetActive(true);

        for (int i = 0; i < m_partPositions.Length; i++)
        {
            transform.GetChild(i).localPosition = m_partPositions[i];
            var rb = transform.GetChild(i).GetComponent<Rigidbody>();

            if (rb)
            {
                rb.velocity = Vector3.zero;
            }
        }

        transform.SetPositionAndRotation(position, rotation);

        m_spawned = true;
        m_spawnedFrame = Time.frameCount;
        m_aliveTimer = 3f;
        m_hitPos = hitPos;
        particles.Play();
    }

    private void Update()
    {
        if(m_spawned)
        {
            m_aliveTimer -= Time.deltaTime;

            if(Time.frameCount - m_spawnedFrame == 1)
            {
                var hitBodies = Physics.OverlapSphere(transform.position, 1f, 1 << LayerMask.NameToLayer("ShroomPart"));

                for (int i = 0; i < hitBodies.Length; i++)
                {
                    var rb = hitBodies[i].GetComponent<Rigidbody>();

                    if (rb)
                    {
                        rb.AddExplosionForce(750f, m_hitPos, 75f);
                    }
                }
            }

            if(m_aliveTimer <= 0f)
            {
                gameObject.SetActive(false);
                m_spawned = false;
            }
        }
    }
}
