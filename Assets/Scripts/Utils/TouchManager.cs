using Game.Core.Board;
using UnityEngine;

namespace Utils
{
	public class TouchManager : MonoBehaviour
	{
		private const string CellCollider = "CellCollider";
	
		public Camera Camera;
		public Board Board;
	
		void Update () {
		
#if UNITY_EDITOR
			GetTouchEditor();
#else
		GetTouchMobile();
		#endif
		}

		private void GetTouchEditor()
		{
			if (Input.GetMouseButtonUp(0))
			{
				ExecuteTouch(Input.mousePosition);
			}
		}

		private void GetTouchMobile()
		{
			var touch = Input.GetTouch(0);
			switch (touch.phase)
			{
				case TouchPhase.Ended:
				case TouchPhase.Canceled:
					ExecuteTouch(touch.position);
					break;
			}
		}

		private void ExecuteTouch(Vector3 pos)
		{
			var hit = Physics2D.OverlapPoint(Camera.ScreenToWorldPoint(pos)) as BoxCollider2D;
		
			if (hit !=null && hit.CompareTag(CellCollider))
			{
				Board.CellTapped(hit.gameObject.GetComponent<Cell>());
			}
		}
	}
}
