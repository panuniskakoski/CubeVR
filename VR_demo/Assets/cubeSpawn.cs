using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubeSpawn : MonoBehaviour
{
    public GameObject cube;

    private Vector3 startPos1;
    private Vector3 startPos2;
    private Vector3 startPos3;
    private Vector3 startPos4;
    private Vector3 startPos5;
    private Vector3 startPos6;
    private Vector3 startPos7;
    private Vector3 startPos8;

    // Start is called before the first frame update
    void Start()
    {
        startPos1 = GameObject.Find("1").transform.position;
        startPos2 = GameObject.Find("2").transform.position;
        startPos3 = GameObject.Find("3").transform.position;
        startPos4 = GameObject.Find("4").transform.position;
        startPos5 = GameObject.Find("5").transform.position;
        startPos6 = GameObject.Find("6").transform.position;
        startPos7 = GameObject.Find("7").transform.position;
        startPos8 = GameObject.Find("8").transform.position;
    }

    // Spawns back the cube
    private void OnTriggerEnter(Collider other)
    {
        int nro = int.Parse(other.name);
        if (other.tag == "cube")
        {
            switch (nro){
                case 1:
                    other.transform.position = startPos1;
                    break;

                case 2:
                    other.transform.position = startPos2;
                    break;

                case 3:
                    other.transform.position = startPos3;
                    break;

                case 4:
                    other.transform.position = startPos4;
                    break;

                case 5:
                    other.transform.position = startPos5;
                    break;

                case 6:
                    other.transform.position = startPos6;
                    break;

                case 7:
                    other.transform.position = startPos7;
                    break;

                case 8:
                    other.transform.position = startPos8;
                    break;
            }
        }
    }
}
