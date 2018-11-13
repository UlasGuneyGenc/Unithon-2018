using Game.Core.Board;
using Game.Core.Item;
using Game.Core.Level;

namespace Game.Levels
{
    public class LevelData_1 : LevelData 
    {        
        public override ItemType GetNextFillItemType()
        {
            return GetRandomCubeItemType();
        }

        public override void Initialize()
        {
            GridData = new ItemType[Board.Rows, Board.Cols];

            GridData[1, 1] = ItemType.Crate;
            GridData[1, 7] = ItemType.Crate;
            
            GridData[7, 1] = ItemType.Crate;
            GridData[7, 7] = ItemType.Crate;
            
            GridData[3, 3] = ItemType.Crate;
            GridData[3, 4] = ItemType.Crate;
            GridData[3, 5] = ItemType.Crate;
            
            GridData[4, 3] = ItemType.Crate;
            GridData[4, 5] = ItemType.Crate;
            
            GridData[5, 3] = ItemType.Crate;
            GridData[5, 4] = ItemType.Crate;
            GridData[5, 5] = ItemType.Crate;

            for (var y = 0; y < Board.Rows; y++)
            {
                for (var x = 0; x < Board.Cols; x++)
                {
                    if(GridData[x, y] != ItemType.None) continue; 
                    GridData[x, y] = GetRandomCubeItemType();
                }
            }            
        }
    }
}
