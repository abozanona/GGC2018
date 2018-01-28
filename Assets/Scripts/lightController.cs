using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightController : MonoBehaviour {

    [SerializeField] float timeToChange;
    float timer;
    [SerializeField] float lerp;

    public Color[] colors;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if (timer >= timeToChange)
        {
            int random = Random.Range(0, colors.Length);
            GetComponent<Light>().color = Color.Lerp(GetComponent<Light>().color , colors[random] , lerp);
            timer = 0;
        }
	}
}
