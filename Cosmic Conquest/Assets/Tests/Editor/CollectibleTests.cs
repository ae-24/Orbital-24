using NUnit.Framework;
using UnityEngine;

public class CollectibleTests
{
    private GameObject collectibleObject;
    private Collectible collectible;
    private GameObject managerObject;

    [SetUp]
    public void SetUp()
    {
        collectibleObject = new GameObject();
        collectible = collectibleObject.AddComponent<Collectible>();
        collectible.collectibleID = "testCollectible";

        managerObject = new GameObject();
        managerObject.AddComponent<CollectibleManager>();
    }

    [TearDown]
    public void TearDown()
    {
        Object.Destroy(collectibleObject);
        Object.Destroy(managerObject);
        PlayerPrefs.DeleteKey("testCollectible");
        Collectible.ResetNewGameFlag(); // Reset the newGame flag
    }

    [Test]
    public void Collectible_Collect_SetsPlayerPref()
    {
        // Act
        collectible.Collect();

        // Assert
        Assert.AreEqual(1, PlayerPrefs.GetInt("testCollectible"));
    }

    [Test]
    public void Collectible_Start_ChecksPlayerPref()
    {
        // Arrange
        PlayerPrefs.SetInt("testCollectible", 1);

        // Act
        collectible.Start();

        // Assert
        Assert.IsFalse(collectible.gameObject.activeSelf);
    }

    [Test]
    public void Collectible_ResetAllCollectibles_DeletesPlayerPref()
    {
        // Arrange
        PlayerPrefs.SetInt("testCollectible", 1);

        // Act
        Collectible.ResetAllCollectibles();

        // Assert
        Assert.IsFalse(PlayerPrefs.HasKey("testCollectible"));
    }

    [Test]
    public void Collectible_OnDestroy_CleansUpPlayerPrefs()
    {
        // Arrange
        collectible.Collect();

        // Act
        Object.DestroyImmediate(collectibleObject); // Immediate destruction to trigger OnDestroy

        // Assert
        Assert.IsFalse(PlayerPrefs.HasKey("testCollectible"));
    }
}

