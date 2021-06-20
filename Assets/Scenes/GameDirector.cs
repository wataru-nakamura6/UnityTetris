using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameDirector : MonoBehaviour
{
  public Text ScoreText;
  public GameObject GO;
  public GameObject hold;
  public GameObject mino;
  int score;
  //int rensa;
  public bool isfirst;
    // Start is called before the first frame update
    void Start()
    {
      score = 0;
      GO.SetActive(false);
      isfirst = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ScoreUp(int rensa)
    {
      if(rensa == 1)
      {
        score += 100;
        ScoreText.text = "SCORE:" + score;
        Debug.Log("1連鎖");
      }
      else if(rensa == 2)
      {
        score += 200;
        ScoreText.text = "SCORE:" + score;
        Debug.Log("2連鎖");
      }
      else if(rensa == 3)
      {
        score += 400;
        ScoreText.text = "SCORE:" + score;
        Debug.Log("3連鎖");
      }
      else if(rensa >= 4)
      {
        score += 600;
        ScoreText.text = "SCORE:" + score;
        Debug.Log("4連鎖");
      }
      FindObjectOfType<MinoController>().rensa = 0;
    }

    public void GameOver()
    {
      GO.SetActive(true);
    }

    public void Restart()
    {
      SceneManager.LoadScene("GameScene");
    }

    public void HoldKeep()
    {
      if(hold != null && isfirst)
      {
        //hold.tag = "Mino";
        mino = hold;//holdからミノに
        mino.transform.position = new Vector3(4, 17, 0);
        mino.GetComponent<MinoController>().enabled = true;

        hold = GameObject.FindWithTag("Mino");//画面上のミノをholdに
        hold.tag = "Hold";
        hold.transform.position = new Vector3(1, 21.5f, 0);
        hold.transform.rotation = Quaternion.identity;
        hold.GetComponent<MinoController>().enabled = false;
        isfirst = false;
      }

      if(hold == null && isfirst)
      {
        hold = GameObject.FindWithTag("Mino");
        hold.tag = "Hold";
        hold.transform.position = new Vector3(1, 21.5f, 0);
        hold.transform.rotation = Quaternion.identity;
        hold.GetComponent<MinoController>().enabled = false;
        isfirst = false;

        FindObjectOfType<Minofab>().CreateMino();
      }
      Debug.Log(isfirst);
    }
}
