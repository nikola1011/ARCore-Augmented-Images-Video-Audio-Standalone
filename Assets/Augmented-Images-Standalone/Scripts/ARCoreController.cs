using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using GoogleARCore; // !important in order for ARCore SDK functions and components access

public class ARCoreController : MonoBehaviour 
{

	/// <summary>
	/// A prefab for visualizing an AugmentedImage.
	/// </summary>
	public Visualizer VisualizerPrefab; // Prefab used for initial testing (one visualizer)

	public List<Visualizer> VisualizerPrefabs = new List<Visualizer>(); // PrefabS used for real application (more visualizerS)
	private float ZoomFactor = 0.01f;
	private float RotateFactor = 40f;
///Test
	public VideoPlayer video;
	public VideoSource videoSource;
	public VideoClip videoClip;
	public new AudioSource audio;
	public AudioClip audioClip;
	public GameObject annotation;
///

	/// <summary>
	/// The overlay containing the fit to scan user guide.
	/// </summary>
	public GameObject FitToScanOverlay;

	private Dictionary<int, Visualizer> m_Visualizers
		= new Dictionary<int, Visualizer>();

	private List<AugmentedImage> m_TempAugmentedImages = new List<AugmentedImage>();


	public void Start() 
	{
		FitToScanOverlay.SetActive(true);
	}

	/// <summary>
	/// The Unity Update method.
	/// </summary>
	public void Update()
	{
		// Exit the app when the 'back' button is pressed.
		if (Input.GetKey(KeyCode.Escape))
		{
			Application.Quit();
		}

		// Check that motion tracking is tracking.
		if (Session.Status != SessionStatus.Tracking)
		{
			return;
		}

		// Get updated augmented images for this frame.
		Session.GetTrackables<AugmentedImage>(m_TempAugmentedImages, TrackableQueryFilter.Updated);

		// Create visualizers and anchors for updated augmented images that are tracking and do not previously
		// have a visualizer. Remove visualizers for stopped images.
		foreach (var image in m_TempAugmentedImages)
		{
			Visualizer visualizer = null;
			m_Visualizers.TryGetValue(image.DatabaseIndex, out visualizer);

			if (image.TrackingState == TrackingState.Tracking && visualizer == null)
			{
				// Create an anchor to ensure that ARCore keeps tracking this augmented image.
				Anchor anchor = image.CreateAnchor(image.CenterPose);
				visualizer = Instantiate(VisualizerPrefabs[image.DatabaseIndex], anchor.transform);
				
				visualizer.Image = image;
				visualizer.gameObject.SetActive(true);
				m_Visualizers.Add(image.DatabaseIndex, visualizer);
			}
			else if (image.TrackingState == TrackingState.Stopped && visualizer != null)
			{
				visualizer.gameObject.SetActive(false);
				//m_Visualizers.Remove(image.DatabaseIndex);
				//GameObject.Destroy(visualizer.gameObject);
			}
		}

		//Visualizer activeVisualizer = GetActiveVisualizer(m_Visualizers);
		
		// Handle touch input  // MOVE THIS CODE TO Visualizer.cs (and try it there)
		//PinchtoZoom(activeVisualizer);
		//Rotate(activeVisualizer); 

		// Show the fit-to-scan overlay if there are no images that are Tracking.
		foreach (var visualizer in m_Visualizers.Values)
		{
			if (visualizer.Image.TrackingState == TrackingState.Tracking)
			{
				FitToScanOverlay.SetActive(false);
				return;
			}
		}

		FitToScanOverlay.SetActive(true);
	}

	public void PlayPause()
	{
		Debug.Log("PlayPause in Controller");
		VisualizerPrefabs[0].PlayPause();

		// if (VisualizerPrefabs[0].isActiveAndEnabled)
		// {
			// AudioSource audioSource = VisualizerPrefabs[0].gameObject.GetComponent<AudioSource>();
			// Debug.Log("Audio source in PlayPause is " + audioSource);
			// if (audioSource.isPlaying)
				// audioSource.Pause();
			// else
				// audioSource.Play();
		// }
	}

	Visualizer GetActiveVisualizer(Dictionary<int, Visualizer> visualizers)
	{
		foreach (var visualizer in visualizers.Values)
		{
			if (visualizer.isActiveAndEnabled)
				return visualizer;
		}
		return null;
	}
	void PinchtoZoom(Visualizer visualizer)
	{
		if (Input.touchCount == 2)
		{
			// Store both touches.
			Touch touchZero = Input.GetTouch(0);
			Touch touchOne = Input.GetTouch(1);

			// Find the position in the previous frame of each touch.
			Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
			Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

			// Find the magnitude of the vector (the distance) between the touches in each frame.
			float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
			float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

			// Find the difference in the distances between each frame.
			float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;


			float pinchAmount = deltaMagnitudeDiff * ZoomFactor * Time.deltaTime;

			if (visualizer != null)
				visualizer.transform.localScale -= new Vector3(pinchAmount, pinchAmount, pinchAmount);
		}
	}

	void Rotate(Visualizer visualizer)
	{
		Touch touch;
		touch = Input.GetTouch(0);
		if (Input.touchCount == 1 && touch.phase == TouchPhase.Moved)
		{
			if (visualizer != null)
				visualizer.transform.Rotate(Vector3.forward * RotateFactor * Time.deltaTime * touch.deltaPosition.y, Space.Self);
		}
	}

}

