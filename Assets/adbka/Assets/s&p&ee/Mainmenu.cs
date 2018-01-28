using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class Mainmenu : MonoBehaviour {
    public Animator anim;
    public void playGame()
    {   if(anim.GetInteger("Selected")==0)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void back()
    {

        SceneManager.LoadScene("menu");

    }
    public void op()
    {


        SceneManager.LoadScene("s");
    }
    public void QuitGame()
    {
        if (anim.GetInteger("Selected") == 3)
        {
            Debug.Log("quit the game!!");
            Application.Quit();
        }
    }
   public void setto(int i)
    {
        bool dir = (i + 3-1 == anim.GetInteger("Selected") + 3)|| !(i==3 && anim.GetInteger("Selected")==0);
        anim.SetBool("ToRight", dir);
        anim.SetInteger("Selected", i);
    }
}
