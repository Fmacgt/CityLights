
using System.Collections;
using UnityEngine;


namespace GGJ18
{
	public sealed class PickTileTest : MonoBehaviour
	{
		public Camera rayCamera;
		public MapSetup map;

		public LayerMask targetLayers;

		public int maxWires = 10;

		/////////////////////////////////////////////////////////////////////////////////////

		private void Update()
		{
			if (Input.GetMouseButtonDown(0)) {
				var screenPt = Input.mousePosition;
				var ray = rayCamera.ScreenPointToRay(screenPt);
				RaycastHit hitInfo;
				if (Physics.Raycast(ray, out hitInfo, 100f, targetLayers.value)) {
					var otherRb = hitInfo.rigidbody;
					Debug.LogFormat("hit {0} - {1}", hitInfo.collider, otherRb);
					if (otherRb != null) {
						var wire = otherRb.gameObject.GetComponent<Wire>();
//						bool canToggle = wire.a.isOn || wire.b.isOn;
						if (wire.isOn) {
							wire.toggle();
							map.updateLightings();
						} else {
							bool canToggle = map.TotalOnWires < maxWires;
							if (canToggle) {
								wire.toggle();

								map.updateLightings();
							}
						}
					}
				}
			}
		}
	}
}
