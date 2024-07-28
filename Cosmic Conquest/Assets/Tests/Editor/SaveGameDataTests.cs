using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.TestTools;

public class SaveGameDataTests
{
    private GameObject gameManagerObject;
    private GameManager gameManager;
    private GameObject gameSessionObject;
    private GameSession gameSession;
    private GameObject playerPosObject;
    private PlayerPos playerPos;
    private GameObject enemyManagerObject;

    [SetUp]
    public void SetUp()
    {
        Debug.Log("Setting up the test");

        gameManagerObject = new GameObject();
        gameManager = gameManagerObject.AddComponent<GameManager>();

        gameSessionObject = new GameObject();
        gameSession = gameSessionObject.AddComponent<GameSession>();

        gameSession.playerLives = 3;
        gameSession.score = 100;

        var scoreTextObject = new GameObject();
        gameSession.scoreText = scoreTextObject.AddComponent<TextMeshProUGUI>();

        var highScoreManagerObject = new GameObject();
        var highScoreManager = highScoreManagerObject.AddComponent<HighScoreManager>();
        var highScoreTextObject = new GameObject();
        highScoreManager.highScoreText = highScoreTextObject.AddComponent<TextMeshProUGUI>();

        playerPosObject = new GameObject();
        playerPos = playerPosObject.AddComponent<PlayerPos>();
        playerPos.player = playerPosObject.transform;

        // Mock EnemyManager
        enemyManagerObject = new GameObject();
        enemyManagerObject.AddComponent<EnemyManager>();

        // Reset EnemyManager instance before each test
        EnemyManager.ResetInstance();
    }

    [TearDown]
    public void TearDown()
    {
        Debug.Log("Tearing down the test");

        Object.DestroyImmediate(gameManagerObject);
        Object.DestroyImmediate(gameSessionObject);
        Object.DestroyImmediate(playerPosObject);
        //Object.DestroyImmediate(enemyManagerObject);
        PlayerPrefs.DeleteAll();
    }

    [Test]
    public void SaveGame_SavesCorrectData()
    {
        Debug.Log("SaveGame method called.");
        gameManager.SaveGame();
        Assert.AreEqual(1, PlayerPrefs.GetInt("playerStarted"));
        Assert.AreEqual(SceneManager.GetActiveScene().buildIndex, PlayerPrefs.GetInt("SavedScene"));
        Assert.AreEqual(3, PlayerPrefs.GetInt("PlayerLives"));
        Assert.AreEqual(100, PlayerPrefs.GetInt("ScoreValue"));
    }

    [UnityTest]
    public IEnumerator LoadGame_LoadsCorrectData()
    {
        gameManager.SaveGame();

        gameSession.playerLives = 3;
        gameSession.score = 0;

        PlayerPrefs.SetInt("SavedScene", 1); // Assuming Scene 1 for test

        gameManager.ContinueGame();
        yield return new WaitForSeconds(0.1f); // Wait for ContinueGame to complete
        yield return gameManager.StartCoroutine("LoadGameSession");

        Assert.AreEqual(3, gameSession.playerLives);
        Assert.AreEqual(0, gameSession.score);
    }
}
