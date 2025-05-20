using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public void StartTwoPlayerMode()
    {
        SceneManager.LoadScene("TwoPlayerGame"); // � cr�er plus tard
    }

    public void StartVsComputerMode()
    {
        SceneManager.LoadScene("VsComputerGame"); // � cr�er plus tard
    }
}
