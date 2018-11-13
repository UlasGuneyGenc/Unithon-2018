using Game.Core.Board;
using Game.Mechanics;
using UnityEngine;

namespace Game.Core.Item
{
	public abstract class Item : MonoBehaviour
	{
		public SpriteRenderer SpriteRenderer;
		public FallAnimation FallAnimation;

		private Cell _cell;
		public Cell Cell
		{
			get { return _cell; }
			set
			{
				if (_cell == value) return;
				
				var oldCell = _cell;
				_cell = value;
				
				if (oldCell != null && oldCell.Item == this)
				{
					oldCell.Item = null;
				}

				if (value != null)
				{
					value.Item = this;					
					gameObject.name = _cell.gameObject.name + " "+GetItemType();
				}
				
			}
		}

		public virtual void setItemType(ItemType itemType)
		{
		}

		public abstract ItemType GetItemType();

		public virtual bool IsMatchable()
		{
			return false;
		}
		
		public virtual bool IsFallable()
		{
			return true;
		}
		
		public virtual bool TryExecute()
		{
			Cell.Item = null;
			Cell = null;
			
			Destroy(gameObject);
			return true;
		}

		public void Prepare(ItemBase itemBase, Sprite sprite)
		{
			SpriteRenderer = itemBase.SpriteRenderer;
			SpriteRenderer.sprite = sprite;
			FallAnimation = itemBase.FallAnimation;
			FallAnimation.Item = this;		
		}


		
		public void Fall()
		{
			if (IsFallable())
			{
				FallAnimation.FallTo(Cell.GetFallTarget());
			}
			
		}
		
		public override string ToString()
		{
			return gameObject.name;
		}
		
	}
}
