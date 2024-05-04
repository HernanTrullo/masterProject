using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuState : IState
{
    private GameManager gameManager;

    public MainMenuState(GameManager gameManager) 
    {
        this.gameManager = gameManager;
        gameManager.startBtn.onClick.AddListener(BeginGame);
        gameManager.exitBtn.onClick.AddListener(ExitGame);
    }

    public void Enter()
    {
        gameManager.gameplayCanvas.SetActive(false);
        gameManager.scoreCanvas.SetActive(false);
        gameManager.mainMenuCanvas.SetActive(true);
    }

    public void Update()
    {

    }

    private void BeginGame(){
        gameManager.stateMachine.TransitionTo(gameManager.stateMachine.gameplayState);
    }
    private void ExitGame(){
        gameManager.stateMachine.TransitionTo(gameManager.stateMachine.exitGameState);
    }

    public void Exit()
    {
        gameManager.mainMenuCanvas.SetActive(false);
    }
}
