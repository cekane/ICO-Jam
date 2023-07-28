using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleDetails : MonoBehaviour
{
    public ShapeType shapeType;
    public bool isHole;
    public Sprite shapeImg;
    public Sprite holeImg;
    private ShapeType? currentCollision = null;
    private bool dragging = false;
    private float distance;

    public void SetImageToShape()
    {
        isHole = false;
        gameObject.GetComponent<SpriteRenderer>().sprite = shapeImg;
        gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
    }

    public void SetImageToHole()
    {
        isHole = true;
        gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
        gameObject.GetComponent<SpriteRenderer>().sprite = holeImg;
    }

    void OnMouseDown()
    {
        distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        if(!isHole)
        {
            dragging = true;
        }
    }
 
    void OnMouseUp()
    {
        dragging = false;
        if(!isHole && CheckCurrentCollision())
        {
            // THIS IS WRONG BUT I DON'T CARE
            GameObject.Find("ShapesGame(Clone)").GetComponent<ShapeMatchGameController>().UpdateNumShapesLeft(-1);
            Destroy(gameObject);
        }
    }

    private bool CheckCurrentCollision()
    {
        if(currentCollision!= null && currentCollision == this.shapeType)
        {
            return true;
        } 
        else if (currentCollision!= null && currentCollision != this.shapeType)
        {
            GameObject.Find("ShapesGame(Clone)").GetComponent<ShapeMatchGameController>().PlayerLost();
        }
        return false;
    }
 
    void Update()
    {
        if (dragging)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 rayPoint = ray.GetPoint(distance);
            transform.position = new Vector3(rayPoint.x, rayPoint.y, 0f);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(!isHole){
            currentCollision = other.gameObject.GetComponent<HoleDetails>().shapeType;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        currentCollision = null;
    }
}
