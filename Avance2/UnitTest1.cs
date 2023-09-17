using NUnit.Framework;
using UnityEngine;
using System;
using System.Reflection;

namespace Avance2
{
    public class Tests
    {
        [Test]
        public void GeneratePosition_UpdatesNewPosCorrectly()
        {
            // Arrange
            PlatformSpawner positionGenerator = new PlatformSpawner();
            positionGenerator.lastPosition = new Vector3(1f, 0f, 2f);
            Vector3 initialNewPos = positionGenerator.newPos;

            // Act
            positionGenerator.GeneratePosition();

            // Assert
            // Verifica que newPos se haya actualizado correctamente según las reglas de GeneratePosition
            if (initialNewPos.x == positionGenerator.lastPosition.x)
            {
                Assert.AreEqual(initialNewPos.z + 2f, positionGenerator.newPos.z, 0.01f); // Verifica si z se actualizó correctamente
            }
            else
            {
                Assert.AreEqual(initialNewPos.x + 2f, positionGenerator.newPos.x, 0.01f); // Verifica si x se actualizó correctamente
            }
        }
        [Test]
        public void CheckInput_FirstInput_NoChangeDirection()
        {
            // Arrange
            var characterController = new CarController();

            // Act
            characterController.CheckInput();

            // Assert
            // La primera llamada a CheckInput debe establecer firstInput en false y no llamar a ChangeDirection
            Assert.IsFalse(characterController.firstInput);
        }
        [Test]
        public void ChangeDirection_MovingLeft_ToggleDirection()
        {
            // Arrange
            var characterController = new CarController();
            characterController.movingLeft = true;
            var initialRotation = Quaternion.Euler(0, 0, 0);

            // Act
            characterController.ChangeDirection();

            // Assert
            // Cuando movingLeft es true, ChangeDirection debe cambiarlo a false y rotar el objeto
            Assert.IsFalse(characterController.movingLeft);
            Assert.AreNotEqual(initialRotation, characterController.transform.rotation);
        }
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