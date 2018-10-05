using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using GoogleARCore;
public  class Visualizer : MonoBehaviour {

	/// <summary>
	/// The AugmentedImage to visualize.
	/// </summary>
	public AugmentedImage Image;

	//public GameObject VisualizedContent;

	private AudioSource Audio;
    private VideoPlayer Video;

	public void PlayPause() 
	{
		Debug.Log("PlayPause in Visualizer");
        if (Audio != null)
        {
            if (Audio.isPlaying)
                Audio.Pause();
            else
                Audio.Play();
        }
        else
        {
            Debug.Log("Audio is not set!");
        }

        if (Video != null)
        {
            if (Video.isPlaying)
                Video.Pause();
            else
                Video.Play();
        }
        else
        {
            Debug.Log("Video component is not set or doesn't exist");
        }

        //GetComponent<AudioSource>().Pause();
	}

	void Start()
	{
		this.Audio = this.GetComponent<AudioSource>();
        this.Video = this.GetComponent<VideoPlayer>();
        //TODO:annotations
        Debug.Log("Audio is set! " + this.Audio.name);
    }

	void Update()
	{
   //         if (Image == null || Image.TrackingState != TrackingState.Tracking)
   //         {
			//	VisualizedContent.SetActive(false);
   //             return;
   //         }

   //         float halfWidth = Image.ExtentX / 2;
   //         float halfHeight = Image.ExtentZ / 2;
   //         // FrameUpperRight.transform.localPosition = (halfWidth * Vector3.right) + (halfHeight * Vector3.forward);
			
			
			//// Try this 
			////VisualizedContent.transform.position = Image.CenterPose.position;
			////VisualizedContent.transform.rotation = Image.CenterPose.rotation;

			//// localPosition is relative to the parent's transform.position // changing scale will scale units of measurement
			//VisualizedContent.transform.localPosition = (halfWidth * Vector3.right) + (halfHeight * Vector3.forward); 

			//VisualizedContent.SetActive(true);
	}

	//public abstract void Activate();
	//public abstract void Deactivate();
}
