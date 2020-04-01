using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

[System.Serializable]
public class mapCreation : MonoBehaviour
{
    // Start is called before the first frame update
        public GameObject gameTile;
        public GameObject backGround;
        public GameObject hrac1Test;
        public GameObject hrac2Test; 

        public GameObject testVez;
        public GameObject testStit;
        public GameObject testSip;

        public static GameObject testStaticky;
        private static List<List<TileClick>> maptiles = new List<List<TileClick>>();

    private const float sizeOfMapBlock = 0.8F;
    private const int MAP_HEIGHT = 17;
    private const int MAP_WIDTH = 14;
    private void Awake()
    {
       Vector3 scaleOfObject = backGround.GetComponent<Renderer>().bounds.size; //backGround.GetComponent<Transform>().lossyScale;
        Debug.Log($"x= " + scaleOfObject.x);
        Debug.Log($"Y= " + scaleOfObject.y);
        Debug.Log($"z= " + scaleOfObject.z);

        bool changeDirectionOftile = true;
        bool changeB;
        bool changeC;

        int b; // diagonala /

        int c; // diagonala opak /

        int posX = 0;
        int posY = 0;

        for (float y = backGround.transform.transform.position.y - scaleOfObject.y; y < (0.95f * MAP_HEIGHT); y += 0.95f)
        {

            b = posY - ((int)(posY / 2));    // set the starting value of b in a line (from 0 to map height/2; every second line, this starting value is increased by 1)
            c = (int)((0.95f * MAP_HEIGHT) - posY) / 2;// set the starting value of c in a line (from map height/2 to 0; every second line, this starting value is decreased by 1)

            posX = 0;
            List<TileClick> tileRow = new List<TileClick>();

            for (float x = backGround.transform.transform.position.x - scaleOfObject.x; x < (0.95f * MAP_WIDTH) ; x += sizeOfMapBlock)
            {
               
                if (changeDirectionOftile)
                {
                    
                    var obj = Instantiate(gameTile, new Vector3(x * sizeOfMapBlock, -y, 0), Quaternion.Euler(0, 0, 180));
                    obj.transform.parent = backGround.transform;
                  
                    TileClick myTile =(TileClick) obj.GetComponent<TileClick>();//accesing my class for the tile object
                    myTile.setTileCoordinates(new Vector2(posX, posY));
                 
                    tileRow.Add(myTile);
                    changeDirectionOftile = !changeDirectionOftile;

                    changeB = true;
                    myTile.setCoordinetsForDistanceCalculation(new Vector3(posY, b, c));  // line, diagonal b, diagonal c
                    if (changeB)
                    {
                        b++;  // after two tiles (in one line) the value of b is increased by 1 (-after second same direction of a tile, increse the value of b)
                        changeB = false;
                    }
                                        
                }
                else
                {
                    
                    var obj = Instantiate(gameTile, new Vector3(x * sizeOfMapBlock, -y, 0), Quaternion.Euler(0, 0, 0));
                    obj.transform.parent = backGround.transform;

                    TileClick myTile = (TileClick)obj.GetComponent<TileClick>();//accesing my class for the tile object
                    myTile.setTileCoordinates(new Vector2(posX, posY));
                  
                    tileRow.Add(myTile);
                    changeDirectionOftile = !changeDirectionOftile;

                    changeC = true;
                    myTile.setCoordinetsForDistanceCalculation(new Vector3(posY, b, c));
                    if (changeC)
                    {
                        c++;
                        changeC = false;
                    }
                }
                posX++;

               
            }
            maptiles.Add(tileRow);
            changeDirectionOftile = !changeDirectionOftile;


            posY++;
        }
      

    }
   

    private void Start() {
        Debug.Log("MAP CRATION STARTED ...");
        Hrac hracTest = hrac1Test.transform.GetComponent<Hrac>();
        for (int y = 0; y < maptiles.Count; y++)
        {
            for (int x = 0; x < maptiles[y].Count; x++)
            {
                if (x == 11 && y == 9)
                {
                    Debug.Log("x: " + maptiles[y][x].getCoordinatesInMatrix().x.ToString() + " y: " + maptiles[y][x].getCoordinatesInMatrix().y.ToString());
                    maptiles[y][x].setTypeOfunit(testVez, player:hracTest,false);

                }
                if (x == 12 && y == 9)
                {
                    Debug.Log("x: " + maptiles[y][x].getCoordinatesInMatrix().x.ToString() + " y: " + maptiles[y][x].getCoordinatesInMatrix().y.ToString());
                    maptiles[y][x].setTypeOfunit(testSip, player: hracTest,true);
                }
                if (x == 8 && y == 10) {
                    Debug.Log("x: " + maptiles[y][x].getCoordinatesInMatrix().x.ToString() + " y: " + maptiles[y][x].getCoordinatesInMatrix().y.ToString());
                    maptiles[y][x].setTypeOfunit(testStit, player: hracTest,false);
                }
            }
        }

       // Debug.Log(maptiles[0][0].GetComponent<TileClick>().getTypeOfUnitCurentlyHavin().name);
       
       // changeLocationOfUnit(maptiles[0][0].gameObject, new Vector2(1, 1));
       //akce z tileclick vola hrace hrac vola nedodelanou funkci  v mapCreation
    }
    // Update is called once per frame

    
   

   
    public static TileClick  moveUnits(Vector2 tileStartMatrixCor, Vector2 destinationTileMatrixCor)
    {

        //functction change coordinates in units transfrom.position
        //function returns Vector2 tu update tyle in player it is used to(mark un mark metod)

        TileClick originalTile = mapCreation.maptiles[(int)tileStartMatrixCor.y][(int)tileStartMatrixCor.x];
        TileClick destinationTile = mapCreation.maptiles[(int)destinationTileMatrixCor.y][(int)destinationTileMatrixCor.x];

        //unmatk previosly selected unit
        originalTile.changeToUnMark();


        GameObject typeOfunitCurentlyHaving = originalTile.getTypeOfUnitCurentlyHavin();

        originalTile.setTypeOfUnitCyrentlyHaving(null);

        if (typeOfunitCurentlyHaving == null)
        {
            //cancle action 
            //deselection of curent tile
            return originalTile;
        }
        else{
            //unit movement
            destinationTile.moveAndSetTypeOfunitCurenlyHaving(typeOfunitCurentlyHaving);
            originalTile.setTypeOfUnitCyrentlyHaving(null);
        }
        return destinationTile;
    } 
    public static List<List<TileClick>> getMapTiles()
    {
        return maptiles;
    }
   
}
