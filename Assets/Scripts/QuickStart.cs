
using System.Linq;
using System.Threading.Tasks;
using Fusion;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public NetworkRunner networkRunner;
    public Text feedbackText; // UI Text component to show feedback
    public bool isReady = false;
    public float count = 4;

    public void Awake()
    {
        count = 4;
    }
    public async void OnStartButtonClicked()
    {
        await
        StartPlayer(networkRunner);
    }
    public async Task StartPlayer(NetworkRunner runner)
    {
        if (networkRunner.ActivePlayers.Count() < 2)
        {
            feedbackText.text = "Waiting for other players..."; 
        }
        var result = await runner.StartGame(new StartGameArgs()
        {
            GameMode = GameMode.Shared, 
        });

        if (result.Ok)
        {
            // all good
        }
        else
        {
            Debug.LogError($"Failed to Start: {result.ShutdownReason}");
        }

    }

    private void Update()
    {
        if (isReady == true)
        {
            count = count - (1f * Time.deltaTime);
            //Debug.Log(count);
        }


    }
    public bool IsReadyFunc() 
    {
        if (networkRunner.ActivePlayers.Count() == 2)
        {
            isReady = true;
        }
        return isReady;
        
    }

    public float countReturner() 
    {
        if (isReady && count >= 3f)
        {
            feedbackText.text = "Player2 joined. Game is Starting...";
        }
        else if (isReady && count >= 2f) 
        {
            feedbackText.text = "3";
        }
        else if (isReady && count >= 1f)
        {
            feedbackText.text = "2";
        }
        else if(isReady && count >= 0f) 
        {
            feedbackText.text = "1";
        }
        if (count <= 0) 
        {
            count = 0;
            feedbackText.text = null;
            //Debug.Log("DÝSAPPEARED");
        }
        return count;

    }

}