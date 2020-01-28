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

        for (float y = backGround.transform.transform.position.y - scaleOfObject.y; y < (0.95f * 17)/*scaleOfObject.y*/; y += 0.95f)
        {

            posX = 0;
            List<TileClick> tileRow = new List<TileClick>();

            for (float x = backGround.transform.transform.position.x - scaleOfObject.x; x < (0.95f * 14) ; x += sizeOfMapBlock)
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
                if (x == 10 && y == 9)
                {
                    Debug.Log("x: " + maptiles[y][x].getCoordinatesInMatrix().x.ToString() + " y: " + maptiles[y][x].getCoordinatesInMatrix().y.ToString());
                    maptiles[y][x].setTypeOfunit(testVez, player:hracTest,false);

                }
                if (x == 10 && y == 10)
                {
                    Debug.Log("x: " + maptiles[y][x].getCoordinatesInMatrix().x.ToString() + " y: " + maptiles[y][x].getCoordinatesInMatrix().y.ToString());
                    maptiles[y][x].setTypeOfunit(testVez, player: hracTest,true);
                }
                if (x == 0 && y == 0) {
                    Debug.Log("x: " + maptiles[y][x].getCoordinatesInMatrix().x.ToString() + " y: " + maptiles[y][x].getCoordinatesInMatrix().y.ToString());
                    maptiles[y][x].setTypeOfunit(testVez, player: hracTest,false);
                }
            }
        }

        Debug.Log(maptiles[0][0].GetComponent<TileClick>().getTypeOfUnitCurentlyHavin().name);
       
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


    void Update()
    {
        
        
    }
   
}
