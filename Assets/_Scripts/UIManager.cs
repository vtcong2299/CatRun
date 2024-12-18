using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public Text txtScore;
    public Text txtScoreEnd;
    public Text txtFishbone;
    public Text txtFishboneEnd;
    public GameObject txtGO;
    public GameObject clickHere;
    public GameObject panelPauseGame;
    public GameObject panelHienThi;
    public GameObject panelStartGame;
    public GameObject panelEndGame;
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
        panelStartGame.SetActive(true);
        SetAllPanelFalse();
    }
    public void ChangeUIScore(double score)
    {
        txtScore.text = ""+ score;
    }
    public void ChangeUIScoreEnd(double score)
    {
        txtScoreEnd.text = ""+ score;
    }
    public void ChangeUIFishbone(int fishBone)
    {
        txtFishbone.text = "x " + fishBone;
    }
    public void ChangeUIFishboneEnd(int fishBone)
    {
        txtFishboneEnd.text = "x " + fishBone;
    }
    public void ClickPauseButton()
    {
        panelPauseGame.SetActive(true);
        GameManager.instance.SpeedPauseGame();
    }
    public void ClickXButton()
    {
        panelPauseGame.SetActive(false) ;
        GameManager.instance.SpeedResumeGame();
    }
    public void ClickButtonRun()
    {
        panelStartGame.SetActive(false);
        GameManager.instance.player.SetActive(true) ;
        StartGame();
    }
    public void ClickButtonRestart()
    {
        GameManager.instance.RestartGame();
        SetAllPanelFalse() ;
        StartGame() ;   
    }
    public void EndGame(double score, int fishBone)
    {
        panelEndGame.SetActive(true);
        ChangeUIFishboneEnd(fishBone);
        ChangeUIScoreEnd(score);
    }
    public void StartGame()
    {
        txtGO.SetActive(true);
        txtGO.LeanScale(Vector3.one * 0.5f, 2);
        LeanTween.alpha(txtGO, 0f, 2f)
            .setEase(LeanTweenType.easeInOutQuad)
            .setOnComplete(() =>
            {
                txtGO.SetActive(false);
                txtGO.LeanScale(Vector3.one, 0);
                LeanTween.alpha(txtGO, 1f, 0)
                    .setEase(LeanTweenType.easeInOutQuad);
                GameManager.instance.StartGame();
                panelHienThi.SetActive(true);
            });
    }
    public void SetAllPanelFalse()
    {
        txtGO.SetActive(false);
        panelPauseGame.SetActive(false);
        panelHienThi.SetActive(false);
        panelEndGame.SetActive(false);
    }
    public void animTxtClickHere()
    {
        LeanTween.scale(clickHere, clickHere.transform.localScale * 0.67f, 0.5f)
            .setEase(LeanTweenType.easeInOutQuad).setOnComplete(() =>
            {
                LeanTween.scale(clickHere, clickHere.transform.localScale / 0.67f, 0.5f)
                .setEase(LeanTweenType.easeInOutQuad);
            })
            .setLoopPingPong();
    }
}
