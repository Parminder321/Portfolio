using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
	private int sceneToRetry;

    public void LoadNextScene()
	{
		int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
		SceneManager.LoadScene(currentSceneIndex + 1);
	}

	public void LoadFirstScene()
	{
		CheckGameStatus();
		SceneManager.LoadScene(0);
	}

	public void LoadFirstLevel()
	{
		CheckGameStatus();
		SceneManager.LoadScene(1);
	}

	public void LoadRetryLevel()
	{
		CheckGameStatus();
		sceneToRetry = PlayerPrefs.GetInt("SavedScene");

		if (sceneToRetry != 0)
		{
			SceneManager.LoadScene(sceneToRetry);
		}
		else
		{
			return;
		}
	}

	public void QuitGame()
	{
		Application.Quit();
	}

	private static void CheckGameStatus()
	{
		if (FindObjectOfType<GameStatus>() != null)
		{
			FindObjectOfType<GameStatus>().DestroyGameStatus();
		}
	}
}
