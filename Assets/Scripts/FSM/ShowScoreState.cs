using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShowScoreState : IState
{
    private GameManager gameManager;

    public ShowScoreState(GameManager gameManager)
    {
        this.gameManager = gameManager;
        gameManager.saveBtn.onClick.AddListener(RestartGame);
    }

    public void Enter()
    {
        int scoreBoxGreen =  gameManager.greenBox.GetComponentInChildren<BoxCounter>().GetScore();
        int scoreBoxRed = gameManager.redBox.GetComponentInChildren<BoxCounter>().GetScore();
        int scoreBoxBlue = gameManager.blueBox.GetComponentInChildren<BoxCounter>().GetScore();

        float puntaje = scoreBoxBlue * 1.2f + scoreBoxGreen * 1.5f + scoreBoxRed;

        gameManager.scoreValue.text = puntaje.ToString("F1");
        gameManager.scoreCanvas.SetActive(true);
    }

    public void Update()
    {

    }

    private void RestartGame()
    {
        gameManager.stateMachine.TransitionTo(gameManager.stateMachine.mainMenuState);
    }

    public void Exit()
    {
        gameManager.ClearIProductsOnScene();
        gameManager.scoreCanvas.SetActive(false);
    }
}
