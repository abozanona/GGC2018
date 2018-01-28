using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ArrowsAnimator : MonoBehaviour {
    public Texture2D[] arrowssprites;
    public GameObject viewer;
    public float duration;
    private float time;
    private int index = 0;
	// Use this for initialization
	void Start () {
        time = duration;
	}
	
	// Update is called once per frame
	void Update () {
        if (duration>0) {
            duration -= Time.deltaTime;
        }
        else
        {
            viewer.GetComponent<RawImage>().texture = arrowssprites[index % 3];
            index += 1;

            duration = time;
        }

        if (index == 17) index = 0;

    }
}
