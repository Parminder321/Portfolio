using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
	[SerializeField] AudioClip brokeClip;
	[SerializeField] GameObject blockSparkingVFX;
	[SerializeField] Sprite[] hitSprites;
	[SerializeField] int maxHits;

	Level level;

	[SerializeField] int timesHit;

	private void Start()
	{
		CountBreakableBlocks();
	}

	private void CountBreakableBlocks()
	{
		level = FindObjectOfType<Level>();
		if (tag == "Breakable")
		{
			level.CountBlocks();
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (tag == "Breakable")
		{
			HandleHit();
		}
	}

	private void ShowNextHitSprite()
	{
		int spriteIndex = timesHit - 1;
		if (hitSprites[spriteIndex] != null)
		{
			GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
		}
		else
		{
			Debug.LogError("Block sprite is missing from the array. " + gameObject.name); 
		}
	}

	private void HandleHit()
	{
		timesHit++;
		maxHits = hitSprites.Length + 1;
		if (timesHit >= maxHits)
		{
			DestroyBlock();
		}
		else
		{
			ShowNextHitSprite();
		}
	}

	private void DestroyBlock()
	{
		PlayBlockDestroyedSFX();
		Destroy(gameObject, 0f);
		level.BlockDestroyed();
		TriggerSparkingVFX();
	}

	private void PlayBlockDestroyedSFX()
	{
		FindObjectOfType<GameStatus>().AddToScore();
		AudioSource.PlayClipAtPoint(brokeClip, Camera.main.transform.position);
	}

	private void TriggerSparkingVFX()
	{
		GameObject sparking = Instantiate(blockSparkingVFX, transform.position, transform.rotation);
		Destroy(sparking, 2f);
	}
}
