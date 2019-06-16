using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameStatus : MonoBehaviour
{
	[Range(0.1f, 10f)] [SerializeField] float normalTime = 1f;
	[SerializeField] int pointsPerBlockDestroyed = 83;
	[SerializeField] TextMeshProUGUI scoreText;
	[SerializeField] bool isAutoPlayEnabled;

	[SerializeField] int currentScore = 0;

	private void Awake()
	{
		CheckGameStatus();
	}

	private void Start()
	{
		scoreText.text = currentScore.ToString();
	}

	// Update is called once per frame
	void Update()
    {
		Time.timeScale = normalTime;
    }

	public void AddToScore()
	{
		currentScore += pointsPerBlockDestroyed;
		scoreText.text = currentScore.ToString();
	}

	public void CheckGameStatus()
	{
		int gameStatusCount = FindObjectsOfType<GameStatus>().Length;
		if (gameStatusCount > 1)
		{
			gameObject.SetActive(false);
			DestroyGameStatus();
		}
		else
		{
			DontDestroyOnLoad(gameObject);
		}
	}

	public void DestroyGameStatus()
	{
		Destroy(gameObject);
	}

	public bool IsAutoPlayEnabled()
	{
		return isAutoPlayEnabled;
	}
}
