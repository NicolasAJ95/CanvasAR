using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class CanvasManager : MonoBehaviour
{

    [SerializeField]
    private GameObject arCanvas;

    [SerializeField]
    private Transform[] objectsTransform;

    [SerializeField]
    private Vector3[] objectsPosition;

    [SerializeField]
    private Transform[] totalObjects;

    [SerializeField]
    private Queue<Vector3> positionsQueue;
    [SerializeField]
    private Queue<Vector3> lastPositionsQueue;

    // Start is called before the first frame update
    void Start()
    {


        positionsQueue = new Queue<Vector3>();
        lastPositionsQueue = new Queue<Vector3>();

        for (int i = 0; i <= objectsTransform.Length - 1; i++)
        {
            objectsPosition[i] = objectsTransform[i].position;
            positionsQueue.Enqueue(objectsPosition[i]);
        }

/*        for(int i = 8; i <= 0; i--)
        {
            Debug.Log("hi");
            lastPositionsQueue.Enqueue(objectsPosition[i]);
        }*/

        foreach (Vector3 position in positionsQueue)
        {
            Debug.Log(position);
        }

        AutoFocus();

    }

    [SerializeField]
    private Vector3 actualPosition;
    [SerializeField]
    private Vector3 nextPosition;

    //Moves the canvas for the next object to be on centre of the screen
    public void SwitchPosition()
    {

        actualPosition = positionsQueue.Dequeue();

        arCanvas.GetComponent<Transform>().position = actualPosition;

        nextPosition = positionsQueue.Peek();

        arCanvas.GetComponent<Transform>().position = -nextPosition;

        positionsQueue.Enqueue(actualPosition);

    }
/*
    private void GetActualPosition()
    {
        actualPosition = positionsQueue.Dequeue();

        arCanvas.GetComponent<Transform>().position = actualPosition;
    }*/

  /*  public void SwitchToNextPosition()
    {

        nextPosition = positionsQueue.Peek();

        arCanvas.GetComponent<Transform>().position = -nextPosition;

        positionsQueue.Enqueue(actualPosition);

    }*/

    private bool isZoomed = false;
    [SerializeField]
    private Vector3 zoomPosition;
    [SerializeField]
    private Vector3 lastPosition;

    public void Zoom()
    {
        if (!isZoomed)
        {
            Debug.Log("zoom in");
            isZoomed = true;
            lastPosition = arCanvas.GetComponent<Transform>().position;
            arCanvas.GetComponent<Transform>().position = zoomPosition;

        }
        else
        {
            Debug.Log("Zoom Out");
            isZoomed = false;
            arCanvas.GetComponent<Transform>().position = lastPosition;

        }
    }

    private bool lightOn;

    public void SwitchFlash()
    {
        if (!lightOn)
        {
            lightOn = true;
            CameraDevice.Instance.SetFlashTorchMode(true);

        }
        else
        {
            lightOn = false;
            CameraDevice.Instance.SetFlashTorchMode(false);
        }
    }

   /* private bool rotate;
    public void RotateObject()
    {
        if (!rotate)
        {
            rotate = true;
            arCanvas.GetComponent<Transform>().rotation = new Quaternion(0, 180, 0, 0);

        }
        else
        {
            rotate = false;
            arCanvas.GetComponent<Transform>().rotation = new Quaternion(0, 0, 0, 0);
        }

    }*/
    public void AutoFocus()
    {
        CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
    }

    public void Focus()
    {
        CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_NORMAL);
    }
}
