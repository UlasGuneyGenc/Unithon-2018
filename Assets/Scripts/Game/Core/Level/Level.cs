using Game.Core.Item;
using Game.Mechanics;
using Settings;
using UnityEngine;

namespace Game.Core.Level
{
	public class Level : MonoBehaviour 
	{
		public int LevelNo;
		public Board.Board Board;
		public FallManager FallManager;
		public ParticleLibrary ParticleLibrary;
		public ImageLibrary ImageLibrary;
		
		private LevelData _levelData;
		
		void Start ()
		{
			ItemFactory.Prepare(ImageLibrary, ParticleLibrary);
			PrepareBoard();
			PrepareLevel(LevelNo);
			StartFalls();
		}

		private void PrepareLevel(int levelNo)
		{
			_levelData = LevelDataFactory.CreateLevelData(levelNo);

			for (var y = 0; y < _levelData.GridData.GetLength(0); y++)
			{
				for (var x = 0; x < _levelData.GridData.GetLength(1); x++)
				{
					var cell = Board.Cells[x, y];
					
					var itemType = _levelData.GridData[x, y];
					var item = ItemFactory.CreateItem(itemType, Board.ItemsParent);
					if (item == null) continue;					
					 					
					cell.Item = item;
					item.transform.position = cell.transform.position;
				}
			}
		}

		private void PrepareBoard()
		{
			Board.Prepare();
		}

		private void StartFalls()
		{
			FallManager.Init(Board, _levelData);
			FallManager.StartFalls();
		}
		
	}
}
