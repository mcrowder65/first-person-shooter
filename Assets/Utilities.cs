using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.SceneManagement;

static class Utilities
{
	
	public static System.Random rand = new System.Random ();
    private static List<Respawn> lockoutSpawnPositions = new List<Respawn>(
                                                            new Respawn[] {
            new Respawn(new Vector3 (-2.44f, 70.73669f, -144.27f), new Vector3(0f, 81f, 0f),  new Vector3(3f, -90f, 0), new Vector3(0f, 3f, 3.5f)),
            new Respawn(new Vector3 (88.73848f, 27.75312f, -5.229659f), new Vector3(0f, 334f, 0f),  new Vector3(9.744f, -90f, 0f), new Vector3(0f, 17f, 9.54f))
       });
    private static List<Respawn> hearthSpawnPositions = new List<Respawn>(
                                                            new Respawn[] {
            new Respawn(new Vector3 (312f, 28.5f, 154.2f), new Vector3(0f, 0f, 0f),  new Vector3(0f, -90f, 0f), new Vector3(0f, 0f, 0f)),
            new Respawn(new Vector3 (146.5f, 28.8f, 169.5f), new Vector3(0f, 0f, 0f),  new Vector3(0f, -90f, 0f), new Vector3(0f, 0f, 0f)),
            new Respawn(new Vector3(150f, 31.09f, 52.6f), new Vector3(0, 0, 0f),  new Vector3(0f, -90f, 0f), new Vector3(0f, 0f, 0f)),
            new Respawn(new Vector3(376f, 26.3f, 356.4f), new Vector3(0, 0, 0),  new Vector3(0f, -90f, 0f), new Vector3(0f, 0f, 0f))
        });
    public static bool isXboxController ()
	{
		return Input.GetJoystickNames ().Length > 0 && Input.GetJoystickNames () [0].ToString () == "Controller (XBOX 360 For Windows)";
	}

	public static Respawn getNewRespawnPoint ()
	{
		string sceneName = SceneManager.GetActiveScene ().name;
		int min = 0;
		int max = sceneName == "Lockout" ? lockoutSpawnPositions.Count : hearthSpawnPositions.Count;
		int randomIndex = rand.Next (min, max);
		return sceneName == "Lockout" ? lockoutSpawnPositions [randomIndex] : hearthSpawnPositions [randomIndex];
	}

    public static SceneEnum GetCurrentScene()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "Lockout":
                return SceneEnum.Lockout;
            case "Hearth":
                return SceneEnum.Hearth;
            default:
                throw new UnityException("Unimplemented current scene.");
        }
       
    }

    public static float GetDeathY(SceneEnum currentScene)
    {
        switch (currentScene)
        {
            case SceneEnum.Lockout:
                return Constants.LOCKOUT_DEATH_Y;
            case SceneEnum.Hearth:
                return Constants.HEARTH_DEATH_Y;
            default:
                throw new UnityException("Unimplemented death y.");
        }
    }

   
}
