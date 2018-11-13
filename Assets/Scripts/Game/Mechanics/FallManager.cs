using System.Collections.Generic;
using Game.Core.Board;
using Game.Core.Item;
using Game.Core.Level;
using UnityEngine;

namespace Game.Mechanics
{
	public class FallManager : MonoBehaviour
	{
		private bool _isActive;
		private Board _board;
		private LevelData _levelData;
		private Cell[] _fillingCells;
	
		public void Init(Board board, LevelData levelData)
		{
			_board = board;
			_levelData = levelData;
			
			CreateFillingCells();
		}

		private void CreateFillingCells()
		{
			var cellList = new List<Cell>();
			for (var y = 0; y < Board.Rows; y++)
			{
				for (var x = 0; x < Board.Cols; x++)
				{
					var cell = _board.Cells[x, y];
					if (cell != null && cell.IsFillingCell)
					{
						cellList.Add(cell);
					}
				}
			}

			_fillingCells = cellList.ToArray();
		}

		public void StartFalls()
		{
			_isActive = true;
		}
		public void StopFalls()
		{
			_isActive = false;
		}

		private void DoFalls()
		{
			for (var y = 0; y < Board.Rows; y++)
			{
				for (var x = 0; x < Board.Cols; x++)
				{
					var cell = _board.Cells[x, y];
					if (cell.Item != null && cell.FirstCellBelow != null && cell.FirstCellBelow.Item == null)
					{
						cell.Item.Fall();
					}
				}
			}
		}

		private void DoFills()
		{
			for (var i = 0; i < _fillingCells.Length; i++)
			{
				var cell = _fillingCells[i];
				if (cell.Item == null)
				{
					cell.Item = ItemFactory.CreateItem(
						_levelData.GetNextFillItemType(), _board.ItemsParent);
					
					var offsetY = 0.0F;
					var targetCellBelow = cell.GetFallTarget().FirstCellBelow;
					if (targetCellBelow != null)
					{
						if (targetCellBelow.Item != null)
						{
							offsetY = targetCellBelow.Item.transform.position.y + 1;
						}
					}
					
					var p = cell.transform.position;
					p.y += 1;
					p.y = p.y > offsetY ? p.y : offsetY;
						
					if(!cell.HasItem()) continue;
					cell.Item.transform.position = p;
					cell.Item.Fall();
				}
			}
		}
		
		public void Update ()
		{
			if (!_isActive) return;
		
			DoFalls();
			DoFills();
		}   
	}
}
