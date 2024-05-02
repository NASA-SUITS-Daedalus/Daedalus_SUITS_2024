using UnityEngine;

// This stores whichever step the EV is currently on during the Egress Procedure temporarily

public class TaskManager : MonoBehaviour
{
    public static TaskManager Instance { get; private set; }

    private int currentTaskIndex = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public int GetCurrentTaskIndex()
    {
        return currentTaskIndex;
    }

    public void SetCurrentTaskIndex(int index)
    {
        currentTaskIndex = index;
    }
}