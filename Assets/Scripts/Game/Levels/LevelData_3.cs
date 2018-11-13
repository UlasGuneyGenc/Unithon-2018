using Game.Core.Board;
using Game.Core.Item;
using Game.Core.Level;

namespace Game.Levels
{
    public class LevelData_3 : LevelData 
    {        
        public override ItemType GetNextFillItemType()
        {
            return GetRandomCubeItemType();
        }

        public override void Initialize()
        {
            GridData = new ItemType[Board.Rows, Board.Cols];

            GridData[2, 2] = ItemType.Bomb;
            GridData[2, 6] = ItemType.Bomb;
            
            GridData[6, 2] = ItemType.Bomb;
            GridData[6, 6] = ItemType.Bomb;
            
            GridData[3, 4] = ItemType.Bomb;
            GridData[4, 3] = ItemType.Bomb;
            GridData[4, 4] = ItemType.Bomb;
            GridData[4, 5] = ItemType.Bomb;
			GridData[5, 4] = ItemType.Bomb;

            
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
