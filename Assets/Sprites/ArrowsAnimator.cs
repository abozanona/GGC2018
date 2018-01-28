using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ArrowsAnimator : MonoBehaviour {
    public Texture2D[] arrowssprites;
    public Sprite[] arrowsspritesx;
    public GameObject viewer;
    public float duration;
    private float time;
    private int index = 0;
	// Use this for initialization
	void Start () {
        time = duration;
        arrowsspritesx = new Sprite[4];
        for (int i=0;i< arrowssprites.Length;i++)
        {
            arrowsspritesx[i] = Sprite.Create(arrowssprites[i], new Rect(0, 0, arrowssprites[i].width, arrowssprites[i].height), new Vector2(0.0f, 0.0f));
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (duration>0) {
            duration -= Time.deltaTime;
        }
        else
        {
            viewer.GetComponent<SpriteRenderer>().sprite = arrowsspritesx[index % 3];
            index += 1;

            duration = time;
        }

        if (index == 17) index = 0;

    }
}
