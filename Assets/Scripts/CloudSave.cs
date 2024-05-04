using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//
using System;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.CloudSave;
using System.Threading.Tasks;
using UnityEngine.Events;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public class CloudSave : Singleton<CloudSave>
{
    [SerializeField] GameObject blueBox;
    [SerializeField] GameObject redBox;
    [SerializeField] GameObject greenBox;

    struct dataToSave
    {
        public int scoreBlue;
        public int scoreRed;
        public int scoreGreen;
        public float scoreTotal;
    }

    async void Start()
    {
        // Initialize unity services
        await UnityServices.InitializeAsync();

        // Setup events listeners
        SetupEvents();

        // Unity Login
        await SignInAnonymouslyAsync();
    }

    void SetupEvents()
    {
        AuthenticationService.Instance.SignedIn += () => {
            // Shows how to get a playerID
            Debug.Log($"PlayerID: {AuthenticationService.Instance.PlayerId}");

            // Shows how to get an access token
            Debug.Log($"Access Token: {AuthenticationService.Instance.AccessToken}");
        };

        AuthenticationService.Instance.SignInFailed += (err) => {
            Debug.LogError(err);
        };

        AuthenticationService.Instance.SignedOut += () => {
            Debug.Log("Player signed out.");
        };
    }

    async Task SignInAnonymouslyAsync()
    {
        try
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
            Debug.Log("Sign in anonymously succeeded!");
        }
        catch (Exception ex)
        {
            // Notify the player with the proper error message
            Debug.LogException(ex);
        }
    }

    public async void SaveDataCloud()
    {
        Debug.Log("Voy a guardar");

        dataToSave performance = new();
        performance.scoreBlue = blueBox.GetComponentInChildren<BoxCounter>().GetScore();
        performance.scoreRed = redBox.GetComponentInChildren<BoxCounter>().GetScore();
        performance.scoreGreen = greenBox.GetComponentInChildren<BoxCounter>().GetScore();

        performance.scoreTotal = performance.scoreBlue * 1.2f + performance.scoreGreen * 1.5f + performance.scoreRed;

        var data = new Dictionary<string, object> { { "Perfomance", performance } };
        await CloudSaveService.Instance.Data.Player.SaveAsync(data);
        Debug.Log("Exito!");

        
    }
}