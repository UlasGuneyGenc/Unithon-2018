using System.Collections.Generic;
using Game.Core.Board;
using Game.Core.Item;

namespace Game.Mechanics
{
	public class MatchFinder
	{
		private bool[,] _visitedCells;

		public MatchFinder(Cell[,] cells)
		{
			var cols = cells.GetLength(0);
			var rows = cells.GetLength(1);
			
			var cellsInternal = new Cell[cols,rows];
			
			for (var y = 0; y < rows; y++)
			{
				for (var x = 0; x < cols; x++)
				{
					cellsInternal[x, y] = cells[x, y];
				}
			}
		}
	
		public List<Cell> FindMatch(Cell cell, ItemType itemType)
		{
			_visitedCells = new bool[Board.Cols, Board.Rows];

			for (var y = 0; y < Board.Rows; y++)
			{
				for (var x = 0; x < Board.Cols; x++)
				{
					_visitedCells[x, y] = false;
				}
			}
			
			var resultCells = new List<Cell>();

			if (cell.Item.GetItemType() == ItemType.Bomb)
			{
				FindBombs(cell, itemType, resultCells);
				return resultCells.Count >= 1 ? resultCells : null;
			}
			else
			{
				FindMatches(cell, itemType, resultCells);
				return resultCells.Count > 1 ? resultCells : null;
			}

			
		}
		

		private void FindMatches(Cell cell, ItemType itemType, List<Cell> resultCells)
		{
			if (cell == null) return;
			
			var x = cell.X;
			var y = cell.Y;
			if (_visitedCells[x, y]) return;
			
			_visitedCells[x, y] = true;

			if (cell.HasItem() && cell.Item.IsMatchable() && cell.Item.GetItemType() == itemType)
			{
				resultCells.Add(cell);
			
				var neighbours = cell.Neighbours;
				if (neighbours.Count == 0) return;
	
				for (var i = 0; i < neighbours.Count; i++)
				{	
					FindMatches(neighbours[i], itemType, resultCells);
				}
			}
		
		}
		
		
		
		private void FindBombs(Cell cell, ItemType itemType, List<Cell> resultCells)
		{
			if (cell == null) return;
			
			var x = cell.X;
			var y = cell.Y;
			if (_visitedCells[x, y]) return;
			
			_visitedCells[x, y] = true;

			if (cell.HasItem() && cell.Item.IsMatchable() && cell.Item.GetItemType() == itemType)
			{
				resultCells.Add(cell);
			
				var regions = cell.Regions;
				if (regions.Count == 0) return;
	
				for (var i = 0; i < regions.Count; i++)
				{	
					FindBombs(regions[i], itemType, resultCells);
				}
			}
		
		}
		
		
		
	}
}
