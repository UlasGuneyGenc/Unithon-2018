using UnityEngine;

namespace Settings
{
	public class GameSettings : MonoBehaviour {

		void Awake ()
		{
			Application.targetFrameRate = 60;
		}
	}
}
