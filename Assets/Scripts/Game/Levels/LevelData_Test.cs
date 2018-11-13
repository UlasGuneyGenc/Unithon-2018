using Game.Core.Board;
using Game.Core.Item;
using Game.Core.Level;

namespace Game.Levels
{
    public class LevelData_Test : LevelData 
    {    
        private readonly ItemType[] _itemArray = 
        {
            ItemType.GreenCube,
            ItemType.YellowCube,
            ItemType.BlueCube,
            ItemType.RedCube,
            ItemType.Balloon
        };
        
        public override ItemType GetNextFillItemType()
        {
            return GetRandomItemType();
        }

        public override void Initialize()
        {
            GridData = new ItemType[Board.Rows, Board.Cols];

            GridData[1, 1] = ItemType.Crate;
            GridData[1, 7] = ItemType.Crate;
            
            GridData[7, 1] = ItemType.Crate;
            GridData[7, 7] = ItemType.Crate;

            GridData[4, 3] = ItemType.Bomb;
            GridData[4, 4] = ItemType.Bomb;
            GridData[4, 5] = ItemType.Bomb;

            for (var y = 0; y < Board.Rows; y++)
            {
                for (var x = 0; x < Board.Cols; x++)
                {
                    if(GridData[x, y] != ItemType.None) continue; 
                    GridData[x, y] = GetRandomItemType();
                }
            }            
        }
        
        private ItemType GetRandomItemType()
        {
            return GetRandomItemTypeFromArray(_itemArray);
        }

    }
}
