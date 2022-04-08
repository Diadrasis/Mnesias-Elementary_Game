using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveMat : MonoBehaviour
{
    public Material pnlMat;
    float myValue = 5.0f;
    float finalValue = 0;
    // Start is called before the first frame update
    void Start()
    {
        //pnlMat.SetFloat("_Level", 0.5f);
        pnlMat.SetFloat("_Level", finalValue);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        finalValue += 1.0f / myValue * Time.deltaTime;
        pnlMat.SetFloat("_Level", finalValue);
        //Debug.Log("Value: "+finalValue);
    }
}
