
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GGJ18
{
	public sealed class MapSetup : MonoBehaviour
	{
		public Transform hostTr;

		public int rows = 4;
		public int columns = 4;
		public float steps = 2f;

		public GameObject nodePrefab;
		public GameObject wirePrefab;

		public float onWireProb = 0.3f;


		public int MINIMAL_WIRES = 2;

		//==============================================================================

		private Node[,] _tiles;
		private List<Wire> _wires;

		/////////////////////////////////////////////////////////////////////////////////////

		public void rebuild(int rows, int columns)
		{
			this.rows = rows;
			this.columns = columns;

			_tiles = new Node[rows, columns];

			var center = hostTr.position + Vector3.up;
			var right = Vector3.right * steps;
			var up = Vector3.forward * steps;
			var corner = center - right * columns * 0.5f - up * rows * 0.5f;
			for (int row = 0; row < rows; row++) {
				for (int column = 0; column < columns; column++) {
					var nodeObj = Instantiate(nodePrefab, hostTr) as GameObject;

					var pos = corner + row * up + column * right;
					nodeObj.transform.position = pos;


					_tiles[row, column] = nodeObj.GetComponent<Node>();
					_tiles[row, column].wireCount = 0;
				}
			}


			// randomly assign connections
			foreach (var wire in _wires) {
				Destroy(wire.gameObject);
			}
			_wires.Clear();

			for (int row = 0; row < rows; row++) {
				for (int wireIdx = 0; wireIdx < columns - 1; wireIdx++) {
//					bool wired = Random.Range(0, 10) % 2 == 0;
					bool wired = true;
					if (wired) {
						var wireObj = Instantiate(wirePrefab, hostTr);

						var wire = wireObj.GetComponent<Wire>();
						wire.connect(_tiles[row, wireIdx], _tiles[row, wireIdx + 1]);
						wire.toggle(Random.value <= onWireProb);


						_wires.Add(wire);
					}
				}
			}

			for (int column = 0; column < columns; column++) {
				for (int wireIdx = 0; wireIdx < columns - 1; wireIdx++) {
					//bool wired = Random.Range(0, 10) % 2 == 0;
					bool wired = true;
					if (wired) {
						var wireObj = Instantiate(wirePrefab, hostTr);

						var wire = wireObj.GetComponent<Wire>();
						wire.connect(_tiles[wireIdx, column], _tiles[wireIdx + 1, column]);
						wire.toggle(Random.value <= onWireProb);


						_wires.Add(wire);
					}
				}
			}


			updateLightings();
		}


		public void updateLightings()
		{
			for (int row = 0; row < rows; row++) {
				for (int column = 0; column < columns; column++) {
					var tile = _tiles[row, column];
					tile.wireCount = 0;
					tile.toggle(false);
				}
			}

			foreach (var wire in _wires) {
				if (wire.isOn) {
					wire.a.wireCount++;
					wire.b.wireCount++;
				}
			}

			for (int row = 0; row < rows; row++) {
				for (int column = 0; column < columns; column++) {
					var tile = _tiles[row, column];
					tile.toggle(tile.wireCount >= MINIMAL_WIRES);
					/**
					Debug.LogFormat("[{0}, {1}] has {2} on wires=> {3}", row, column, tile.wireCount,
							tile.isOn);
					**/
				}
			}
		}

		/////////////////////////////////////////////////////////////////////////////////////

		private void Awake()
		{
			_wires = new List<Wire>();
			rebuild(rows, columns);
		}
	}
}
