using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DunGen;
using ARPG.Movement;

[RequireComponent(typeof(RuntimeDungeon))]
public class SpawnPlayer : MonoBehaviour
{
	private RuntimeDungeon runtimeDungeon;

	[SerializeField] SaveDungeon dungeon1;
	[SerializeField] SaveDungeon dungeon2;
	[SerializeField] SaveDungeon dungeon3;

	private void Awake()
	{
		runtimeDungeon = GetComponent<RuntimeDungeon>();
		runtimeDungeon.Generator.OnGenerationStatusChanged += OnDungeonGenerationStatusChanged;
		runtimeDungeon.Generator.Generate();
	}

	private void OnDestroy()
	{
		runtimeDungeon.Generator.OnGenerationStatusChanged -= OnDungeonGenerationStatusChanged;
	}
	
	private void OnDungeonGenerationStatusChanged(DungeonGenerator generator, GenerationStatus status)
	{
		Debug.Log("BEING CALLED?!");

		GameObject player = GameObject.FindWithTag("Player");
		Debug.Log(player.GetComponent<PlayerController>().DungeonNum);

		if (status == GenerationStatus.Complete)
		{
			Debug.Log("Generation completed?!");
			Debug.Log(player.GetComponent<PlayerController>().DungeonNum);
			switch (player.GetComponent<PlayerController>().DungeonNum)
            {
				case 1:
					Debug.Log("Okay...");
					player.GetComponent<MovementHandler>().NavMeshAgent.enabled = false;
					player.transform.position = dungeon1.startLocation;
					player.GetComponent<MovementHandler>().NavMeshAgent.enabled = true;
					break;
				case 2:
					player.transform.position = dungeon2.startLocation;
					break;
				case 3:
					player.transform.position = dungeon3.startLocation;
					break;
				default:
					// ???
					break;
            }
		}
	}
}
