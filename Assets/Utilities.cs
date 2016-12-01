using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.SceneManagement;

static class Utilities
{
	
	private static System.Random rand = new System.Random ();
    private static List<Vector3> lockoutSpawnPositions = new List<Vector3>(
                                                            new Vector3[] {
            new Vector3 (-2.44f, 70.73669f, -144.27f),
            new Vector3 (88.73848f, 27.75312f, -5.229659f)
       });
    private static List<Vector3> hearthSpawnPositions = new List<Vector3>(
                                                            new Vector3[] {
            new Vector3 (312f, 28.5f, 154.2f),
            new Vector3 (146.5f, 28.8f, 169.5f)
        });
    public static bool isXboxController ()
	{
		return Input.GetJoystickNames ().Length > 0 && Input.GetJoystickNames () [0].ToString () == "Controller (XBOX 360 For Windows)";
	}

	public static Vector3 getNewRespawnPoint ()
	{
		string sceneName = SceneManager.GetActiveScene ().name;
		int min = 0;
		int max = sceneName == "Lockout" ? lockoutSpawnPositions.Count : hearthSpawnPositions.Count;
		int randomIndex = rand.Next (min, max);
		return sceneName == "Lockout" ? lockoutSpawnPositions [randomIndex] : hearthSpawnPositions [randomIndex];
	}
}
