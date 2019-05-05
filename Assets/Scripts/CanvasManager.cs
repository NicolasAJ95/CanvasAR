using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    // Start is called before the first frame update
    void Start()
    {
        totalObjects = arCanvas.GetComponentsInChildren<Transform>();

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

        for (int i = 0; i <= objectsTransform.Length - 1; i++)
        {
            Debug.Log("hi");
            objectsPosition[i] = objectsTransform[i].position;
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
        for (int i = 0; i <= objectsTransform.Length; i++)
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

            return;

        }
    }
}
