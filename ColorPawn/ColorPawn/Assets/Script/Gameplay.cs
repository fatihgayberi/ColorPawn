using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


/*  Player;
 *  
 *  TouchControl fonksiyonu:
 *      Ekrana tiklayacak tiklanan sey gameobject selectedObject degiskeni icinde tutulacak ve ne oldugunu anlayacak.
 *            
 *      selectedObject pawn ise:
 *          ==> Secilen pawn gameObject selectedPawn degiskeni icinde tutulur.
 *  
 *      selectedObject region ise:
 *          ==> Secilen region gameobject selectedRegion degiskeni icinde tutulur.
 *          
 *      selectedObject buton ise:
 *          ==> selectedPawn == null ve selectedRegion == null olup olmadigini control eder.
 *          en az biri bile null ise:
 *              ==> Hata mesaji verecek
 *          ikisi de null degil ise:
 *              ==> selectedPawn ve selectedRegion return edilir.
 *
 *  Control fonksiyonu:
 *      return edilen selectedPawn ve selectedRegion u alır.
 *      
 *      selectedPawn ve selectedRegion iki kordinat arasınaki mesafeyi hesaplar.
 *      sartlar uygun ise:
 *          ==> bool areaControl == true yapilir.
 *      sartlar uygun degil ise:
 *          ==> bool areaControl == false yapilir.
 *          ==> Hata mesaji verecek.
 *          
 *      areaControl true ise:
 *          ==> regionun bos olup olmadigi kontrol edilir.
 *              bos ise:
 *              ==> selectedPawn selectedRegion un konumuna tasinir.
 *              bos degil ise:
 *              ==> Hata mesaji verecek. 
 *      
 *      Secilen 
 * 
 * */

public class Gameplay : MonoBehaviour
{
    public Button controlButton, leaveButton;
    RaycastHit selectedRegion, selectedPawn;
    List<GameObject> livePawn = new List<GameObject>();
    GameObject selectedPawnSymbol, selectedRegionSymbol;
    float redWinCounter = 0, yellowdWinCounter = 0, greendWinCounter = 0, bluedWinCounter = 0, orangedWinCounter = 0, pinkdWinCounter = 0;
    bool flagSelected = false; /* Pawn secilip secilmedigini tutar */

    void LivePawn() /* oyundaki kazanilmamis piyonlari tutan listeyi hazirlar */
    {
        for (int i = 0; i < Table.pawnList.Count; i++)
            livePawn.Add(Table.pawnList[i]);
    }

    bool RegionEmptyControl(RaycastHit region) /* arguman olarak aldigi regionun bos olup olmadigini kontrol eder. */
    {
        for (int i = 0; i < livePawn.Count; i++)
        {
            if (livePawn[i].transform.position.x == region.transform.position.x
                && livePawn[i].transform.position.y == region.transform.position.y + 0.075f
                && livePawn[i].transform.position.z == region.transform.position.z)
            {
                return false;
            }
        }
        return true;
    }

    /*pawnRed
     *pawnYellow 
     *pawnGreen
     *pawnBlue
     *pawnOrange 
     *pawnPink 
     */
    bool PawnWin(RaycastHit region, RaycastHit pawn)
    {
        GameObject  winPawn;

        for (int i = 0; i < livePawn.Count; i++)
        {
            if (livePawn[i].transform.position.x == (region.transform.position.x + pawn.transform.position.x) / 2f
                && livePawn[i].transform.position.z == (region.transform.position.z + pawn.transform.position.z) / 2f)
            {
                winPawn = livePawn[i];
                if (winPawn.name.Contains("pawnRed"))
                {
                    winPawn.transform.position = new Vector3(GameObject.Find("redWin").transform.position.x, 0.075f + (redWinCounter * 0.1f), GameObject.Find("redWin").transform.position.z);
                    livePawn.Remove(winPawn);
                    redWinCounter++;
                    return true;
                }
                if (winPawn.name.Contains("pawnYellow"))
                {
                    winPawn.transform.position = new Vector3(GameObject.Find("yellowWin").transform.position.x, 0.075f + (yellowdWinCounter * 0.1f), GameObject.Find("yellowWin").transform.position.z);
                    livePawn.Remove(winPawn);
                    yellowdWinCounter++;
                    return true;
                }
                if (winPawn.name.Contains("pawnGreen"))
                {
                    winPawn.transform.position = new Vector3(GameObject.Find("greenWin").transform.position.x, 0.075f + (greendWinCounter * 0.1f), GameObject.Find("greenWin").transform.position.z);
                    livePawn.Remove(winPawn);
                    greendWinCounter++;
                    return true;
                }
                if (winPawn.name.Contains("pawnBlue"))
                {
                    winPawn.transform.position = new Vector3(GameObject.Find("blueWin").transform.position.x, 0.075f + (bluedWinCounter * 0.1f), GameObject.Find("blueWin").transform.position.z);
                    livePawn.Remove(winPawn);
                    bluedWinCounter++;
                    return true;
                }
                if (winPawn.name.Contains("pawnOrange"))
                {
                    winPawn.transform.position = new Vector3(GameObject.Find("orangeWin").transform.position.x, 0.075f + (orangedWinCounter * 0.1f), GameObject.Find("orangeWin").transform.position.z);
                    livePawn.Remove(winPawn);
                    orangedWinCounter++;
                    return true;
                }
                if (winPawn.name.Contains("pawnPink"))
                {
                    winPawn.transform.position = new Vector3(GameObject.Find("pinkWin").transform.position.x, 0.075f + (pinkdWinCounter * 0.1f), GameObject.Find("pinkWin").transform.position.z);
                    livePawn.Remove(winPawn);
                    pinkdWinCounter++;
                    return true;
                }
            }
        }
        return false;
    }

    void TouchAndControl()
    {
        controlButton = GameObject.Find("controlButton").GetComponent<Button>();
        leaveButton = GameObject.Find("leaveButton").GetComponent<Button>();

        Ray rayArea = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitArea;

        if (Input.touchCount == 1 && Physics.Raycast(rayArea, out hitArea)) /* Dokunulan objenin ne oldugunu tanimlar */
        {
            if (hitArea.transform.name.Contains("pawn"))
            {
                selectedPawn = hitArea;
                selectedPawnSymbol.transform.position = new Vector3(selectedPawn.transform.position.x, 0.07f, selectedPawn.transform.position.z + 0.1f); /* Halkayi isaretler */
                flagSelected = true; /* Pawn secilince true ya donusur */
                //Debug.Log("selectedPawn: " + selectedPawn.transform.name);
            }
            if (hitArea.transform.name.Contains("region") && RegionEmptyControl(hitArea) && flagSelected) /* secilen objenin region olup olmadigini anlamaya calisir ve ustunde pawn var mi kontrol eder */
            {
                selectedRegion = hitArea;
                selectedRegionSymbol.transform.position = new Vector3(selectedRegion.transform.position.x, 0.07f, selectedRegion.transform.position.z + 0.1f); /* X'i isaretler */
                //Debug.Log("selectedRegion: " + selectedRegion.transform.name);
            }
        }

        leaveButton.onClick.AddListener(PushLeave); 
        void PushLeave() /* leaveButton a tiklandiginda tutlan degerleri sifirlar. */
        {
            selectedRegion = new RaycastHit();
            selectedPawn = new RaycastHit();
            selectedPawnSymbol.transform.position = new Vector3(selectedPawnSymbol.transform.position.x, -0.68f, selectedPawnSymbol.transform.position.z + 0.1f);
            selectedRegionSymbol.transform.position = new Vector3(selectedRegionSymbol.transform.position.x, -0.18f, selectedRegionSymbol.transform.position.z + 0.1f);
            flagSelected = false; /* Secilen pawn unutuldugundan false ye donusur */
        }

        controlButton.onClick.AddListener(PushControl);
        void PushControl() /* controlButton a tiklandiginda gerekli sartlarin saglanip saglanmadiginin kontrolu saglanir. */
        {
            if (selectedPawn.collider != null && selectedRegion.collider != null)
            {
                if (((Mathf.Abs(selectedPawn.transform.position.x - selectedRegion.transform.position.x) == 2f && selectedPawn.transform.position.z == selectedRegion.transform.position.z)
                    || (selectedPawn.transform.position.x == selectedRegion.transform.position.x && Mathf.Abs(selectedPawn.transform.position.z - selectedRegion.transform.position.z) == 2f)) 
                    && PawnWin(selectedRegion, selectedPawn))
                {
                    selectedPawn.transform.position = new Vector3(selectedRegion.transform.position.x, selectedPawn.transform.position.y, selectedRegion.transform.position.z);
                    selectedRegion = new RaycastHit();
                    selectedPawn = new RaycastHit();
                    selectedPawnSymbol.transform.position = new Vector3(selectedPawnSymbol.transform.position.x, -0.68f, selectedPawnSymbol.transform.position.z + 0.1f); /* Halkayi gizler */
                    selectedRegionSymbol.transform.position = new Vector3(selectedRegionSymbol.transform.position.x, -0.18f, selectedRegionSymbol.transform.position.z + 0.1f); /* X'i gizler */
                    flagSelected = false; /* Yeni bir pawn gerektiginden false ye donusur */
                }
                else
                {
                    selectedPawnSymbol.transform.position = new Vector3(selectedPawnSymbol.transform.position.x, -0.68f, selectedPawnSymbol.transform.position.z + 0.1f); /* Halkayi gizler */
                    selectedRegionSymbol.transform.position = new Vector3(selectedRegionSymbol.transform.position.x, -0.18f, selectedRegionSymbol.transform.position.z + 0.1f); /* X'i gizler */
                    flagSelected = false; /* Islemi hatali yaptıgından false ye donusur */
                }
            }
            else
                return;
        }
    }
   
    void A()
    {
        Debug.Log("BURDAYIM" );
    }

    // Start is called before the first frame update
    void Start()
    {
        selectedPawnSymbol = GameObject.Find("Circle"); /* Halkayi bulur */
        selectedRegionSymbol = GameObject.Find("XSymbol"); /* X' i bulur */
        LivePawn();
        //A();
    }

    // Update is called once per frame
    void Update()
    {
        TouchAndControl();
    }
}
