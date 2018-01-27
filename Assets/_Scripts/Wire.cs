
using System.Collections;
using UnityEngine;


namespace GGJ18
{
	public sealed class Wire : MonoBehaviour
	{
		public Transform wireTr;
		public MeshRenderer wireRenderer;

		public Color connectedColor = Color.blue;
		public Color disconnectedColor = Color.black;


		public Node a;
		public Node b;

		public bool isOn = false;

		//==============================================================================

		private MaterialPropertyBlock _matBlk;

		/////////////////////////////////////////////////////////////////////////////////////

		private void Awake()
		{
			_matBlk = new MaterialPropertyBlock();
		}

		/////////////////////////////////////////////////////////////////////////////////////

		public void toggle()
		{
			toggle(!isOn);
		}

		public void toggle(bool isOn)
		{
			this.isOn = isOn;
			/**
			if (isOn) {
				a.wireCount++;
				b.wireCount++;
			} else {
				a.wireCount--;
				b.wireCount--;
			}
			**/

			_matBlk.SetColor("_Color", isOn ? connectedColor : disconnectedColor);
			wireRenderer.SetPropertyBlock(_matBlk);
		}

		public void connect(Node a, Node b)
		{
			this.a = a;
			this.b = b;

			var pos = (a.transform.position + b.transform.position) * 0.5f;
			wireTr.position = pos;
			wireTr.rotation = Quaternion.LookRotation(a.transform.position - b.transform.position);
		}
	}
}
