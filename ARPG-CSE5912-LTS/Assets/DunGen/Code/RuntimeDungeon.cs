using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using DunGen.Adapters;
using UnityEngine.AI;

namespace DunGen
{
    [AddComponentMenu("DunGen/Runtime Dungeon")]

	public class RuntimeDungeon : MonoBehaviour
	{
        public DungeonGenerator Generator = new DungeonGenerator();
        public bool GenerateOnStart = true;
		public GameObject Root;
		public SaveDungeon savedDunData;

        protected virtual void Awake()
        {
			if (GenerateOnStart)
			{
				Generate();
				BakeNavMesh();
				AddEnemies();
				Debug.Log("Dungeon Generated!");
			}
		}

		public void Generate()
		{
			if(Root != null)
				Generator.Root = Root;

			if (savedDunData != null)
				Generator.savedDungeon = savedDunData;

			if (!Generator.IsGenerating)
				Generator.Generate();
		}

		void BakeNavMesh()
        {
			foreach (Transform child in Root.transform)
			{
				if (child.CompareTag("DungeonPart"))
				{
					child.GetComponent<NavMeshSurface>().BuildNavMesh();
				}
			}
		}

		void AddEnemies()
        {
			foreach (Transform tile in Root.transform)
			{
				foreach (Transform child in tile.transform)
                {
					if (child.CompareTag("Enemy"))
					{
						foreach (Transform enemy in child.transform)
                        {
							enemy.gameObject.GetComponent<NavMeshAgent>().enabled = true;
						}
					}
				}
			}
		}


	}
}
