using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
	[SerializeField] Paddle paddle1;
	Vector2 paddleToBallVector;
	bool hasStarted = false;
	[SerializeField] float xPush = 2f;
	[SerializeField] float yPush = 15f;
	[SerializeField] AudioClip[] ballSounds;
	[SerializeField] float randomFactor = 0.2f;

	Rigidbody2D myRigidBody2D;
	AudioSource myAudioSource;

	// Start is called before the first frame update
	void Start()
    {
		myAudioSource = GetComponent<AudioSource>();
		myRigidBody2D = GetComponent<Rigidbody2D>();
		paddleToBallVector = transform.position - paddle1.transform.position;        
    }

    // Update is called once per frame
    void Update()
	{
		if (!hasStarted)
		{
			LockBallToPaddle();
			ShootOnMouseClick();
		}
	}

	private void ShootOnMouseClick()
	{
		if (Input.GetMouseButtonDown(0))
		{
			hasStarted = true;
			myRigidBody2D.velocity = new Vector2(xPush, yPush);
		}
	}

	private void LockBallToPaddle()
	{
		Vector2 paddlePos = new Vector2(paddle1.transform.position.x, paddle1.transform.position.y);
		transform.position = paddlePos + paddleToBallVector;
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		Vector2 velocityTweak = new Vector2(UnityEngine.Random.Range(0f, randomFactor), UnityEngine.Random.Range(0f, randomFactor));
		if (hasStarted)
		{
			AudioClip clip = ballSounds[UnityEngine.Random.Range(0, ballSounds.Length)];
			myAudioSource.PlayOneShot(clip);
			myRigidBody2D.velocity += velocityTweak;
		}
	}
}
