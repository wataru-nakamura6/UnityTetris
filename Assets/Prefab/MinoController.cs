using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MinoController : MonoBehaviour
{
  public float time;
  public float span;
  public GameObject text;
  private static int width = 10;
  private static int height = 20;
  //public Vector3 rotationPoint;
  private static Transform[,] grid = new Transform[width, height];
  bool GameOver;
  public int rensa;
  float PosX0; //タップし、指が画面に触れた瞬間の指のx座標
  float PosX1; //タップし、指が画面から離れた瞬間のx座標
  float PosXNow;
  float PosXPast;
  float PosY0;
  float PosY1;
  float PosYNow;
  float PosYPast;
  float PosDiff; //x,y座標の差のいき値。

    // Start is called before the first frame update
    void Start()
    {
      span = 1.0f;
      rensa = 0;
      PosDiff = 100.0f;
    }

    // Update is called once per frame
    void Update()
    {
      time += Time.deltaTime;
      TapMove();
    }
//ここまで宣言文

    void TapMove()
    {
      if (Input.GetMouseButtonDown(0))
      {
        PosX0 = Input.mousePosition.x;//最初
        PosY0 = Input.mousePosition.y;
        PosXPast = PosX0;//ミノが動くたび更新
        PosYPast = PosY0;
      }

      if (Input.GetMouseButtonUp(0))
      {
        PosX1 = Input.mousePosition.x;
        PosY1 = Input.mousePosition.y;

        if (Mathf.Abs(PosX1 - PosX0) < PosDiff && Mathf.Abs(PosY1 - PosY0) < PosDiff)//回転の判断基準
        {
          transform.Rotate(new Vector3(0,0,-90));
          if (Screen())
          {
            transform.Rotate(new Vector3(0,0,90));
          }
        }
      }

      if (Input.GetMouseButton(0))
      {
        PosXNow = Input.mousePosition.x;
        PosYNow = Input.mousePosition.y;
      }

      //横移動の判断基準
      if (PosXNow　-　PosXPast >= PosDiff)
      {
        PosXPast = PosXNow;
        transform.position += new Vector3(1,0,0);

        if (Screen())
        {
          transform.position += new Vector3(-1,0,0);
        }
      }

      if (PosXNow　-　PosXPast <= -PosDiff)
      {
        PosXPast = PosXNow;
        transform.position += new Vector3(-1,0,0);
        if (Screen())
        {
          transform.position += new Vector3(1,0,0);
        }
      }

      if (PosYNow　-　PosYPast <= -PosDiff || time >= span)
      {
        PosYPast = PosYNow;
        transform.position += new Vector3(0,-1,0);
        time = 0;
        if (Screen())
        {
          transform.position += new Vector3(0,1,0);
          this.enabled = false;
          AddToGrid();
          CheckLines();
          gameObject.tag = "Block";
          if(GameOver != true)
          {
            FindObjectOfType<Minofab>().CreateMino();
            FindObjectOfType<GameDirector>().isfirst = true;
          }
        }
      }
    }

    public void CheckLines()
    {
      for (int i = height - 1; i >= 0; i--)
      {
        if (HasLine(i))
        {
          DeleteLine(i);
          RowDown(i);
          rensa += 1;
        }
      }
      FindObjectOfType<GameDirector>().ScoreUp(rensa);
    }

    // 列がそろっているか確認
    bool HasLine(int i)
    {
      for (int j = 0; j < width; j++)
      {
        if (grid[j, i] == null)
        return false;
      }
      return true;
    }

     // ラインを消す
    void DeleteLine(int i)
    {
      for (int j = 0; j < width; j++)
      {
        Destroy(grid[j, i].gameObject);
        grid[j, i] = null;
      }
    }

     // 列を下げる
    public void RowDown(int i)
    {
       for (int y = i; y < height; y++)
       {
         for (int j = 0; j < width; j++)
         {
           if (grid[j, y] != null)
           {
             grid[j, y - 1] = grid[j, y];
             grid[j, y] = null;
             grid[j, y - 1].transform.position -= new Vector3(0, 1, 0);
           }
         }
       }
    }

    void AddToGrid()
    {
    foreach (Transform children in transform)
      {
        int posx = Mathf.RoundToInt(children.transform.position.x);
        int posy = Mathf.RoundToInt(children.transform.position.y);
        grid[posx, posy] = children;

        if(posy >= height -1)
        {
          GameOver = true;
          FindObjectOfType<GameDirector>().GameOver();
        }
      }
    }

    bool Screen()//はみ出し、グリッドに入ったらtrue返す
    {
        foreach (Transform children in transform)
        {
            int posx = Mathf.RoundToInt(children.transform.position.x);
            int posy = Mathf.RoundToInt(children.transform.position.y);

            // minoがステージよりはみ出さないように制御
            if (posx < 0 || posx >= 10 || posy < -0 )//|| posy >= 10)
            {
                return true;
            }

            // 今回の追加
  　　　　　　if (grid[posx, posy] != null)
  　　　　　　{
      　        return true;
  　　　　　　}
        }
        return false;
    }
}
