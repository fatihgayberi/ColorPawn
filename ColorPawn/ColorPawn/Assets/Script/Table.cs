using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{
    public List<GameObject> tableList = new List<GameObject>();
    public static List<GameObject> pawnList = new List<GameObject>();

    void CreateTable() /* regionlari tek tek liste icine atar */
    {
        for (int i = 0; i < 100; i++)
            tableList.Add(GameObject.Find("region" + i));
    }

    void CreatePawn() /* pawnlari cogaltip tek tek liste icine atar */
    {
        pawnList.Add(GameObject.Find("pawnRed"));
        pawnList.Add(GameObject.Find("pawnYellow"));
        pawnList.Add(GameObject.Find("pawnGreen"));
        pawnList.Add(GameObject.Find("pawnBlue"));
        pawnList.Add(GameObject.Find("pawnOrange"));
        pawnList.Add(GameObject.Find("pawnPink"));
        
        for (int i = 1; i < 16; i++)
        {
            pawnList.Add(Instantiate(GameObject.Find("pawnRed")));
            pawnList.Add(Instantiate(GameObject.Find("pawnYellow")));
            pawnList.Add(Instantiate(GameObject.Find("pawnGreen")));
            pawnList.Add(Instantiate(GameObject.Find("pawnBlue")));
            pawnList.Add(Instantiate(GameObject.Find("pawnOrange")));
            pawnList.Add(Instantiate(GameObject.Find("pawnPink")));
        }
    }
    
    void PawnInRegion()/* 0-100 arasi sayilari (0 ve 100 dahil) listede tutup bu sayilari index olarak tableList e gonderip pawnlari random olarak yerlestirir*/
    {
        List<int> randomNumList = new List<int>();
        int count = 0;
        int randNum;
        bool control;

        while (count != tableList.Count - 4)
        {
            randNum = Random.Range(0, tableList.Count);
            control = randomNumList.Contains(randNum);

            if (control == false && randNum != 44 && randNum != 45 && randNum != 54 && randNum != 55)
            {
                randomNumList.Add(randNum);
                pawnList[count].transform.position = new Vector3(tableList[randomNumList[count]].transform.position.x, pawnList[count].transform.position.y, tableList[randomNumList[count]].transform.position.z);
                count++;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        CreatePawn();
        CreateTable();
        PawnInRegion();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
