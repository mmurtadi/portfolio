using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour {

	public float FadeInTimeInSeconds;

	private Image fadePanel;
	private Color currentColour = Color.black;

	// Use this for initialization
	void Start () {
		fadePanel = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.timeSinceLevelLoad < FadeInTimeInSeconds){
		float alphaCHange = Time.deltaTime / FadeInTimeInSeconds;
			currentColour.a -= alphaCHange;
			fadePanel.color = currentColour;
		}
		else{
		gameObject.SetActive(false);
		}
	}
}
