using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class VideoManager : MonoBehaviour, ITrackableEventHandler
{

    private TrackableBehaviour mTrackableBehaviour;

    [SerializeField]
    private GameObject videoPlayer;
    [SerializeField]
    private GameObject videoPlayerUI;

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
            videoPlayer.GetComponent<UnityEngine.Video.VideoPlayer>().Play();
            videoPlayerUI.SetActive(true);
        }
        else
        {
            videoPlayer.GetComponent<UnityEngine.Video.VideoPlayer>().Pause();
            videoPlayerUI.SetActive(false);
        }
    }
}

