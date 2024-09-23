using UnityEngine;
using UnityEngine.UI;

//게임에 관리를 담당.
public class GG_GameManager : MonoBehaviour
{
    public GG_TileBoard board;

    public Text txt_Score;
    public Text txt_BestScore;

    int score;



    private void Start()
    {
        NewGame_2048();
    }

    public void NewGame_2048()
    {
        SetScore(0);
        txt_BestScore.text = LoadBestScore().ToString();


        board.ClearBoard();
        board.CreateTile();
        board.CreateTile();
        board.enabled = true;
    }

    public void GameOver_2048()
    {
        board.enabled = false;
    }

    public void IncreaseScore(int _points)
    {
        SetScore(score + _points);
    }

    void SetScore(int _score)
    {
        this.score = _score;
        txt_Score.text = _score.ToString();

        SaveBestScore();
    }

    void SaveBestScore()
    {
        int bestscore = LoadBestScore();

        if (score > bestscore)
            PlayerPrefs.SetInt("bestscore", score);
    }

    int LoadBestScore()
    {
        return PlayerPrefs.GetInt("bestscore",0);
    }
}
