using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Avance2
{
    class GameManager : MonoBehaviour
    {

        public string scoreText;
        public int score;
        public GameObject platformSpawner;
        public Action<string> StopCoroutine { get; set; }

        public IEnumerator UpdateScore()
        {
            while (true)
            {
                yield return new WaitForSeconds(1f);
                score++;
                scoreText = score.ToString();
            }
        }
        public void GameOver()
        {
            platformSpawner.SetActive(false);
            StopCoroutine("UpdateScore");
            SaveHighScore();
            Invoke("ReloadLevel", 1f);
        }

        public void ReloadLevel(string scene)
        {
            SceneManager.LoadScene(scene);
        }
        void SaveHighScore()
        {
            if (PlayerPrefs.HasKey("HighScore"))
            {
                if (score > PlayerPrefs.GetInt("HighScore"))
                {
                    PlayerPrefs.SetInt("HighScore", score);
                }
            }
            else
            {
                PlayerPrefs.SetInt("HighScore", score);
            }
        }
    }
}
