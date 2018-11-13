using Game.Core.Board;
using Game.Core.Item;
using Game.Core.Level;
using UnityEngine;

namespace Game.Levels
{
    public class LevelData_2 : LevelData 
    {
        public override ItemType GetNextFillItemType()
        {
            
            var chance = Random.Range(0, 9);
            if (chance == 0)
                return ItemType.Balloon;
            return GetRandomCubeItemType();
        }
        
        public override void Initialize()
        {
            GridData = new ItemType[Board.Rows, Board.Cols];

            for (var y = 0; y < Board.Rows; y++)
            {
                for (var x = 0; x < Board.Cols; x++)
                {
                    if(GridData[x, y] != ItemType.None) continue;
                    if (x == y || x + y == 8)
                    {
                        GridData[x, y] = ItemType.Balloon;
                    }
                    else
                    {
                        GridData[x, y] = GetRandomCubeItemType();
                    }
                }
            }            
        }
    }
}
