using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class NetworkManagerUI : MonoBehaviour
{

    [SerializeField] private Button serverButton;
    [SerializeField] private Button hostButton;
    [SerializeField] private Button clientButton;
    [SerializeField] private Button leaveSessionButton;

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
        ToggleUIButtons(true);
    }

    private void OnHost()
    {
        NetworkManager.Singleton.StartHost();
        ToggleUIButtons(true);
    }

    private void OnClient()
    {
        NetworkManager.Singleton.StartClient();
        ToggleUIButtons(true);
    }

    private void OnLeave()
    {
        Debug.Log("on leave clicked");
        NetworkManager.Singleton.Shutdown();
        ToggleUIButtons(false);
    }

    public void ToggleUIButtons(bool started)
    { 
        serverButton.gameObject.SetActive(!started);
        hostButton.gameObject.SetActive(!started);
        clientButton.gameObject.SetActive(!started);
        leaveSessionButton.gameObject.SetActive(started);
    }
}
