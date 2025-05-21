using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManagerTwoPlayer : MonoBehaviour
{
    public Sprite crossSprite;
    public Sprite circleSprite;

    public TextMeshProUGUI statusText;
    public GameObject replayButton;

    public AudioSource victoryAudioSource;
    public AudioSource drawAudioSource;

    private string currentPlayer = "X";
    private string[,] board = new string[3, 3];
    private bool gameEnded = false;

    public void OnCellClicked(Button cellButton)
    {
        // Récupère les coordonnées de la cellule depuis son nom
        string[] indices = cellButton.name.Split('_');
        int x = int.Parse(indices[1]);
        int y = int.Parse(indices[2]);

        if (gameEnded || board[x, y] != null)
            return;

        board[x, y] = currentPlayer;

        Image img = cellButton.GetComponent<Image>();
        img.sprite = currentPlayer == "X" ? crossSprite : circleSprite;
        cellButton.interactable = false;

        if (CheckWin())
        {
            gameEnded = true;
            statusText.text = $"Le joueur {currentPlayer} a gagné !";
            PlayVictorySound();
            replayButton.SetActive(true);
        }
        else if (CheckDraw())
        {
            gameEnded = true;
            statusText.text = "Match nul !";
            PlayDrawSound();
            replayButton.SetActive(true);
        }
        else
        {
            currentPlayer = currentPlayer == "X" ? "O" : "X";
            statusText.text = $"Joueur {currentPlayer}";
        }
    }

    private bool CheckWin()
    {
        // Lignes et colonnes
        for (int i = 0; i < 3; i++)
        {
            if (board[i, 0] != null && board[i, 0] == board[i, 1] && board[i, 1] == board[i, 2]) return true;
            if (board[0, i] != null && board[0, i] == board[1, i] && board[1, i] == board[2, i]) return true;
        }

        // Diagonales
        if (board[0, 0] != null && board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2]) return true;
        if (board[0, 2] != null && board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0]) return true;

        return false;
    }

    private bool CheckDraw()
    {
        foreach (var cell in board)
            if (cell == null) return false;
        return true;
    }

    public void ReplayGame()
    {
        board = new string[3, 3];
        currentPlayer = "X";
        gameEnded = false;
        statusText.text = $"Joueur {currentPlayer}";
        replayButton.SetActive(false);

        foreach (Transform cell in GameObject.Find("BoardGrid").transform)
        {
            Button btn = cell.GetComponent<Button>();
            btn.interactable = true;
            btn.GetComponent<Image>().sprite = null;
        }
    }

    void PlayVictorySound()
    {
        victoryAudioSource.Play();
    }

    void PlayDrawSound()
    {
        drawAudioSource.Play();
    }

    public void BackToMenu()
    {
        // Retourne au menu principal
        SceneManager.LoadScene("MainMenu");
    }
}
