
using UnityEngine;
namespace Utils
{
	public class ScreenManager : MonoBehaviour
	{
		void Awake()
		{
			PrepareCamera();
		}

		private void PrepareCamera()
		{
			var cam = GetComponent<Camera>();
			cam.orthographicSize = (10 / cam.aspect) / 2;
		}
	}
}
