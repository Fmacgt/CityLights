
using System.Collections;
using UnityEngine;


namespace GGJ18
{
	public sealed class Node : MonoBehaviour
	{
		public GameObject darknessObj;
		public MeshRenderer nodeRenderer;

		public Color offColor = Color.black;
		public Color onColor = Color.yellow;

		public bool isOn = true;

		public bool[] connectionStatus = new bool[4] {
			false, false, false, false
		};

		public int wireCount = 0;

		//==============================================================================

		private MaterialPropertyBlock _matBlk;
		private bool _isOn = false;

		/////////////////////////////////////////////////////////////////////////////////////

		public void toggle()
		{
			toggle(!_isOn);
		}

		public void toggle(bool isOn)
		{
			_isOn = isOn;

			darknessObj.SetActive(!_isOn);

			_matBlk.SetColor("_Color", _isOn ? onColor : offColor);
			nodeRenderer.SetPropertyBlock(_matBlk);
		}

		/////////////////////////////////////////////////////////////////////////////////////

		private void Awake()
		{
			_matBlk = new MaterialPropertyBlock();
		}
	}
}
