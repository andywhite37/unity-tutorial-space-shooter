using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    public GameObject hazard;
    public int hazardCount;
    public Vector3 spawnValues;
    public float spawnWait;
    public float startWait;
    public float waveWait;
    public GUIText scoreText;
    public GUIText restartText;
    public GUIText gameOverText;
    private int score;
    private bool gameOver;
    private bool restart;

    private void Start() {
        score = 0;
        UpdateScore();

        gameOver = false;
        gameOverText.text = "";

        restart = false;
        restartText.text = "";

        StartCoroutine(SpawnWaves());
    }

    private void Update()
    {
        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Application.LoadLevel(Application.loadedLevel);
            }
        }
    }

    IEnumerator SpawnWaves() {
        yield return new WaitForSeconds(startWait);
        while (true) {
            for (int i = 0; i < hazardCount; i++) {
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

            if (gameOver) {
                restartText.text = "Press 'R' for restart";
                restart = true;
                break;
            }
        }
    }

    public void AddScore(int newScoreValue) {
        score += newScoreValue;
        UpdateScore();
    }

    public void GameOver() {
        gameOverText.text = "Game over";
        gameOver = true;
    }

    void UpdateScore() {
        scoreText.text = "Score: " + score;
    }
}
