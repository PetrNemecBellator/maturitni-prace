using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapCreation : MonoBehaviour
{
    // Start is called before the first frame update
        public GameObject gameTile;
        public GameObject backGround;
        public GameObject testUnit;

        private List<List<TileClick>> maptiles = new List<List<TileClick>>();

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
            posY++;
            posX = 0;
            List<TileClick> tileRow = new List<TileClick>();

            for (float x = backGround.transform.transform.position.x - scaleOfObject.x; x < scaleOfObject.x * 2 * sizeOfMapBlock; x += sizeOfMapBlock)
            {
                posX++;
                if (changeDirectionOftile)
                {

                    var obj = Instantiate(gameTile, new Vector3(x * sizeOfMapBlock, y, 0), Quaternion.Euler(0, 0, 180));
                    obj.transform.parent = backGround.transform;
                  
                    TileClick myTile =(TileClick) obj.GetComponent<TileClick>();//accesing my class for the tile object
                    myTile.setTileCoordinates(new Vector2(posX, posY));
                    tileRow.Add(myTile);
                    changeDirectionOftile = !changeDirectionOftile;
                    continue;
                }
                else
                {
                    var obj = Instantiate(gameTile, new Vector3(x * sizeOfMapBlock, y, 0), Quaternion.Euler(0, 0, 0));
                    obj.transform.parent = backGround.transform;

                    TileClick myTile = (TileClick)obj.GetComponent<TileClick>();//accesing my class for the tile object
                    myTile.setTileCoordinates(new Vector2(posX, posY));
                    tileRow.Add(myTile);
                    changeDirectionOftile = !changeDirectionOftile;
                    continue;
                }
            }
            maptiles.Add(tileRow);
        }

    }
    void Start(){
        for(int y=0; y < maptiles.Count; y++)
        {
            for(int x=0; x < maptiles[y].Count; x++)
            {
                if (x == 10 && y == 9)
                {
                    Debug.Log("x: " + maptiles[y][x].getCoordinates().x.ToString() + " y: " + maptiles[y][x].getCoordinates().y.ToString());
                    maptiles[y][x].setTypeOfunit(testUnit);
                }
                
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
        
    }
   
}
