using Game.Core.Item;
using UnityEditor.VersionControl;
using UnityEngine;

namespace Game.Items
{
    public class CrateItem : Item
    {
        private SpriteRenderer spriteRenderer;
  
        private Sprite crateOpen;
        private bool cratelock=true;
        public override ItemType GetItemType()
        {
            return ItemType.Crate;
        }        
        
        public override bool IsMatchable()
        {
            return false;
        }
        public override bool IsFallable()
        {
            return false;
        }

        public void openLock()
        {
            cratelock = false;
        }
        public bool isLocked()
        {
            return cratelock;
        }
         
        public void PrepareCrateItem(ItemBase itemBase, Sprite crateLock, Sprite crateOpen)
        {
            
            spriteRenderer = itemBase.SpriteRenderer;

            this.crateOpen = crateOpen;
            spriteRenderer.sprite = crateLock;
            FallAnimation = itemBase.FallAnimation;
            FallAnimation.Item = this;		
        }

        public override bool TryExecute()
        {
            if (isLocked())
            {
                openLock();
                //sprite change 
                spriteRenderer.sprite = crateOpen;     
                return true;
            }
            else
            {
                return base.TryExecute();
            }
            // else change sprite   return base.TryExecute();


        }


    }
}
