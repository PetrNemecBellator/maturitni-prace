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
    public void moveUnitsInGroup(int groupNumber, Vector2 vectorOfmovement)
    {
        Vector2 finalCoordinates;
        for(int x=0; x < this.groupsOfUnits[groupNumber].Count; x++)
        {

            finalCoordinates = new Vector2 (this.groupsOfUnits[groupNumber][x].getCoordinates().x+ vectorOfmovement.x , this.groupsOfUnits[groupNumber][x].getCoordinates().y + vectorOfmovement.y );

            mapCreation.moveUnits( this.groupsOfUnits[groupNumber][x].getCoordinates(), finalCoordinates);
            
        }
    }
}
