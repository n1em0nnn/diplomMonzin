using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveCam : MonoBehaviour
{
	public float damping = 1.5f;
	public Vector2 offset = new Vector2(2f, 1f);
	public bool faceLeft;
	private Transform player;
	private int lastX;

	[SerializeField] float upLim;
	[SerializeField] float downLim;
	[SerializeField] float LeftLim;
	[SerializeField] float RightLim;
	void Start()
	{
		offset = new Vector2(Mathf.Abs(offset.x), offset.y);
		FindPlayer(faceLeft);
	}

	public void FindPlayer(bool playerFaceLeft)
	{
		player = GameObject.FindGameObjectWithTag("Player").transform;
		lastX = Mathf.RoundToInt(player.position.x);
		if (playerFaceLeft)
		{
			transform.position = new Vector3(player.position.x - offset.x, player.position.y + offset.y, transform.position.z);
		}
		else
		{
			transform.position = new Vector3(player.position.x + offset.x, player.position.y + offset.y, transform.position.z);
		}
	}

	void FixedUpdate()
	{
		if (player)
		{
			int currentX = Mathf.RoundToInt(player.position.x);
			if (currentX > lastX) faceLeft = false; else if (currentX < lastX) faceLeft = true;
			lastX = Mathf.RoundToInt(player.position.x);

			Vector3 target;
			if (faceLeft)
			{
				target = new Vector3(player.position.x - offset.x, player.position.y + offset.y, transform.position.z);
			}
			else
			{
				target = new Vector3(player.position.x + offset.x, player.position.y + offset.y, transform.position.z);
			}
			Vector3 currentPosition = Vector3.Lerp(transform.position, target, damping * Time.deltaTime);
			transform.position = currentPosition;
		}
		transform.position = new Vector3(
			Mathf.Clamp(transform.position.x, LeftLim,RightLim),
			Mathf.Clamp(transform.position.y, downLim, upLim), 
			transform.position.z);
	}
	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawLine(new Vector2(RightLim,downLim),new Vector2(RightLim,upLim));
	}

}
