using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public ShroomController shroomController;
    public GameObject playerWeapon;
    public HudController hudController;
    public Transform playerTransform;

    public float gameTime = 180f;

    public Text timeremainingText;
    public GameObject startScreen;
    public GameObject endScreen;

    public Text scoreText;
    public Text highScoreText;
    public Text newHigscoreText;

    private float m_gameTimer = 0f;

    private List<Shroom> m_nearShrooms;

    private int m_highscore;

    private const float NEAR_SHROOM_DISTANCE = 10f;

    public bool IsGameActive { get; private set; }

    private void Start()
    {
        //StartGame();
        shroomController.ToggleShrooms(false);
        playerWeapon.SetActive(false);

        if(!PlayerPrefs.HasKey("highscore"))
        {
            m_highscore = 0;
            PlayerPrefs.SetInt("highscore", m_highscore);
            PlayerPrefs.Save();
        }

        startScreen.SetActive(true);
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
        //hudController.hudCanvas.enabled = true;

        m_nearShrooms = new List<Shroom>();
    }

    public void EndGame()
    {
        IsGameActive = false;
        playerWeapon.SetActive(false);
        shroomController.ToggleShrooms(false);
        //hudController.hudCanvas.enabled = false;

        if (m_highscore < shroomController.Score)
        {
            newHigscoreText.enabled = true;
            PlayerPrefs.SetInt("highscore", shroomController.Score);
            PlayerPrefs.Save();

        }

        scoreText.text = shroomController.Score.ToString();
        highScoreText.text = m_highscore.ToString();

        shroomController.ResetScore();
        endScreen.SetActive(true);
    }
}
