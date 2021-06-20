using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minofab : MonoBehaviour
{
  public GameObject[] Minofabs;
  public GameObject Hold;
  public GameObject Next;
  GameObject Mino;

    // Start is called before the first frame update
  void Start()
  {
    StartMino();
    //CreateMino();
  }


  // Update is called once per frame
  void Update()
  {
  }

  public void StartMino()
  {
    Mino = Instantiate(Minofabs[Random.Range(0, 5)]);
    Mino.transform.position = new Vector3(4, 17, 0);
    Mino.tag = "Mino";

    Next = Instantiate(Minofabs[Random.Range(0, 5)]);//Next生成,maxの数字は含まない
    Next.transform.position = new Vector3(8, 21.5f, 0);
    Next.GetComponent<MinoController>().enabled = false;
  }

  public void CreateMino()
  {
    Mino = Next;//Next代入
    Mino.tag = "Mino";
    Mino.transform.position = new Vector3(4, 17, 0);
    Mino.GetComponent<MinoController>().enabled = true;

    Next = Instantiate(Minofabs[Random.Range(0, 5)]);
    Next.transform.position = new Vector3(8, 21.5f, 0);
    Next.GetComponent<MinoController>().enabled = false;
  }
}
