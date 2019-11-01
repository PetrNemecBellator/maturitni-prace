using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class mapCreation : MonoBehaviour
{
    // Start is called before the first frame update
        public GameObject gameTile;
        public GameObject backGround;
        public GameObject hrac1Test; 

        public GameObject testVez;
        public GameObject testStit;
        public GameObject testSip;

        public static GameObject testStaticky;
        private static List<List<TileClick>> maptiles = new List<List<TileClick>>();

    private const float sizeOfMapBlock = 0.8F;

    private void Awake()
    {
        Vector3 scaleOfObject = backGround.GetComponent<Renderer>().bounds.size; //backGround.GetComponent<Transform>().lossyScale;
        Debug.Log($"x= " + scaleOfObject.x);
        Debug.Log($"Y= " + scaleOfObject.y);
        Debug.Log($"z= " + scaleOfObject.z);
        bool changeDirectionOftile = true;


        int posX = 0;
        int posY = 0;

        for (float y = backGround.transform.transform.position.y - scaleOfObject.y; y < scaleOfObject.y; y += 0.95f)
        {
         
            posX = 0;
            List<TileClick> tileRow = new List<TileClick>();

            for (float x = backGround.transform.transform.position.x - scaleOfObject.x; x < scaleOfObject.x * 2 * sizeOfMapBlock; x += sizeOfMapBlock)
            {
               
                if (changeDirectionOftile)
                {

                    var obj = Instantiate(gameTile, new Vector3(x * sizeOfMapBlock, y, 0), Quaternion.Euler(0, 0, 180));
                    obj.transform.parent = backGround.transform;
                  
                    TileClick myTile =(TileClick) obj.GetComponent<TileClick>();//accesing my class for the tile object
                    myTile.setTileCoordinates(new Vector2(posX, posY));
                    tileRow.Add(myTile);
                    changeDirectionOftile = !changeDirectionOftile;
                    
                }
                else
                {
                    var obj = Instantiate(gameTile, new Vector3(x * sizeOfMapBlock, y, 0), Quaternion.Euler(0, 0, 0));
                    obj.transform.parent = backGround.transform;

                    TileClick myTile = (TileClick)obj.GetComponent<TileClick>();//accesing my class for the tile object
                    myTile.setTileCoordinates(new Vector2(posX, posY));
                    tileRow.Add(myTile);
                    changeDirectionOftile = !changeDirectionOftile;
                    
                }
                posX++;
            }
            maptiles.Add(tileRow);

            posY++;
        }
      

    }
   

    void Start() {
        Debug.Log("MAP CRATION STARTED ...");
        for (int y = 0; y < maptiles.Count; y++)
        {
            for (int x = 0; x < maptiles[y].Count; x++)
            {
                if (x == 10 && y == 9)
                {
                    Debug.Log("x: " + maptiles[y][x].getCoordinates().x.ToString() + " y: " + maptiles[y][x].getCoordinates().y.ToString());
                    maptiles[y][x].setTypeOfunit(testVez);

                }
                if (x == 10 && y == 10)
                {
                    Debug.Log("x: " + maptiles[y][x].getCoordinates().x.ToString() + " y: " + maptiles[y][x].getCoordinates().y.ToString());
                    maptiles[y][x].setTypeOfunit(testSip);

                }
                if (x == 9 && y == 9) {
                    Debug.Log("x: " + maptiles[y][x].getCoordinates().x.ToString() + " y: " + maptiles[y][x].getCoordinates().y.ToString());
                    maptiles[y][x].setTypeOfunit(testStit);

                }

            }
        }


        for (int y = 0; y < maptiles.Count; y++)
        {
            for (int x = 0; x < maptiles[y].Count; x++)
            {
              //  Debug.Log("name of maptile: " + maptiles[y][x].transform.GetChild(1).name);
            }
        }

        //** tests
        Debug.Log("auto fight TEST");
        Debug.Log("");

        Debug.Log("maptiles coor attacking unit:" + maptiles[9][10].getCoordinates().x + " y" + maptiles[9][10].getCoordinates().y);
        Debug.Log("maptiles coor passive unit:" + maptiles[9][9].getCoordinates().x + " y" + maptiles[9][9].getCoordinates().y);

        Debug.Log("chack jednotky: " + maptiles[9][10].getChildObject());
        Debug.Log("chack jednotky: " + maptiles[9][9].getChildObject());

        Hrac testHrace = hrac1Test.GetComponent<Hrac>();
        testHrace.setNewGroupOfPlayerUnits(new List<TileClick>() {maptiles[9][9], maptiles[10][10], maptiles[9][10]} );
        testHrace.selectGroupOfUnits(0);
        // TileClick.moveUnitWithFightMode(  maptiles[9][10].gameObject, maptiles[9][9].gameObject);
    }
    // Update is called once per frame



    public static void coordinatesActualization()
    {

    }

    public static List<Vector2> helpFieldForTileActualization = new List<Vector2>();
    public static TileClick moveUnits(Vector2 tileStart, Vector2 destinationTile)
    {
        helpFieldForTileActualization.Add(tileStart);

        TileClick movedTile = new TileClick();

        //tot
        Debug.Log("move group of units TEST");
        Debug.Log("tilestart Coordinates" + tileStart.ToString());
        Debug.Log("tileDESTINATION Coordinates" + destinationTile.ToString());

        maptiles[(int)tileStart.y][(int)tileStart.x].changeToUnMark();
        maptiles[(int)destinationTile.y][(int)destinationTile.x].changeToUnMark();
        
        Debug.Log($"\\////////////startovni pozice {tileStart} konecna pozice {destinationTile}");
        TileClick.moveUnitWithFightMode(maptiles[(int)tileStart.y][(int)tileStart.x].gameObject, maptiles[(int)destinationTile.y] [(int)destinationTile.x].gameObject);
      

      
        Debug.Log($"//////// destination tile unit coor {maptiles[(int)destinationTile.y][(int)destinationTile.x].getCoordinatesOfTheChild()} " +
            $"tile coor y{destinationTile.y} x {destinationTile.x} ");
        
        
         movedTile = maptiles[(int)destinationTile.y][(int)destinationTile.x];
         Debug.Log($"1111111movedTile souradnice{movedTile.getCoordinatesOfTheChild()}");
        
        return movedTile;
    } 


    void Update()
    {
        
        
    }
   
}
