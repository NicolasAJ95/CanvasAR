using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class AutoRotate : MonoBehaviour, ITrackableEventHandler
{

    private TrackableBehaviour mTrackableBehaviour;

    [SerializeField]
    private GameObject arCanvas;
    [SerializeField]
    private GameObject arCanvasUI;


    void Start()
    {

        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
        {
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
        }
    }

    public void OnTrackableStateChanged(
                                    TrackableBehaviour.Status previousStatus,
                                    TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            arCanvas.SetActive(true);
            arCanvas.GetComponent<Transform>().rotation = new Quaternion(0, 0, 0, 0);
            arCanvasUI.SetActive(true);
        }
        else
        {
            arCanvas.SetActive(false);
            arCanvasUI.SetActive(false);
        }
    }
}
