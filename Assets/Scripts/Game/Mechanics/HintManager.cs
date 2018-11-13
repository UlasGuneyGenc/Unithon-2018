using Game.Core.Board;
using UnityEngine;
using System.Collections.Generic;
using Game.Core.Item;
using Game.Items;

namespace Game.Mechanics
{	
	public class HintManager : MonoBehaviour 
	{
		public Board Board;
		private bool[,] _visitedCells;
		private bool lockChange = false;


		// Use this for initialization
		void Start () 
		{
		
		}
	
		// Update is called once per frame
		void Update () 
		{
			for (int y = 0; y < Board.Rows; y++) {
				for (int x = 0; x < Board.Cols; x++) {
					var cell = Board.Cells [x, y];
					if (cell.HasItem ()) {
						List<Cell> adjacent = FindMatch (Board.Cells [x, y], Board.Cells [x, y].Item.GetItemType ());
						if (adjacent != null && adjacent.Count > 6 ) {
							for (int i = 0; i < adjacent.Count; i++) {
								if (adjacent [i].HasItem () && adjacent[i].Item.GetItemType() != ItemType.Bomb && adjacent[i].Item.GetItemType() != ItemType.Balloon
									&& adjacent[i].Item.GetItemType() != ItemType.Crate) {
									var item = (CubeItem)adjacent [i].Item;
									item.openHint ();
									item.ChangeHint ();
								}
							}
						} else {
							for (int i = 0; i < adjacent.Count; i++) {
								if (adjacent [i].HasItem () && adjacent[i].Item.GetItemType() != ItemType.Bomb && adjacent[i].Item.GetItemType() != ItemType.Balloon
									&& adjacent[i].Item.GetItemType() != ItemType.Crate) {
									var item = (CubeItem)adjacent [i].Item;
									item.closeHint ();
									item.ChangeHint ();
								}

							}
						}
					}
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

			FindMatches(cell, itemType, resultCells);

			return resultCells;

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
	}
}
