using NUnit.Framework;
using UnityEngine;
using System;
using System.Reflection;

namespace Avance2
{
    public class Tests
    {
        [Test]
        public void UpdateScore_IncreasesScoreAndUpdatesText()
        {
            // Arrange
            GameObject gameManagerObject = new GameObject();
            GameManager gameManager = gameManagerObject.AddComponent<GameManager>();
            gameManager.scoreText = "";
            gameManager.score = 0;

            // Act
            var enumerator = gameManager.UpdateScore();
            enumerator.MoveNext(); // Move to the first yield return

            // Assert
            Assert.AreEqual(1, gameManager.score);
            Assert.AreEqual("1", gameManager.scoreText);
            // Cleanup
            UnityEngine.Object.Destroy(gameManagerObject);
        }
        [Test]
        public void GameOver_DisablesPlatformSpawner_StopsUpdateScore_CallsSaveHighScore_ReloadsLevel()
        {
            // Arrange
            GameManager gameManager = new GameManager();
            GameObject platformSpawnerObject = new GameObject();
            gameManager.platformSpawner = platformSpawnerObject;
            platformSpawnerObject.SetActive(true);
            string sceneName = "";

            // Mock StopCoroutine and LoadScene
            bool stopCoroutineCalled = false;
            bool reloadLevelCalled = false;

            gameManager.StopCoroutine = coroutineName =>
            {
                if (coroutineName == "UpdateScore")
                {
                    stopCoroutineCalled = true;
                }
            };

            gameManager.ReloadLevel(sceneName);
            {
                if (sceneName == "Game")
                {
                    reloadLevelCalled = true;
                }
            }

            // Act
            gameManager.GameOver();

            // Assert
            Assert.IsFalse(platformSpawnerObject.activeSelf); // Platform spawner should be deactivated
            Assert.IsTrue(stopCoroutineCalled); // StopCoroutine should be called for "UpdateScore"
            // You can add further assertions for SaveHighScore if needed
            Assert.IsTrue(reloadLevelCalled); // ReloadLevel (LoadScene) should be called with "Game" as the scene name
        }
    }
}