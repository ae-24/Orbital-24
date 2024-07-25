using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PlayerHealthTest
{
    private GameObject playerObject;
    private GameSession player;

    [SetUp]
    public void SetUp()
    {
        playerObject = new GameObject();
        player = playerObject.AddComponent<GameSession>();
        player.playerLives = 3; // Initialize player lives
    }

    [TearDown]
    public void TearDown()
    {
        Object.Destroy(playerObject);
    }

    [UnityTest]
    public IEnumerator PlayerTakeDamage()
    {
        // Act
        player.ProcessPlayerDeath();
        yield return null; // Skip a frame

        // Assert
        Assert.AreEqual(2, player.playerLives);

        // Act
        player.ProcessPlayerDeath();
        yield return null; // Skip a frame

        // Assert
        Assert.AreEqual(1, player.playerLives);
    }

   [UnityTest]
    public IEnumerator PlayerGetHealth()
    {
        // Act
        player.ProcessPlayerDeath();
        player.ProcessPlayerDeath();
        yield return null; // Skip a frame

        // Act
        player.AddHealth();
        yield return null; // Skip a frame

        // Assert
        Assert.AreEqual(2, player.playerLives);

        // Act
        player.AddHealth();
        yield return null; // Skip a frame

        // Assert
        Assert.AreEqual(3, player.playerLives);
    }

    [Test]
    public void PlayerLives_NotExceedMax()
    {
        // Act
        player.AddHealth();
        player.AddHealth();
        player.AddHealth();

        // Assert
        Assert.AreEqual(3, player.playerLives); // Assuming 3 is the max health
    }

    [UnityTest]
    public IEnumerator PlayerLives_NeverBelowZero()
    {
        // Act
        player.ProcessPlayerDeath();
        player.ProcessPlayerDeath();
        player.ProcessPlayerDeath();
        yield return null; // Skip a frame

        // Assert
        Assert.AreEqual(0, player.playerLives); // Assuming player lives can't go below zero

        // Act
        player.ProcessPlayerDeath();
        yield return null; // Skip a frame

        // Assert
        Assert.AreEqual(0, player.playerLives);
    }
}