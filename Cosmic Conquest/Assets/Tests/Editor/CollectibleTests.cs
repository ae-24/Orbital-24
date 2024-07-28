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
        CollectibleManager.Instance.RegisterCollectible(collectible.collectibleID); // Ensure instance is initialized
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(collectibleObject);
        Object.DestroyImmediate(managerObject);
        PlayerPrefs.DeleteKey("testCollectible");
        Collectible.ResetNewGameFlag();
    }

    [Test]
    public void Collectible_Collect_SetsPlayerPref()
    {
        collectible.Collect();
        Assert.AreEqual(1, PlayerPrefs.GetInt("testCollectible"));
    }

    [Test]
    public void Collectible_Start_ChecksPlayerPref()
    {
        PlayerPrefs.SetInt("testCollectible", 1);
        collectible.Start();
        Assert.IsFalse(collectible.gameObject.activeSelf);
    }

    [Test]
    public void Collectible_ResetAllCollectibles_DeletesPlayerPref()
    {
        PlayerPrefs.SetInt("testCollectible", 1);
        Collectible.ResetAllCollectibles();
        Assert.IsFalse(PlayerPrefs.HasKey("testCollectible"));
    }

    [Test]
    public void Collectible_OnDestroy_CleansUpPlayerPrefs()
    {
        collectible.Collect();
        Object.DestroyImmediate(collectibleObject);
        Assert.IsFalse(PlayerPrefs.HasKey("testCollectible"));
    }
}
