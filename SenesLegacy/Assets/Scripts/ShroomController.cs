using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.UI;

public class ShroomController : MonoBehaviour
{
    public UnityEvent OnGoodShroomHit;
    public UnityEvent OnBadShroomHit;

    public List<Collider> goodShrooms;
    public List<Collider> badShrooms;

    public DeadShroom[] deadShrooms;

    public AudioSource goodShroomAudio;
    public AudioSource badShroomAudio;

    public Text scoreText;

    private int m_curDeadShroomIndex = 0;

    public int Score { get; private set; }

    public void HandleShroomHit(Collider hit, Vector3 hitPos)
    {
        if(goodShrooms.Contains(hit))
        {
            OnGoodShroomHit.Invoke();
            Score++;
            scoreText.text = Score.ToString();

            StartCoroutine(AddScoreFeedback());
        }
        else if(badShrooms.Contains(hit))
        {
            OnBadShroomHit.Invoke();

            if (Score > 0)
            {
                Score--;
                scoreText.text = Score.ToString();
                StartCoroutine(RemoveScoreFeedback());
            }
        }
        else
        {
            throw new System.Exception("Shroom not found");
        }

        hit.gameObject.SetActive(false);
        deadShrooms[m_curDeadShroomIndex % (deadShrooms.Length - 1)].Spawn(hit.transform.position, hit.transform.rotation, hitPos);
        m_curDeadShroomIndex++;
    }

    public void ToggleShrooms(bool enabled)
    {
        for (int i = 0; i < goodShrooms.Count; i++)
        {
            goodShrooms[i].gameObject.SetActive(enabled);
        }

        for (int i = 0; i < badShrooms.Count; i++)
        {
            badShrooms[i].gameObject.SetActive(enabled);
        }
    }

    private IEnumerator AddScoreFeedback()
    {
        OVRInput.SetControllerVibration(0.5f, 0.5f, OVRInput.Controller.RTouch);
        yield return new WaitForSeconds(0.15f);
        OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.RTouch);
        goodShroomAudio.Play();
    }

    private IEnumerator RemoveScoreFeedback()
    {
        OVRInput.SetControllerVibration(1f, 1f, OVRInput.Controller.All);
        yield return new WaitForSeconds(0.5f);
        OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.All);
        badShroomAudio.Play();
    }

    public void ResetScore()
    {
        Score = 0;
    }
}
