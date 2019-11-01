using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Hrac : MonoBehaviour
{
    // Start is called before the first frame update

    //public for testing
    private readonly int maximalNumberOfMoves = 8; 

   // public List<TileClick> placedBlocks;//asi by stacil int nwm proc tu je list
        
    private List<List<TileClick>> groupsOfUnits =new List<List<TileClick>>();
    void Start()
    {
        
    }
    public void selectGroupOfUnits(int y)
    {
        //oznaci vsechny jsednotky ve skupine
        for (int x =0; x < this.groupsOfUnits[y].Count; x++)
        {

            groupsOfUnits[y][x].changeToMarked();
        }
    }
   
    public void deSelectGroupOfUnits(int y)
    {
        //oznaci vsechny jsednotky ve skupine
        for (int x = 0; x < this.groupsOfUnits[y].Count; x++)
        {

            groupsOfUnits[y][x].changeToUnMark();
        }
    }
    public static void moveGroupOfPlayers(Vector2 movementVector, int groupOfPlayers)
    {
        //doplnit 
    }
    public void setNewGroupOfPlayerUnits(List<TileClick> units)
    {

        for (int x = 0; x < units.Count; x++)
        {
            units[x].setGroupnumber(this.groupsOfUnits.Count);
            units[x].setPlayer(this);
        }

        this.groupsOfUnits.Add(units);
        
    }
    public List<List<TileClick>> getAllPlayersUnits()
    {
        return this.groupsOfUnits;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public int numberOfPossibleMovesByCurUnit()
    {
        return 0;
    }
    public void moveUnitsInGroup(int groupNumber, Vector2 vectorOfmovement,Vector2 clickedTile)
    {
        Debug.Log("$------------------Last clicked tile " +clickedTile);

        List<TileClick> movedTiles = new List<TileClick>();
        mapCreation.helpFieldForTileActualization = new List<Vector2>();

        Vector2 finalCoordinates;

        //fyzicky presun bojovych jednotek
        for (int x=0; x < this.groupsOfUnits[groupNumber].Count; x++)
        {

            finalCoordinates = new Vector2 (this.groupsOfUnits[groupNumber][x].getCoordinatesOfTheChild().x+ vectorOfmovement.x , this.groupsOfUnits[groupNumber][x].getCoordinatesOfTheChild().y + vectorOfmovement.y );
            movedTiles.Add( mapCreation.moveUnits( this.groupsOfUnits[groupNumber][x].getCoordinatesOfTheChild(), finalCoordinates));
            

        }

        //aktualizace seznamu jednotek
        ///asi neni potreba/*
        /*
        for(int x = 0; x < this.groupsOfUnits[groupNumber].Count; x++)
        {
            //tady muze byt problem
            Debug.Log($"777777777++++++group accesibility coor update {this.groupsOfUnits[groupNumber][x].getChildObject()}");
            this.groupsOfUnits[groupNumber][x].setUnitCoordinates(mapCreation.helpFieldForTileActualization[x]);
        }*/

        //aktualizace na nove blocky bohuzel nevim jestli to jde vyresit jinak kdyz vytvarim nove soubory
        for (int x = 0; x < this.groupsOfUnits[groupNumber].Count; x++)
            this.groupsOfUnits[groupNumber][x] = movedTiles[x];
        
        

        mapCreation.helpFieldForTileActualization = new List<Vector2>();
        this.deSelectGroupOfUnits(groupNumber);
      
    }
    public void reverseMoveUnitsInGroup(int groupNumber, Vector2 vectorOfmovementReverseTOPreviusOne, Vector2 componentOfWar)
    {

        //reverzni pohyb pred implementaci
        //asi nebude treba implementovat
        Vector2 finalCoordinates;
        for (int x = this.groupsOfUnits[groupNumber].Count-1; x > 0 ; x--)
        {
            if (this.groupsOfUnits[groupNumber][x] == null)
            {
                //place component of war 
                
            }
            //if vektro vzdalenosti odpovida opacnemu componentOfwar se nastakuje
            Debug.Log($"kontrola souradnic {this.groupsOfUnits[groupNumber][x].getCoordinatesOfTheChild().x + vectorOfmovementReverseTOPreviusOne.x}");
            Debug.Log($"kontrola souradnic {this.groupsOfUnits[groupNumber][x].getCoordinatesOfTheChild().y + vectorOfmovementReverseTOPreviusOne.y}");

            finalCoordinates = new Vector2(this.groupsOfUnits[groupNumber][x].getCoordinatesOfTheChild().x + vectorOfmovementReverseTOPreviusOne.x,
                                           this.groupsOfUnits[groupNumber][x].getCoordinatesOfTheChild().y + vectorOfmovementReverseTOPreviusOne.y);

            mapCreation.moveUnits(this.groupsOfUnits[groupNumber][x].getCoordinatesOfTheChild(), finalCoordinates);

        }
    }
    public Hrac getPlayer()
    {
        //mozna bude potreba instantiate
        return this;
    }
}
