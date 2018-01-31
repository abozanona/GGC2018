using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {
    public GameObject direction;
    float angle;
    public bool isMoving = false;
    public int remainingBounces = 3;
    public Rigidbody2D rb;

    private GamePlay gamePlay;

    public GameObject gameoverCanvas;
    public GameObject gameoverText;

    public GameObject destroyParticle;

    public GameObject rocket1, rocket2;

    public bool hasColided = false;

    public bool hasLost = false;

    public GameObject light;
    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        gamePlay = Camera.main.gameObject.GetComponent<GamePlay>();
        fireLight();
        fireLight();
        fireLight();
        fireLight();
    }
	
    void fireLight()
    {
        bool isRight = (Random.value > 0.5f);
        if (isRight)
        {
            GameObject light = Instantiate(this.light, new Vector3(4,Random.Range(-2,5),0), Quaternion.identity);
            light.transform.LookAt(new Vector3(-4, 7, 0));
            light.GetComponent<Rigidbody2D>().AddForce(light.transform.forward * 7000 * Time.deltaTime);
            Destroy(light, 5);
        }
        else
        {
            GameObject light = Instantiate(this.light, new Vector3(-4, Random.Range(-2, 5), 0), Quaternion.identity);
            light.transform.LookAt(new Vector3(4, 7, 0));
            light.GetComponent<Rigidbody2D>().AddForce(light.transform.forward * 7000 * Time.deltaTime);
            Destroy(light, 5);
        }
    }

    public float getMinimumDistanceIsAvailable()
    {
        return Vector3.Distance(transform.position, gamePlay.points[gamePlay.playerPosition].transform.position);
    }
    // Update is called once per frame
    bool isUp = false;
    void Update () {
        if (isMoving)
        {
            rocket1.SetActive(true);
            rocket2.SetActive(true);
        }
        else
        {
            rocket1.SetActive(false);
            rocket2.SetActive(false);
        }
        //float angle = Mathf.PingPong(Time.time, 140) - 70;
        float value = 1.3f;
        if (!isMoving)
        {
            //angle = Mathf.Sin(Time.time) * 70;
            if (isUp)
            {
                angle += value;
                if (angle > 70)
                {
                    isUp = false;
                }
            }
            else
            {
                angle -= value;
                if (angle < -70)
                {
                    isUp = true;
                }
            }
            direction.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        if((Input.touchCount > 0 || Input.GetMouseButtonDown(0)) && hasLost)
        {
            hasLost = false;
            Application.LoadLevel(Application.loadedLevel);
        }
        else if ((Input.touchCount > 0 || Input.GetMouseButtonDown(0)) && (!isMoving))
        {
            isMoving = true;
            rb.AddForce(direction.transform.up * 8000 * Time.deltaTime);
        }

        if (hasColided)
        {
            Vector2 curPos = new Vector2(transform.position.x, transform.position.y);
            Vector2 newPos = gamePlay.points[gamePlay.playerPosition].transform.position;

            transform.position = Vector2.MoveTowards(curPos, newPos, 5f * Time.deltaTime);

            if (getMinimumDistanceIsAvailable() < 0.3f)
            {
                hasColided = false;
                isMoving = false;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Point" && !hasColided)
        {
            if(coll.gameObject.GetComponent<Point>().number == gamePlay.playerPosition)
            {
                return;
            }
            AudioSource audio = GetComponent<AudioSource>();
            audio.Play();
            fireLight();
            GameObject destroyParticle = Instantiate(this.destroyParticle, gameObject.transform);
            Destroy(destroyParticle, 0.5f);
            gamePlay.score++;
            hasColided = true;
            int newPointNumber = coll.gameObject.GetComponent<Point>().number;
            gamePlay.playerPosition = newPointNumber;
            Destroy(coll.gameObject.GetComponent<CircleCollider2D>());
        }
        else if(coll.gameObject.tag == "Death")
        {
            gameoverCanvas.SetActive(true);
            gameoverText.GetComponent<Text>().text = "Game over\nScore: " + gamePlay.score;
            Time.timeScale = 0;
            hasLost = true;
            //todo: show game over here
        }
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Out")
        {
            gameoverCanvas.SetActive(true);
            gameoverText.GetComponent<Text>().text = "Game over\nScore: " + gamePlay.score;
            Time.timeScale = 0;
            hasLost = true;
            //todo: show game over here
        }
    }
}
