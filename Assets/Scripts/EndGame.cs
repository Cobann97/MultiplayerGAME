using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Fusion;
public class EndGame : MonoBehaviour
{
    private NetworkRunner networkRunner;
    [SerializeField] private GameObject EndGameScreen;
    [SerializeField] private GameObject win;
    [SerializeField] private GameObject lose;
    [SerializeField] private GameObject mainMenüButton;
    [SerializeField] private GameObject exitButton;

    private static bool gameEnded = false;

    private void Awake()
    {
        GameObject runner = GameObject.FindGameObjectWithTag("Runner");
        if (runner != null)
        {
            networkRunner = runner.GetComponent<NetworkRunner>();
        }
        if (networkRunner == null)
        {
            Debug.Log("Cannot reach networkRunner");
        }

        mainMenüButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(MainMenü);
        exitButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(Exit);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player") && !gameEnded)
        {
            PlayerMovement player = collider.GetComponent<PlayerMovement>();

            Debug.Log("End Point Reached");

            if (player == null)
            {
                Debug.Log("Cannot reach player");
            }

            gameEnded = true;
            EndGameScreen.SetActive(true);
            

            if (player.HasStateAuthority)
            {
                win.SetActive(true);
                Debug.Log("You are the winner!");
            }
            else
            {
                lose.SetActive(true);
                Debug.Log("You lost the game.");
            }

            StartCoroutine(ShutdownNetwork());
        }
    }

    private IEnumerator ShutdownNetwork()
    {
        // Wait for .5 seconds to ensure both players see the correct end screen
        yield return new WaitForSeconds(0.5f);
        networkRunner.Shutdown();
    }

    private void MainMenü()
    {
        EndGameScreen.SetActive(false);
        gameEnded = false;
        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void Exit() 
    {
        Application.Quit();
    }
}