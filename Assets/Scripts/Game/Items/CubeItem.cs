using Game.Core.Item;
using UnityEngine;

namespace Game.Items
{
    public class CubeItem : Item
    {
        private SpriteRenderer spriteRenderer;
		private Sprite nonHint;
		private Sprite hint;
		private bool _hint = true;
	
        
        private ItemType _itemType;
        private ParticleSystem _particleSystem;


        public override ItemType GetItemType()
        {
            return _itemType;
        }

        public override void setItemType(ItemType itemType)
        {
            _itemType = itemType;
        }

        public override bool IsMatchable()
        {
            return true;
        }

        public void SetCubeType(ItemType colorId)
        {
            _itemType = colorId;
        }

        
		public void PrepareHintItem(ItemBase itemBase, Sprite nonHint, Sprite hint)
        {
            
            spriteRenderer = itemBase.SpriteRenderer;
            spriteRenderer.sprite = nonHint;
			this.nonHint = nonHint;
			this.hint = hint;
            FallAnimation = itemBase.FallAnimation;
            FallAnimation.Item = this;		
        }

		public void openHint()
		{
			_hint = true;
		}
		public void closeHint()
		{
			_hint = false;
		}
		public bool isHint()
		{
			return _hint;
		}

		public void ChangeHint(){
			if (isHint()) {
				spriteRenderer.sprite = hint;
			} else {
				spriteRenderer.sprite = nonHint;

			}
			//return base.TryExecute ();
		}

        public override bool TryExecute()
        {
		
			if (_particleSystem == null)
				return base.TryExecute ();
        
			var particle = Instantiate (_particleSystem, transform.position, Quaternion.identity, Cell.Board.ParticlesParent);
			particle.Play ();
        
			return base.TryExecute ();

        }



        public void SetParticle(ParticleSystem particles)
        {
            _particleSystem = particles;
        }
        

    }
}
