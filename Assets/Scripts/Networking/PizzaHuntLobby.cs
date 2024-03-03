using System.Collections;
using System.Collections.Generic;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using UnityEngine;

public class PizzaHuntLobby : MonoBehaviour
{
    public static PizzaHuntLobby instance;

    private Lobby joinedLobby;
    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);

        InitializeUnityAuthentication();
    }

    private async void InitializeUnityAuthentication()
    {
        if(UnityServices.State != ServicesInitializationState.Initialized)
        {
            InitializationOptions options = new InitializationOptions();
            options.SetProfile(Random.Range(0, 100000).ToString());

            await UnityServices.InitializeAsync(options);

            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        } 
    }

    public async void CreateLobby(string lobbyName, bool isPrivate)
    {
        try{
            joinedLobby = await LobbyService.Instance.CreateLobbyAsync(lobbyName, 4, new CreateLobbyOptions{
            IsPrivate = isPrivate,

            });
        } catch (RequestFailedException e)
        {
            Debug.LogError("Failed to create lobby: " + e.Message);
        }
        
    }
}
