
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace GGJ18
{
	public sealed class ReloadScene : MonoBehaviour
	{
		public void reload()
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}

		/////////////////////////////////////////////////////////////////////////////////////

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.R)) {
				reload();
			}
		}
	}
}
