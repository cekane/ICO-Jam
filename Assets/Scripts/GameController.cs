using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    public List<GameObject> possibleGames;
    private List<GameObject> gameList = new List<GameObject>();
    int currentGameIndex = 0;
    private System.Random random;
    // Start is called before the first frame update
    void Start()
    {
        random = new System.Random();
        int size = possibleGames.Count;
        // Generate list of games
        for(int index = 0; index < size; index++)
        {
            int randomIndex = random.Next(0, possibleGames.Count);
            gameList.Add(possibleGames[randomIndex]);
            possibleGames.RemoveAt(randomIndex);
        }
        // Play first game
        PlayGame();
    }

    public void PlayGame()
    {
        switch(gameList[currentGameIndex].name)
        {
            case "ShapesGame":
                GameObject shapesGame = gameList[currentGameIndex].gameObject;
                Instantiate(shapesGame);
                shapesGame.GetComponent<ShapeMatchGameController>().StartMatchGame();
                break;
        }
    }

    public void PlayNextGame()
    {
        currentGameIndex ++;
        PlayGame();
    }

    public void GameOver()
    {

    }
}
