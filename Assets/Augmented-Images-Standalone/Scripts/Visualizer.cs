using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using GoogleARCore;
//using UnityEngine.UI;
public  class Visualizer : MonoBehaviour {

	/// <summary>
	/// The AugmentedImage to visualize.
	/// </summary>
	public AugmentedImage Image;

	//public GameObject VisualizedContent;

    private CanvasRenderer AnnotationsCanvas;
	private AudioSource Audio;
    private VideoPlayer Video;

    void Start()
	{
		this.Audio = this.GetComponent<AudioSource>();
        this.Video = this.GetComponent<VideoPlayer>();
        this.AnnotationsCanvas = this.GetComponent<CanvasRenderer>();

        Video.loopPointReached += VideoContentEnded;
    }

    void VideoContentEnded(VideoPlayer videoPlayer)
    {
        if (!videoPlayer.isLooping)
        {
            //videoPlayer.Stop();
            MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
            meshRenderer.enabled = false; // hides the video
        }
    }

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
	}

    public void Loop() 
    {
        Debug.Log("Loop in Visualizer");
        if (Audio != null)
        {
            if (Audio.loop == false)
                Audio.loop = true;
            else
                Audio.loop = false;
        }
        else
        {
            Debug.Log("Audio is not set!");
        }

        if (Video != null)
        {
            if (Video.isLooping == false)
                Video.isLooping = true;
            else
                Video.isLooping = false;
        }
        else
        {
            Debug.Log("Video component is not set or doesn't exist");
        }
    }

    public void Stop() 
    {
        Debug.Log("Stop in Visualizer");
        if (Audio != null)
        {
            Audio.Stop();
        }
        else
        {
            Debug.Log("Audio is not set!");
        }

        if (Video != null)
        {
            Video.Stop();
        }
        else
        {
            Debug.Log("Video component is not set or doesn't exist");
        }
    }

    public void ShowHide() 
    {
        Debug.Log("ShowHide in Visualizer");

        if (Video != null)
        {
            MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
            if (Video.isPlaying) 
            {
                // Disable mesh rendered (make video invisible) and pause video  
                meshRenderer.enabled = false;
                Video.Pause();
            }
            else
            {
                // Enable mesh rendered (make video visible) and play video
                meshRenderer.enabled = true;
                Video.Play();
            }
        }
        else
        {
            Debug.Log("Video component is not set or doesn't exist");
        }

        if (AnnotationsCanvas != null)
        {
            Canvas canvas = AnnotationsCanvas.GetComponentInChildren<Canvas>();
            if (canvas == null)
            {
                Debug.Log("Canvas from AnnotationCanvas is not found (null)");
                return;
            }

            if (canvas.enabled)
                canvas.enabled = false;
            else
                canvas.enabled = true;
        }
        else
        {
            Debug.Log("AnnotationCanvas(Canvas) component is not set or doesn't exist");
        }
    }
}
