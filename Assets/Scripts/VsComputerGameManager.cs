using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VsComputerGameManager : MonoBehaviour
{
    public Button[] cells;
    public Sprite xSprite;
    public Sprite oSprite;
    public TextMeshProUGUI currentPlayerText;
    public TextMeshProUGUI resultText;
    public Button replayButton;
    public Button backButton;

    public AudioSource victoryAudioSource;
    public AudioSource buttonClickComputerAudioSource;
    public AudioSource drawAudioSource;

    private string[] board = new string[9];
    private bool isPlayerTurn = true;
    private bool gameOver = false;

    void Start()
    {
        replayButton.gameObject.SetActive(false);
        for (int i = 0; i < cells.Length; i++)
        {
            int index = i;
            cells[i].onClick.AddListener(() => OnCellClicked(index));
        }
        UpdateUI();
    }

    void OnCellClicked(int index)
    {
        if (board[index] != null || gameOver || !isPlayerTurn) return;

        board[index] = "X";
        cells[index].image.sprite = xSprite;
        cells[index].interactable = false;

        if (CheckWin("X"))
        {
            EndGame("Joueur X gagne !");
            PlayVictorySound();
            return;
        }
        else if (IsDraw())
        {
            EndGame("Match nul !");
            PlayDrawSound();
            return;
        }

        isPlayerTurn = false;
        UpdateUI();
        Invoke("ComputerMove", 1f);
    }

    void ComputerMove()
    {
        int move = GetBestMove();
        board[move] = "O";
        cells[move].image.sprite = oSprite;
        cells[move].interactable = false;
        PlayClickComputerSound();

        if (CheckWin("O"))
        {
            EndGame("Ordinateur gagne !");
            PlayVictorySound();
            return;
        }
        else if (IsDraw())
        {
            EndGame("Match nul !");
            PlayDrawSound();
            return;
        }

        isPlayerTurn = true;
        UpdateUI();
    }

    int GetBestMove()
    {
        // Mouvement aléatoire simple
        int[] emptyCells = System.Linq.Enumerable.Range(0, 9).Where(i => board[i] == null).ToArray();
        return emptyCells[Random.Range(0, emptyCells.Length)];
    }

    bool CheckWin(string player)
    {
        int[,] wins = new int[,] {
            {0,1,2}, {3,4,5}, {6,7,8},  // Rows
            {0,3,6}, {1,4,7}, {2,5,8},  // Cols
            {0,4,8}, {2,4,6}            // Diags
        };

        for (int i = 0; i < wins.GetLength(0); i++)
        {
            if (board[wins[i, 0]] == player &&
                board[wins[i, 1]] == player &&
                board[wins[i, 2]] == player)
                return true;
        }
        return false;
    }

    bool IsDraw()
    {
        return System.Array.TrueForAll(board, cell => cell != null);
    }

    void EndGame(string message)
    {
        gameOver = true;
        resultText.text = message;
        replayButton.gameObject.SetActive(true);
    }

    void UpdateUI()
    {
        currentPlayerText.text = isPlayerTurn ? "Joueur X" : "Ordinateur";
    }

    public void Replay()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("VsComputerGame");
    }

    void PlayVictorySound()
    {
        victoryAudioSource.Play();
    }

    void PlayDrawSound()
    {
        drawAudioSource.Play();
    }

    public void PlayClickComputerSound()
    {
        buttonClickComputerAudioSource.Play();
    }

    public void GoBack()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
