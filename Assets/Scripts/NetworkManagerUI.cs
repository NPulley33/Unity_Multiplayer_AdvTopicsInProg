using System;
using TMPro;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using UnityEngine.UI;

public class NetworkManagerUI : MonoBehaviour
{
    [SerializeField] private GameObject backgroundImage;

    [SerializeField] private Button hostButton;
    [SerializeField] private Button clientButton;
    [SerializeField] private Button leaveSessionButton;
    [SerializeField] private Button quitButton;

    [SerializeField] private Button quickJoinButton;
    [SerializeField] private GameObject joiningSessionText;
    //[SerializeField] private Button RespawnButton;

    [SerializeField] private GameObject PauseMenu;
    [SerializeField] private GameObject DeathScreen;

    private ushort port = 7777;
    private UnityTransport transport;

    private void Awake()
    {
        hostButton.onClick.AddListener(OnHost);
        clientButton.onClick.AddListener(OnClient);
        leaveSessionButton.onClick.AddListener(OnLeave);
        quitButton.onClick.AddListener(QuitGame);
        quickJoinButton.onClick.AddListener(QuickJoin);

        leaveSessionButton.gameObject.SetActive(false);

        transport = NetworkManager.Singleton.GetComponent<UnityTransport>();
    }

    private void QuickJoin()
    {
        ToggleMenuButtons(true);
    }

    private void OnStart()
    {
        NetworkManager.Singleton.StartServer();
        ToggleMenuButtons(true);
    }

    private void OnHost()
    {
        transport.SetConnectionData("0.0.0.0", port);

        NetworkManager.Singleton.StartHost();
        ToggleMenuButtons(true);
    }

    private void OnClient()
    {
        NetworkManager.Singleton.StartClient();
        ToggleMenuButtons(true);
    }

    private void OnLeave()
    {
        Debug.Log("on leave clicked");
        NetworkManager.Singleton.Shutdown();
        ToggleMenuButtons(false);
        ToggleLeaveSession(false);
        ToggleDeathDirections(false);
    }

    private void QuitGame()
    {
        Application.Quit();
    }

    public void ToggleMenuButtons(bool started)
    {
        //hostButton.gameObject.SetActive(!started);
        //clientButton.gameObject.SetActive(!started);
        quickJoinButton.gameObject.SetActive(!started);
        quitButton.gameObject.SetActive(!started);
        joiningSessionText.gameObject.SetActive(!started);
        backgroundImage.SetActive(!started);
        //leaveSessionButton.gameObject.SetActive(started);
    }

    public void ToggleLeaveSession(bool toggled)
    {
        Debug.Log("toggled leave session");
        leaveSessionButton.gameObject.SetActive(toggled);
        PauseMenu.SetActive(toggled);
    }

    public void ToggleDeathDirections(bool toggled)
    {
        ToggleLeaveSession(false);

        leaveSessionButton.gameObject.SetActive(!toggled);
        DeathScreen.SetActive(toggled);
    }

    public void ToggleJoiningSessionText(bool toggled)
    {
        joiningSessionText.SetActive(toggled);
    }
}
