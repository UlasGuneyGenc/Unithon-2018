using Game.Levels;

namespace Game.Core.Level
{
    public static class LevelDataFactory
    {
        public static LevelData CreateLevelData(int no)
        {
            LevelData levelData;
            switch (no)
            {
                case -1:
                    levelData = new LevelData_Test();
                    break;
                case 0:
                    levelData = new LevelData_0();
                    break;
                case 1:
                    levelData = new LevelData_1();
                    break;
                case 2:
                    levelData = new LevelData_2();
                    break;
                case 3:
                    levelData = new LevelData_3();
                    break;
                default:
                    levelData = new LevelData_0();
                    break;
            }
            
            levelData.Initialize();

            return levelData;
        }
    }
}