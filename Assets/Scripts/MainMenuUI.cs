using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public void StartTwoPlayerMode()
    {
        SceneManager.LoadScene("TwoPlayerGame"); // À créer plus tard
    }

    public void StartVsComputerMode()
    {
        SceneManager.LoadScene("VsComputerGame"); // À créer plus tard
    }
}
