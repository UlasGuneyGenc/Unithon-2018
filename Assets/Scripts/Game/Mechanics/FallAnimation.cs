using DG.Tweening;
using Game.Core.Board;
using Game.Core.Item;
using UnityEngine;

namespace Game.Mechanics
{
	public class FallAnimation : MonoBehaviour
	{
		public Item Item;
		[HideInInspector] public  Cell TargetCell;

		private static float _startVel = 0F;
		private static float _acc = 0.4F;
		private static float _maxSpeed = 20F;
		
		private float _vel = _startVel;
		
		private bool _isFalling;
		private Vector3 _targetPosition;
		private Sequence _jumpSequence;
		
		public void FallTo(Cell targetCell)
		{
			if (TargetCell != null && targetCell.Y >= TargetCell.Y) return;
			TargetCell = targetCell;
			Item.Cell = TargetCell;
			_targetPosition = TargetCell.transform.position;
			_isFalling = true;
		}

		public void Update()
		{
			if(!_isFalling) return;
			_vel += _acc;
			_vel = _vel >= _maxSpeed? _maxSpeed : _vel;
			var p = Item.transform.position;
			p.y -= _vel*Time.deltaTime;
			if (p.y <= _targetPosition.y)
			{
				_isFalling = false;
				p.y = _targetPosition.y;
				_vel = _startVel;
			}
			Item.transform.position = p;
		}
		
	}
}
