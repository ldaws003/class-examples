using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class ShootEmUpController : MonoBehaviour
{
	// handling wave spawning
	// spawning items
	
	public GameObject[] Enemies;
	public GameObject[] Bosses;
	private float CountDownBoss;
	public float BossSpawnRate;
	public uint MaxEnemies;
	private uint EnemyCount;
	private float currentSpawnRate;
	public float SpawnCountDown;
	public float SpawnMinTime;
	public float SpawnTimeDecrement;
	public GameObject[] SpawnAreas;
	private bool isGameOver;
	private bool isPaused;
	public GameObject GameOverText;
	public GameObject GameOverOverlay;
	public GameObject PauseText;
	
	public void decreaseEnemyCount()
	{
		EnemyCount--;
	}
	
	void Awake()
	{
		CountDownBoss = BossSpawnRate;
		currentSpawnRate = SpawnCountDown;
		EnemyCount = 0;
		Time.timeScale = 1;
		isGameOver = false;
		isPaused = false;
		GameOverText.SetActive(false);
		GameOverOverlay.SetActive(false);
		PauseText.SetActive(false);
	}
	
	int RandomEntry(int size)
	{
		return (int)Random.Range(0, size);
	}

    // Update is called once per frame
    void Update()
    {
        if(isGameOver && Input.GetKey(KeyCode.Space) && !isPaused)
		{
			Time.timeScale = 1;
			SceneManager.LoadScene("shootemup");
		}
		
		if(Input.GetKeyDown(KeyCode.P) && !isGameOver)
		{
			PauseGame();
		}
    }
	
	public void PauseGame()
	{
		isPaused = !isPaused;
		if(isPaused)
		{
			Time.timeScale = 0;
			GameOverOverlay.SetActive(true);
			PauseText.SetActive(true);
		}
		else
		{
			GameOverOverlay.SetActive(false);
			PauseText.SetActive(false);
			Time.timeScale = 1;
		}
	}
	
	void LateUpdate()
	{
		CountDownBoss -= Time.deltaTime;
		currentSpawnRate -= Time.deltaTime;
		
		if(CountDownBoss <= 0 && EnemyCount < MaxEnemies)
		{
			SpawnBoss();
			EnemyCount++;
			CountDownBoss = BossSpawnRate;
		}
		
		if(currentSpawnRate <= 0 && EnemyCount < MaxEnemies)
		{
			SpawnMob();
			EnemyCount++;
			SpawnCountDown -= SpawnTimeDecrement;
			currentSpawnRate = SpawnCountDown;			
		}
	}
	
	
	void SpawnBoss()
	{
		int entry = RandomEntry(Bosses.Length);
		int area = RandomEntry(SpawnAreas.Length);
		while(SpawnAreas[area].GetComponent<SpawnPointController>().IsVisible())
			area = RandomEntry(SpawnAreas.Length);
		Instantiate(Bosses[entry], SpawnAreas[area].transform.position, Quaternion.identity);
	}
	
	void SpawnMob()
	{
		int entry = RandomEntry(Enemies.Length);
		int area = RandomEntry(SpawnAreas.Length);
		// check if the spawnarea is not visible
		while(SpawnAreas[area].GetComponent<SpawnPointController>().IsVisible())
			area = RandomEntry(SpawnAreas.Length);
		Instantiate(Enemies[entry], SpawnAreas[area].transform.position, Quaternion.identity);
	}
	
	public void GameOver()
	{
		Time.timeScale = 0;
		isGameOver = true;
		GameOverText.SetActive(true);
		GameOverOverlay.SetActive(true);
	}
}
