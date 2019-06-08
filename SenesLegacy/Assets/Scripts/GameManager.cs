using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public ShroomController shroomController;
    public GameObject playerWeapon;
    public HudController hudController;
    public Transform playerTransform;

    public float gameTime = 180f;

    public UnityEngine.UI.Text timeremainingText;

    private float m_gameTimer = 0f;

    private List<Shroom> m_nearShrooms;

    private const float NEAR_SHROOM_DISTANCE = 10f;

    public bool IsGameActive { get; private set; }

    private void Start()
    {
        StartGame();
    }

    private void Update()
    {
        if(IsGameActive)
        {
            m_gameTimer -= Time.deltaTime;
            timeremainingText.text = System.TimeSpan.FromSeconds(m_gameTimer).ToString("g");

            if(m_gameTimer < 0f)
            {
                EndGame();
            }

            Collider[] nearShrooms;
            if((nearShrooms = Physics.OverlapSphere(playerTransform.position, NEAR_SHROOM_DISTANCE, 1 << LayerMask.NameToLayer("Shroom"))) != null)
            {
                for (int i = 0; i < nearShrooms.Length; i++)
                {
                    var shroom = nearShrooms[i].GetComponent<Shroom>();
                    shroom.isClose = true;
                    m_nearShrooms.Add(shroom);
                }
            }

            Stack<Shroom> farShrooms = new Stack<Shroom>();

            for (int i = 0; i < m_nearShrooms.Count; i++)
            {
                if(Vector3.Distance(playerTransform.position, m_nearShrooms[i].transform.position) > NEAR_SHROOM_DISTANCE)
                {
                    farShrooms.Push(m_nearShrooms[i]);
                }
            }

            while(farShrooms.Count > 0)
            {
                var shroom = farShrooms.Pop();
                shroom.isClose = false;
                m_nearShrooms.Remove(shroom);
            }
        }
    }

    public void StartGame()
    {
        m_gameTimer = gameTime;
        IsGameActive = true;
        playerWeapon.SetActive(true);
        shroomController.ToggleShrooms(true);
        hudController.hudCanvas.enabled = true;

        m_nearShrooms = new List<Shroom>();
    }

    public void EndGame()
    {
        IsGameActive = false;
        playerWeapon.SetActive(false);
        shroomController.ToggleShrooms(false);
        hudController.hudCanvas.enabled = false;
        Application.Quit();
    }
}
