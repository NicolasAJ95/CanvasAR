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
    private Vector3 anchorPosition;

    [SerializeField]
    private Vector3[] objectsPosition;

    [SerializeField]
    private Transform[] totalObjects;

    [SerializeField]
    private Queue<Vector3> positionsQueue;
    // Start is called before the first frame update
    void Start()
    {
        //totalObjects = arCanvas.GetComponentsInChildren<Transform>();

        //TO-DO: Create system to auto assign objects to transform array

        /* objectsPosition = new Vector3[objectsTransform.Length];

         for (int i = 0; i <= objectsTransform.Length - 1; i++)
            {
                for(int k = 0; k <= totalObjects.Length - 1; k++)
                {
                 if (totalObjects[k].CompareTag("Object"))
                 {
                     objectsTransform[i] = totalObjects[k].transform;
                     Debug.Log("match");
                 }
                 else
                     Debug.Log("no match");
                }

            }
            */

        AutoFocus();
        arCanvas.GetComponent<Transform>().rotation = new Quaternion(0, 0, 0, 0);

        positionsQueue = new Queue<Vector3>();

        for (int i = 0; i <= objectsTransform.Length - 1; i++)
        {
            Debug.Log("hi");
            objectsPosition[i] = objectsTransform[i].position;
            positionsQueue.Enqueue(objectsPosition[i]);
        }

        foreach (Vector3 position in positionsQueue)
        {
            Debug.Log(position);
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private Vector3 actualPosition;
    private Vector3 nextPosition;

    //Moves the canvas for the next object to be on centre of the screen
    public void SwitchPosition()
    {

        actualPosition = positionsQueue.Dequeue();

        arCanvas.GetComponent<Transform>().position = actualPosition;

        nextPosition = positionsQueue.Peek();

        arCanvas.GetComponent<Transform>().position = -nextPosition;

        positionsQueue.Enqueue(actualPosition);





        /*for (int i = 0; i <= objectsTransform.Length; i++)
        {

            actualPosition = arCanvas.GetComponent<Transform>().position;

            if(i < objectsTransform.Length - 1)
            {
                Debug.Log("normal");
                nextPosition = objectsTransform[i + 1].position - actualPosition;
            }
            else
            {
                Debug.Log("not normal");
                nextPosition = objectsTransform[0].position - actualPosition;
            }

            arCanvas.GetComponent<Transform>().position = -nextPosition;

            //return;

        }*/
    }

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

    private bool rotate;
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

    }
    public void AutoFocus()
    {
        CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
    }
}
