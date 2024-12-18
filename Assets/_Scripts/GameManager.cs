using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public const float initSpeed = 5f;
    public const float initBonusSpeed = 0.01f;
    public float speedRoad;
    public float bonusSpeedRoad;
    public float speedRoadPauseGame;
    public float bonusSpeedRoadPauseGame;
    public double score;
    public int fishBone;
    public bool isStart;
    public GameObject player;
    private void OnEnable()
    {
        instance = this;
    }
    private void OnDisable()
    {
        instance = null;
    }
    private void Awake()
    {
        player.SetActive(false);
        InitItem();
        InitSpeed(); 
    }
    public void InitItem()
    {
        score = 0;
        fishBone = 0;
    }
    public void InitSpeed()
    {
        speedRoad = initSpeed;
        bonusSpeedRoad = initBonusSpeed;
    }
    private void Start()
    {
        UIManager.instance.animTxtClickHere();
    }
    private void Update()
    {
        speedRoad += Time.deltaTime * bonusSpeedRoad;
    }
    private void FixedUpdate()
    {
        CaculateScore();
    }
    public void CaculateScore()
    {
        score += Math.Round((Time.deltaTime * speedRoad * 10),0);
        UIManager.instance.ChangeUIScore(score);
    }    
    public void CheckPlayerAteFishBone(Collider other)
    {
        if (other.gameObject.tag == "Fishbone")
        {
            fishBone++;
            UIManager.instance.ChangeUIFishbone(fishBone);
            Destroy(other.gameObject);
        }
    }
    public void StartGame()
    {
        isStart = true;
        PlayerCtrl.instance.playerAnim.SetAnimRun();
        InitSpeed();
        InitItem();
    }
    public void ChangeEndGamePanel()
    {
        UIManager.instance.EndGame(score,fishBone);
    }
    public void RestartGame()
    {
        InitItem();
        UIManager.instance.ChangeUIFishbone(fishBone);
        PlayerCtrl.instance.PlayerRestartGame();
        SpawnMap.instance.SpawnMapRestart();
    }
    public void SpeedPauseGame()
    {
        speedRoadPauseGame = speedRoad;
        bonusSpeedRoadPauseGame = bonusSpeedRoad;
        speedRoad = 0;
        bonusSpeedRoad =0;
    }
    public void SpeedResumeGame()
    {
        speedRoad = speedRoadPauseGame;
        bonusSpeedRoad = bonusSpeedRoadPauseGame;
    }
    public void PlayerHit()
    {
        ChangeEndGamePanel();
        bonusSpeedRoad = 0;
        speedRoad = 0;
    }
}
