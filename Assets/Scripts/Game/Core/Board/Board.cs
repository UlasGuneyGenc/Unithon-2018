using System;
using System.Collections.Generic;
using System.Linq;
using Game.Core.Item;
using Game.Mechanics;
using UnityEngine;
using Game.Items;

namespace Game.Core.Board
{
	public class Board : MonoBehaviour
	{
		public const int Rows = 9;
		public const int Cols = 9;
	
		public Cell CellPrefab;
		
		public Transform CellsParent;
		public Transform ItemsParent;
		public Transform ParticlesParent;

		[HideInInspector] public Cell[,] Cells = new Cell[Cols, Rows];
	
		public void Prepare()
		{
			CreateCells();
			PrepareCells();
		}
		
		private void CreateCells()
		{
			for (var y = 0; y < Rows; y++)
			{
				for (var x = 0; x < Cols; x++)
				{
					var cell = Instantiate(CellPrefab, Vector3.zero, Quaternion.identity, CellsParent);
					Cells[x, y] = cell;
				}
			}
		}

		private void PrepareCells()
		{
			for (var y = 0; y < Rows; y++)
			{
				for (var x = 0; x < Cols; x++)
				{
					Cells[x, y].Prepare(x, y, this);
				}
			}
		}
		public void CellTapped(Cell cell)
		{
			if (cell == null) return;
			
			if (!cell.HasItem() || !cell.Item.IsMatchable()) return;
		
			Explore(cell);
		}
		
		//if clicked cell is 
		public void Explore(Cell cell)
		{
			
			// Items must be exploded
			if (cell.Item.GetItemType() != ItemType.Bomb)
			{
				var matchFinder = new MatchFinder(Cells);
				var cells = matchFinder.FindMatch(cell, cell.Item.GetItemType());
				if (cells == null) return;
				ExplodeMatchingCells(cells);

				// if total number of adjacent number of cubes are higher than 7
				//convert it to bomb by creating new item.
				if (cells.Count >= 7)
				{
					var item = ItemFactory.CreateItem(ItemType.Bomb, cell.transform);
					cell.Item = item;
					item.transform.position = cell.transform.position;
				}
			}
			//If the current cell is bomb, then explode it.
			else
			{
				ExplodeMatchingBomb(cell);	
			}
			
		}

		
		private void ExplodeMatchingBomb(Cell cell)
		{
			var matchFinder = new MatchFinder(Cells);
			var cells = matchFinder.FindMatch(cell, cell.Item.GetItemType());
			List<Cell> effectiveBlastRegion = new List<Cell>();
			for (var i = 0; i < cells.Count; i++)
			{
				for (int j = 0; j < cells[i].Regions.Count; j++)
				{
					if (cells[i].Regions[j].HasItem()) 
					{
						if(!effectiveBlastRegion.Contains(cells[i].Regions[j]))
							effectiveBlastRegion.Add(cells[i].Regions[j]);
					}
				}
				effectiveBlastRegion.Add(cells[i]);
			}
			for (var i = 0; i < effectiveBlastRegion.Count; i++)
			{
				if (effectiveBlastRegion[i].HasItem())
				{
					var explodedCell = effectiveBlastRegion[i];
					var item = explodedCell.Item;
					item.TryExecute();
				}
			}
		}
		
		private void ExplodeMatchingCells(List<Cell> cells)
		{
			List<Cell> affectedCells = new List<Cell>();		
			for (var i = 0; i < cells.Count; i++)
			{
				for (int j = 0; j < cells[i].Neighbours.Count; j++)
				{
					if (cells[i].Neighbours[j].HasItem() && (cells[i].Neighbours[j].Item.GetItemType() == ItemType.Crate || cells[i].Neighbours[j].Item.GetItemType()==ItemType.Balloon)) 
					{
						if(!affectedCells.Contains(cells[i].Neighbours[j]))
							affectedCells.Add(cells[i].Neighbours[j]);
					}
				}
			}
			cells = cells.Concat(affectedCells).ToList();
			for (var i = 0; i < cells.Count; i++)
			{
				var explodedCell = cells[i];
				var item = explodedCell.Item;
				item.TryExecute();
			}
		}

		public Cell GetNeighbourWithDirection(Cell cell, Direction direction)
		{
			var x = cell.X;
			var y = cell.Y;
			switch (direction)
			{
				case Direction.None:
					break;
				case Direction.Up:
					y += 1;
					break;
				case Direction.UpRight:
					y += 1;
					x += 1;
					break;
				case Direction.Right:
					x += 1;
					break;
				case Direction.DownRight:
					y -= 1;
					x += 1;
					break;
				case Direction.Down:
					y -= 1;
					break;
				case Direction.DownLeft:
					y -= 1;
					x -= 1;
					break;
				case Direction.Left:
					x -= 1;
					break;
				case Direction.UpLeft:
					y += 1;
					x -= 1;
					break;
				default:
					throw new ArgumentOutOfRangeException("direction", direction, null);
			}

			if (x >= Cols || x < 0 || y >= Rows || y < 0) return null;

			return Cells[x, y];
		}
	}
}
