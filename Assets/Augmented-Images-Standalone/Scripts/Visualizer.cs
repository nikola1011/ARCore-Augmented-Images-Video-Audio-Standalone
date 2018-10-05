using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;
public  class Visualizer : MonoBehaviour {

	/// <summary>
	/// The AugmentedImage to visualize.
	/// </summary>
	public AugmentedImage Image;

	public GameObject VisualizedContent;

	public AudioSource Audio;

	public void PlayPause() 
	{
		Debug.Log("PlayPause in Visualizer");
		if (Audio.isPlaying)
			Audio.Pause();
		else
			Audio.Play();
			
		GetComponent<AudioSource>().Pause();
	}

	void Start()
	{
		this.Audio = this.GetComponent<AudioSource>();
	}

	void Update()
	{
            if (Image == null || Image.TrackingState != TrackingState.Tracking)
            {
				VisualizedContent.SetActive(false);
                return;
            }

            float halfWidth = Image.ExtentX / 2;
            float halfHeight = Image.ExtentZ / 2;
            // FrameUpperRight.transform.localPosition = (halfWidth * Vector3.right) + (halfHeight * Vector3.forward);
			
			
			// Try this 
			//VisualizedContent.transform.position = Image.CenterPose.position;
			//VisualizedContent.transform.rotation = Image.CenterPose.rotation;

			// localPosition is relative to the parent's transform.position // changing scale will scale units of measurement
			VisualizedContent.transform.localPosition = (halfWidth * Vector3.right) + (halfHeight * Vector3.forward); 

			VisualizedContent.SetActive(true);
	}

	//public abstract void Activate();
	//public abstract void Deactivate();
}
