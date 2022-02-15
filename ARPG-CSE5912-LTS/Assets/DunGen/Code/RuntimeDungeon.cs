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

        protected virtual void Awake()
        {
			if (GenerateOnStart)
			{
				Generate();
				BakeNavMesh();
				Debug.Log("Dungeon Generated!");
			}
		}

		public void Generate()
		{
			if(Root != null)
				Generator.Root = Root;

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


	}
}
