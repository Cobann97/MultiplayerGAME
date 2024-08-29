using Fusion;
using UnityEngine;

public class playerSpawner : SimulationBehaviour, IPlayerJoined
{
    public GameObject PlayerPrefab;
    public GameObject HurdleEmpty;
    public GameObject HurdlePrefab1;
    public GameObject HurdlePrefab2;
    public GameObject HurdlePrefab3;

    private Vector3[] spawnPositions = new Vector3[]
    {
        new Vector3(10f, 0.6f, -395.5f),
        new Vector3(-10f, 0.6f, -395.5f),
        // Add more positions for more than 2 players
    };

    private Vector3[] ObjectspawnPositionsLeft = new Vector3[]
    {
        new Vector3(0f, 0f, 230f),
        new Vector3(0f, 0f, 190f),
        new Vector3(0f, 0f, 150f),
        new Vector3(0f, 0f, 110f),
        new Vector3(0f, 0f, 70f),
        new Vector3(0f, 0f, 30f),
        new Vector3(0f, 0f, -10f),
        new Vector3(0f, 0f, -50f),
        new Vector3(0f, 0f, -90f),
        new Vector3(0f, 0f, -130f),
        new Vector3(0f, 0f, -170f),
        new Vector3(0f, 0f, -210f),
        new Vector3(0f, 0f, -250f),
        new Vector3(0f, 0f, -290f),
        new Vector3(0f, 0f, -330f),
        new Vector3(0f, 0f, -370f),
        // Add more positions as needed
    };

    private Vector3[] ObjectspawnPositionsRight = new Vector3[]
    {
        new Vector3(19.5f, 0f, 80f),
        new Vector3(19.5f, 0f, 50f),
        new Vector3(19.5f, 0f, 20f),
        new Vector3(19.5f, 0f, -10f),
        new Vector3(19.5f, 0f, -40f),
        new Vector3(19.5f, 0f, -70f),
        new Vector3(19.5f, 0f, -100f),
        new Vector3(19.5f, 0f, -130f),
        new Vector3(19.5f, 0f, -160f),
        new Vector3(19.5f, 0f, -190f),
        new Vector3(19.5f, 0f, -220f),
        new Vector3(19.5f, 0f, -250f),
        new Vector3(19.5f, 0f, -280f),
        new Vector3(19.5f, 0f, -310f),
        new Vector3(19.5f, 0f, -340f),
        new Vector3(19.5f, 0f, -370f)
        // Add more positions as needed
    };

    public void PlayerJoined(PlayerRef player)
    {
        if (player == Runner.LocalPlayer)
        {
            int positionIndex = player.RawEncoded % spawnPositions.Length;
            Vector3 spawnPosition = spawnPositions[positionIndex];

            // Spawn the player
            Runner.Spawn(PlayerPrefab, spawnPosition, Quaternion.identity, player);

            // Spawn the objects based on player's spawn position
            SpawnHurdles(spawnPosition);
        }
    }

    private void SpawnHurdles(Vector3 spawnPosition)
    {
        if (spawnPosition == spawnPositions[0])
        {
            // Spawn Hurdles on the Right
            foreach (Vector3 pos in ObjectspawnPositionsRight)
            {
                SpawnRandomHurdle(pos);
            }
        }
        else
        {
            // Spawn Hurdles on the Left
            foreach (Vector3 pos in ObjectspawnPositionsLeft)
            {
                SpawnRandomHurdle(pos);
            }
        }
    }

    private void SpawnRandomHurdle(Vector3 position)
    {
        GameObject hurdlePrefab = GetRandomHurdle();
        Runner.Spawn(hurdlePrefab, position, Quaternion.identity);
    }

    private GameObject GetRandomHurdle()
    {
        // Randomly select one of the hurdle prefabs
        int randomIndex = Random.Range(0, 4);
        switch (randomIndex)
        {
            case 0: return HurdlePrefab1;
            case 1: return HurdlePrefab2;
            case 2: return HurdlePrefab3;
            case 3: return HurdleEmpty;
            default: return HurdlePrefab2;
        }
    }
}