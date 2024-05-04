using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGameState : IState
{
    // Start is called before the first frame update
    private GameManager gameManager;
    
    public ExitGameState(GameManager gameManager){
        this.gameManager = gameManager;
        gameManager.cancelBtnPanelQuestion.onClick.AddListener(BeginGamePanel);
        gameManager.exitBtnPanelQuestion.onClick.AddListener(Exit);
    }

    public void Enter(){
        gameManager.exitCanvasQuestion.SetActive(true);
    }
    public void Update(){

    }

    public void Exit(){
        gameManager.exitCanvasQuestion.SetActive(false);
        Application.Quit();
    }

    void BeginGamePanel(){
        gameManager.stateMachine.TransitionTo(gameManager.stateMachine.mainMenuState);
    }

}
