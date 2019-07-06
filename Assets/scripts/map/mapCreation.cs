using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapCreation : MonoBehaviour
{
    // Start is called before the first frame update
        public GameObject gameTile;
        public GameObject backGround;


    private const float sizeOfMapBlock = 0.8F;

    private void Awake()
    {
        Vector3 scaleOfObject = backGround.GetComponent<Renderer>().bounds.size; //backGround.GetComponent<Transform>().lossyScale;
        Debug.Log($"x= " + scaleOfObject.x);
        Debug.Log($"Y= " + scaleOfObject.y);
        Debug.Log($"z= " + scaleOfObject.z);
        bool changeDirectionOftile = true;

        for (float y = backGround.transform.transform.position.y - scaleOfObject.y; y < scaleOfObject.y; y += 0.95f)
        {
            for (float x = backGround.transform.transform.position.x - scaleOfObject.x; x < scaleOfObject.x * 2 * sizeOfMapBlock; x += sizeOfMapBlock)
            {
                if (changeDirectionOftile)
                {

                    var obj = Instantiate(gameTile, new Vector3(x * sizeOfMapBlock, y, 0), Quaternion.Euler(0, 0, 180));
                    obj.transform.parent = backGround.transform;
                    changeDirectionOftile = !changeDirectionOftile;
                    continue;
                }
                else
                {
                    var obj = Instantiate(gameTile, new Vector3(x * sizeOfMapBlock, y, 0), Quaternion.Euler(0, 0, 0));
                    obj.transform.parent = backGround.transform; ;

                    changeDirectionOftile = !changeDirectionOftile;
                    continue;
                }

            }
        }

    }
    void Start(){
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
   
}
