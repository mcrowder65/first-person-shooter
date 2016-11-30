using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

static class Utilities
{
	private static List<Vector3> lockoutSpawnPositions = new List<Vector3> (
		                                                     new Vector3[] {
			new Vector3 (4.4f, 53.2f, -70f),
			new Vector3 (-2.44f, 70.73669f, -144.27f),
			new Vector3 (88.73848f, 27.75312f, -5.229659f)
		});
	private static List<Vector3> hearthSpawnPositions = new List<Vector3> (
		                                                    new Vector3[] { new Vector3 (312f, 28.5f, 154.2f) 	});
	private static System.Random rand = new System.Random ();

	public static bool isXboxController ()
	{
		return Input.GetJoystickNames ().Length > 0 && Input.GetJoystickNames () [0].ToString () == "Controller (XBOX 360 For Windows)";
	}

	public static Vector3 getNewRespawnPoint ()
	{
		int min = 0;
		int max = hearthSpawnPositions.Count;
		int randomIndex = rand.Next (min, max);
		Debug.Log (hearthSpawnPositions [randomIndex]);
		return hearthSpawnPositions [randomIndex];
	}
}
