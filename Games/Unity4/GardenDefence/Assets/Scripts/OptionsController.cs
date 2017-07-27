using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OptionsController : MonoBehaviour {

	public Slider volumeSlider, diffSlider;
	public LevelManager levelManager;

	private MusicManager musicManager;

	// Use this for initialization
	void Start () {
		musicManager = GameObject.FindObjectOfType<MusicManager>();
		volumeSlider.value = PlayerPrefsManager.GetMasterVolume();
		diffSlider.value = PlayerPrefsManager.GetDifficutly();
	
	}
	
	// Update is called once per frame
	void Update () {
		musicManager.SetVolume(volumeSlider.value);
	
	}

	public void SaveAndExit(){
		PlayerPrefsManager.SetDifficulty(diffSlider.value);
		PlayerPrefsManager.SetMasterVolume(volumeSlider.value);
		Debug.LogError ("before levelManager");
		levelManager.LoadLevel("01a Start");
		Debug.LogError ("after function");
	}

	public void SetDefaults () {
		volumeSlider.value = 0.8f;
		diffSlider.value = 2f;
	}
}
