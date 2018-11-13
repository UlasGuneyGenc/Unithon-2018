using System.CodeDom;
using Game.Core.Item;
using UnityEngine;

namespace Game.Core.Level
{
	public abstract class LevelData
	{		
		public abstract ItemType GetNextFillItemType();
		public abstract void Initialize();

		public ItemType[,] GridData { get; protected set; }

		private static readonly ItemType[] DefaultCubeArray = new[]
		{
			ItemType.GreenCube,
			ItemType.YellowCube,
			ItemType.BlueCube,
			ItemType.RedCube,
			ItemType.Balloon
		
			
		};

		protected static ItemType GetRandomCubeItemType()
		{
			return GetRandomItemTypeFromArray(DefaultCubeArray);
		}

		protected static ItemType GetRandomItemTypeFromArray(ItemType[] itemTypeArray)
		{
			return itemTypeArray[Random.Range(0, itemTypeArray.Length-1)];
			
		}
	}
}
