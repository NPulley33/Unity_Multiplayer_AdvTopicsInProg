using UnityEngine;

public class UiController : MonoBehaviour
{
    [SerializeField] GameObject CreateButton;
    [SerializeField] GameObject JoinButton;
    [SerializeField] GameObject QuickJoinButton;
    [SerializeField] GameObject LeaveButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CreateButton.SetActive(true);
        JoinButton.SetActive(true);
        QuickJoinButton.SetActive(true);
        LeaveButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnGameEntered()
    {
        CreateButton.SetActive(false);
        JoinButton.SetActive(false);
        QuickJoinButton.SetActive(false);
        LeaveButton.SetActive(true);
    }

    public void OnGameExited()
    {
        CreateButton.SetActive(true);
        JoinButton.SetActive(true);
        QuickJoinButton.SetActive(true);
        LeaveButton.SetActive(false);
    }

}
