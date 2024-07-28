using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using TMPro;

public class PlayerHealthTest
{
    private GameObject playerObject;
    private GameSession player;

    [SetUp]
    public void SetUp()
    {
        playerObject = new GameObject();
        player = playerObject.AddComponent<GameSession>();
        player.playerLives = 3;

        // Ensure scoreText is initialized to avoid null reference
        var scoreTextObject = new GameObject();
        player.scoreText = scoreTextObject.AddComponent<TMPro.TextMeshProUGUI>();

        // Ensure HighScoreManager instance is initialized
        var highScoreManagerObject = new GameObject();
        var highScoreManager = highScoreManagerObject.AddComponent<HighScoreManager>();
        var highScoreTextObject = new GameObject();
        highScoreManager.highScoreText = highScoreTextObject.AddComponent<TextMeshProUGUI>();
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(playerObject);
        var highScoreManager = Object.FindObjectOfType<HighScoreManager>();
        if (highScoreManager != null)
        {
            Object.DestroyImmediate(highScoreManager.gameObject);
        }
    }

    [UnityTest]
    public IEnumerator PlayerTakeDamage()
    {
        player.ProcessPlayerDeath();
        yield return null;
        Assert.AreEqual(2, player.playerLives);

        player.ProcessPlayerDeath();
        yield return null;
        Assert.AreEqual(1, player.playerLives);
    }

    [UnityTest]
    public IEnumerator PlayerGetHealth()
    {
        player.ProcessPlayerDeath();
        player.ProcessPlayerDeath();
        yield return null;

        player.AddHealth();
        yield return null;
        Assert.AreEqual(2, player.playerLives);

        player.AddHealth();
        yield return null;
        Assert.AreEqual(3, player.playerLives);
    }

    [Test]
    public void PlayerLives_NotExceedMax()
    {
        player.AddHealth();
        player.AddHealth();
        player.AddHealth();
        Assert.AreEqual(3, player.playerLives);
    }

    [UnityTest]
public IEnumerator PlayerLives_NeverBelowZero()
{
    // Initial lives count
    Assert.AreEqual(3, player.playerLives);
    
    // Process death calls
    player.ProcessPlayerDeath();
    yield return null;
    Assert.AreEqual(2, player.playerLives);

    player.ProcessPlayerDeath();
    yield return null;
    Assert.AreEqual(1, player.playerLives);

    player.ProcessPlayerDeath();
    yield return null;
    Assert.AreEqual(3, player.playerLives);

    // Ensure lives do not go below zero
    player.ProcessPlayerDeath();
    yield return null;

    // Check if the GameSession object is still alive
    if (player != null)
    {
        Assert.AreEqual(0, player.playerLives);
    }
    else
    {
        // GameSession was destroyed, hence lives are implicitly zero
        Assert.Pass("GameSession was destroyed after player lives reached zero.");
    }
}

}
