using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface InstructionParent{
    public void StartButtonClick();
}

public class ShapeMatchGameController : MonoBehaviour, InstructionParent
{
    public List<GameObject> shapes;
    public GameObject descriptionUI;
    public GameObject timerControllerGo;
    private TimerController timerController;

    private string gameTitle = "SORT A SHAPE";
    private string gameDescription = "YOU KNOW THIS ONE, JUST PUT THE SHAPE IN THE HOLE. CHOP CHOP!";
    // Diff levels 1 = 3 shapes, 2 = 4 shapes, 3 = 5 shapes
    private int difficulty = 1; 
    private Camera myCamera;
    private System.Random random;
    private double halfHeight;
    private InstructionController controller;
    private int numShapesLeft = 10;


    public void StartMatchGame()
    {
        myCamera = Camera.main;
        random = new System.Random();
        halfHeight = myCamera.orthographicSize;
        GameObject newTC = Instantiate(timerControllerGo);
        timerController = newTC.GetComponent<TimerController>();
        // Show instructions for game
        ShowInstructions();
    }

    void FixedUpdate()
    {
        if(numShapesLeft <= 0)
        {
            // you won
            timerController.StartTimer(false);
        }
    }

    private void ShowInstructions()
    {
        GameObject descriptionGO = Instantiate(descriptionUI);
        controller = descriptionGO.GetComponent<InstructionController>();
        controller.SetTitleText(gameTitle);
        controller.SetDescriptionText(gameDescription);
        controller.SetButtonClickListener(this);
    }

    void InstructionParent.StartButtonClick()
    {
        Debug.Log("In here");
        // Setup where the holes are placed
        SetupHoles();
        // Randomly place where the shapes will be
        SetupShapes();
        controller.HideDescriptionUI();
        // Start timer
        timerController.StartTimer(true);
    }

    private void SetupHoles()
    {
        Vector3 holePlacementPosition = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0f); 
        for (int index = 0; index < shapes.Count; index++) {
            GameObject newHole = shapes[index];
            Vector3 offset = newHole.GetComponent<SpriteRenderer>().bounds.size;
            // Init hole
            newHole.GetComponent<HoleDetails>().SetImageToHole();
            holePlacementPosition = new Vector3(offset.x + holePlacementPosition.x + 1f, 0f, 0f);
            Instantiate(newHole, holePlacementPosition, Quaternion.identity);
        } 
    }

    private void SetupShapes()
    {
        numShapesLeft = 0;
        for (int index = 0; index < shapes.Count; index++) {
            GameObject newShape = shapes[index];
            // Init Shape
            newShape.GetComponent<HoleDetails>().SetImageToShape();
            Instantiate(newShape, new Vector3((float)GetRandomXPosition(), (float)GetRandomYPosition(), 0f), Quaternion.identity);
            numShapesLeft++;
        }
    }

    private int GetRandomXPosition()
    {
        double halfWidth = myCamera.aspect * halfHeight;
        return random.Next(-(int)halfWidth, (int)halfWidth);
    }

    private int GetRandomYPosition()
    {
        return random.Next(-(int)(halfHeight-1), (int)(halfHeight-1));
    }

    public void UpdateNumShapesLeft(int num)
    {
        numShapesLeft += num;
    }

    public void PlayerLost()
    {
        timerController.StartTimer(false);
        GameObject.Find("GameController").GetComponent<GameController>().GameOver();
    }

    public void SetDifficulty(int d)
    {
        difficulty = d;
    }
}

public enum ShapeType {
    Circle, Square, Triangle
}
