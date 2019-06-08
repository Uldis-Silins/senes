using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shroom : MonoBehaviour
{
    public AnimationCurve moveCurve;
    public AnimationCurve scaleCurve;
    public float animationTime = 0.42f;

    public bool isClose;

    private float m_animationTimer;
    private Vector3 m_startPosition;
    private Vector3 m_startScale;

    private Vector3 m_targetPos;
    private float yRand;

    private bool m_invertScale;

    private void Start()
    {
        m_startPosition = transform.position;
        m_startScale = transform.localScale;
        m_targetPos = m_startPosition;// + Vector3.up * Random.Range(0.5f, 1.2f);

        m_invertScale = Random.value < 0.5f;
    }

    private void Update()
    {
        if(m_animationTimer > animationTime)
        {
            m_startPosition = transform.position;
            RaycastHit hit;
            if (isClose)
            {
                if (Physics.Raycast(transform.position + (Vector3.up * 10f) + (new Vector3(Random.insideUnitCircle.x * Random.Range(5f, 10f), 0f, Random.insideUnitCircle.y * Random.Range(5f, 10f)) * (float)Random.Range((int)-1, 1)), -Vector3.up, out hit, 100f, 1 << LayerMask.NameToLayer("Ground")))
                {
                    m_targetPos = hit.point;// + Vector3.up * Random.Range(0.5f, 1.2f);
                }

                yRand = Random.Range(1.5f, 2.5f);
            }
            else
            {
                m_targetPos = m_startPosition;// = m_startPosition + Vector3.up * Random.Range(0.5f, 1.2f);
                yRand = 0f;
            }

            //yRand = isClose ? Random.Range(1.5f, 2.5f) : Random.Range(0.5f, 1.2f);
            m_animationTimer = 0f;
        }

        Vector3 targetPos = Vector3.Lerp(m_startPosition, m_targetPos, m_animationTimer / animationTime);
        targetPos.y += Mathf.Lerp(0f, yRand, moveCurve.Evaluate(m_animationTimer / animationTime));
        transform.position = targetPos;

        float t = m_invertScale ? 1 - scaleCurve.Evaluate(m_animationTimer / animationTime) : scaleCurve.Evaluate(m_animationTimer / animationTime);
        transform.localScale = Vector3.Lerp(m_startScale - Vector3.one * 0.25f, m_startScale + Vector3.one * 0.25f, t);
        m_animationTimer += Time.deltaTime;
    }
}
