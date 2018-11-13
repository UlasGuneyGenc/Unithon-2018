using Game.Core.Item;
using UnityEditor.VersionControl;
using UnityEngine;

namespace Game.Items
{
    public class BalloonItem : Item
    {
      
        public override ItemType GetItemType()
        {
            return ItemType.Balloon;
        }        
        
        public override bool IsMatchable()
        {
            return false;
        }
        public override bool IsFallable()
        {
            return true;
        }



    }
}
