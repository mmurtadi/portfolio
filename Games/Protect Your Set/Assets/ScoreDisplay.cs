using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreDisplay : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Text myTest = GetComponent<Text>();
		myTest.text = ScoreKeeper.score.ToString();
		ScoreKeeper.Reset();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
