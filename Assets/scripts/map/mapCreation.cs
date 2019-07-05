using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapCreation : MonoBehaviour
{
    // Start is called before the first frame update
        public GameObject gameTile;
        public GameObject backGround;


    private const float sizeOfMapBlock = 0.8F;
    void Start(){
        Vector3 scaleOfObject =  backGround.GetComponent<Renderer>().bounds.size; //backGround.GetComponent<Transform>().lossyScale;
        Debug.Log($"x= " + scaleOfObject.x);
        Debug.Log($"Y= " + scaleOfObject.y);
        Debug.Log($"z= " + scaleOfObject.z);
        bool changeDirectionOftile = true;
       /* for (float y= backGround.transform.transform.position.y-8; y < scaleOfObject.y; y+=0.95f)
        {
            for (float x = backGround.transform.transform.position.x-8; x < scaleOfObject.x *2 * sizeOfMapBlock; x += sizeOfMapBlock)
            {
                if (changeDirectionOftile)
                {
                    Instantiate(gameTile, new Vector3(x * sizeOfMapBlock, y, 0), Quaternion.Euler(0, 0, 180));
                    changeDirectionOftile = !changeDirectionOftile;
                    continue;
                }
                else
                {
                    Instantiate(gameTile, new Vector3(x * sizeOfMapBlock, y, 0), Quaternion.Euler(0, 0, 0));
                    changeDirectionOftile = !changeDirectionOftile;
                    continue;
                }

            }
        }*/

    }
    // Update is called once per frame
    void Update()
    {
        
    }
   
}
