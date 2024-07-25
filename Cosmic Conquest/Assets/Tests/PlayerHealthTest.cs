using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PlayerHealthTest
{
    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator PlayerTakeDamage()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        var gameObject = new GameObject();
        var player = gameObject.AddComponent<GameSession>();
        player.ProcessPlayerDeath();
        Assert.IsTrue(player.playerLives == 2);
        player.ProcessPlayerDeath();
        Assert.IsTrue(player.playerLives == 1);
        yield return null;
    }

    [UnityTest]
        public IEnumerator PlayerGetHealth()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        var gameObject = new GameObject();
        var player = gameObject.AddComponent<GameSession>();
        player.ProcessPlayerDeath();
        player.ProcessPlayerDeath();
        player.AddHealth();
        Assert.IsTrue(player.playerLives == 2);
        player.AddHealth();
        Assert.IsTrue(player.playerLives == 3);
        yield return null;
    }
}
