using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class NetworkManagerUI : MonoBehaviour
{
    [SerializeField] private GameObject backgroundImage;

    [SerializeField] private Button serverButton;
    [SerializeField] private Button hostButton;
    [SerializeField] private Button clientButton;
    [SerializeField] private Button leaveSessionButton;
    //[SerializeField] private Button RespawnButton;

    [SerializeField] private TextMeshProUGUI pausedText;
    [SerializeField] private TextMeshProUGUI youDiedText;

    private void Awake()
    {
        serverButton.onClick.AddListener(OnStart);
        hostButton.onClick.AddListener(OnHost);
        clientButton.onClick.AddListener(OnClient);
        leaveSessionButton.onClick.AddListener(OnLeave);
        leaveSessionButton.gameObject.SetActive(false);
    }

    private void OnStart()
    {
        NetworkManager.Singleton.StartServer();
        ToggleMenuButtons(true);
    }

    private void OnHost()
    {
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

    public void ToggleMenuButtons(bool started)
    { 
        serverButton.gameObject.SetActive(!started);
        hostButton.gameObject.SetActive(!started);
        clientButton.gameObject.SetActive(!started);
        backgroundImage.SetActive(!started);
        //leaveSessionButton.gameObject.SetActive(started);
    }

    public void ToggleLeaveSession(bool toggled)
    {
        leaveSessionButton.gameObject.SetActive(toggled);
        pausedText.gameObject.SetActive(toggled);
    }

    public void ToggleDeathDirections(bool toggled)
    {
        leaveSessionButton.gameObject.SetActive(toggled);
        youDiedText.gameObject.SetActive(toggled);
    }
}
