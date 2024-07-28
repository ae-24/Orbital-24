using NUnit.Framework;
using UnityEngine;
using System.Collections;
using UnityEngine.TestTools;

public class EnemyTests
{
    private GameObject enemyObject;
    private EnemyMovement enemyMovement;
    private EnemyState enemyState;
    private GameObject bloodEffectPrefab;

    [SetUp]
    public void SetUp()
    {
        Debug.Log("Setting up the test");

        enemyObject = new GameObject();
        enemyObject.AddComponent<Animator>();
        enemyObject.AddComponent<Rigidbody2D>();
        enemyMovement = enemyObject.AddComponent<EnemyMovement>();
        enemyState = enemyObject.AddComponent<EnemyState>();

        bloodEffectPrefab = new GameObject();
        bloodEffectPrefab.AddComponent<ParticleSystem>();

        enemyMovement.GetType().GetField("health", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(enemyMovement, 10);
        enemyMovement.GetType().GetField("startDazedTime", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(enemyMovement, 1.0f);
        enemyMovement.GetType().GetField("bloodEffect", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(enemyMovement, bloodEffectPrefab);
        enemyState.enemyID = "enemy_test";

        // Log initial number of enemies
        Debug.Log($"Initial number of enemies: {Object.FindObjectsOfType<EnemyState>().Length}");
    }

    [TearDown]
    public void TearDown()
    {
        Debug.Log("Tearing down the test");

        Object.DestroyImmediate(enemyObject);
        Object.DestroyImmediate(bloodEffectPrefab);
        PlayerPrefs.DeleteKey("enemy_test");
        PlayerPrefs.DeleteKey("enemy_test_x");
        PlayerPrefs.DeleteKey("enemy_test_y");
        EnemyManager.ResetInstance();
        EnemyState.ResetAllEnemies();

        // Log the number of enemies after teardown
        EnemyState[] enemiesAfterTearDown = Object.FindObjectsOfType<EnemyState>();
        Debug.Log($"Number of enemies after teardown: {enemiesAfterTearDown.Length}");
    }

    [UnityTest]
    public IEnumerator Enemy_TakesDamage()
    {
        enemyMovement.TakeDamage(3);
        yield return null;

        if (enemyMovement != null)
        {
            int health = (int)enemyMovement.GetType().GetField("health", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(enemyMovement);
            Assert.AreEqual(7, health);
        }

        enemyMovement.TakeDamage(5);
        yield return null;

        if (enemyMovement != null)
        {
            int health = (int)enemyMovement.GetType().GetField("health", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(enemyMovement);
            Assert.AreEqual(2, health);
        }
    }

    [UnityTest]
    public IEnumerator Enemy_Death()
    {
        enemyMovement.TakeDamage(10);
        yield return null;

        if (enemyObject != null && enemyMovement != null)
        {
            int health = (int)enemyMovement.GetType().GetField("health", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(enemyMovement);
            Assert.AreEqual(0, health);
            Assert.IsFalse(enemyObject.activeSelf);
            Assert.AreEqual(0, PlayerPrefs.GetInt("enemy_test"));
        }
    }

    [UnityTest]
    public IEnumerator Enemy_SpawnsCorrectly()
    {
        GameObject managerObject = new GameObject();
        EnemyManager enemyManager = managerObject.AddComponent<EnemyManager>();

        Debug.Log("Starting enemy registration");
        enemyManager.RegisterEnemy(enemyObject, "enemy_test");
        yield return null;

        EnemyState[] enemies = Object.FindObjectsOfType<EnemyState>();
        Debug.Log($"Number of enemies found after registration: {enemies.Length}");
        Assert.AreEqual(1, enemies.Length);

        // Destroy the enemy object and check again
        Object.DestroyImmediate(enemyObject);
        yield return null;

        enemies = Object.FindObjectsOfType<EnemyState>();
        Debug.Log($"Number of enemies found after destroying enemy object: {enemies.Length}");
        Assert.AreEqual(0, enemies.Length);

        Object.DestroyImmediate(managerObject);

        // Check again after destroying the manager object
        enemies = Object.FindObjectsOfType<EnemyState>();
        Debug.Log($"Number of enemies found after destroying manager object: {enemies.Length}");
        Assert.AreEqual(0, enemies.Length);
    }
}
