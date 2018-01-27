using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlay : MonoBehaviour {

    public float rate;
    public List<GameObject> points;
    public GameObject pointPrefab;
    public GameObject player;

    public int playerPosition;
    public double score = 0;

    // Use this for initialization
    void Start () {
        rate = 1f;
        GameObject obj;
        Vector3 pos;

        float[] yPos = { 0.77f, 3.8f, 6.0f, 9.1f, 10.2f };
        for(int i = 0;i < yPos.Length; i++){
            obj = Instantiate(pointPrefab) as GameObject;
            pos = new Vector3(Random.Range(-3.3f, 3.3f), yPos[i], 0);
            obj.GetComponent<Transform>().position = pos;
            obj.GetComponent<Point>().number = i;
            points.Add(obj);
        }
        player.transform.position = points[0].transform.position;
        playerPosition = 0;
        Time.timeScale = rate;
        Invoke("startSpeedAtOne", 10);
    }

    void startSpeedAtOne()
    {
        rate = 1.3f;
    }

    // Update is called once per frame
    void Update () {

        #region remove points out of screen, update player position
        if (!player.GetComponent<Player>().isMoving)
        {
            player.transform.position = points[playerPosition].transform.position;
        }
        Time.timeScale += 0.00001f;
        for (int i=0;i< points.Count;i++)
        {
            Vector3 newPos = points[i].GetComponent<Transform>().position;
            newPos.y-= 1 * Time.deltaTime;
            points[i].GetComponent<Transform>().position = newPos;
            if (points[i].GetComponent<Transform>().position.y < -6)
            {
                if(i == playerPosition)
                {
                    //todo: show game over here
                }
                //find max y point
                float maxY = -999;
                for(int j = 0; j < points.Count; j++)
                {
                    if (points[j].GetComponent<Transform>().position.y > maxY)
                    {
                        maxY = points[j].GetComponent<Transform>().position.y;
                    }
                }

                Destroy(points[i]);
                Vector3 pos = new Vector3(Random.Range(-3.3f, 3.3f), Random.Range(maxY + 2, maxY + 5), 0);
                points[i] = Instantiate(pointPrefab) as GameObject;
                points[i].GetComponent<Transform>().position = pos;
                points[i].GetComponent<Point>().number = i;
            }
        }
        #endregion
    }

    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 100, 20), "Score: " + score);
    }

}
