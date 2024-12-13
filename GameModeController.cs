using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject[] objectsToActivate; // Assign objects in the Inspector

    void Start()
    {
        if (Mainmenu.gameStarted)
        {
            Debug.Log("Game started from Main Menu. Activating game objects...");
            foreach (GameObject obj in objectsToActivate)
            {
                if (obj != null)
                    obj.SetActive(true); // Activate the object
            }
        }
        else
        {
            Debug.LogWarning("Game not started from Main Menu. Keeping objects inactive.");
        }
    }
}
