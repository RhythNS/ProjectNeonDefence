using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FloatingTextBehaviour : MonoBehaviour
{

    public GameObject floatingText;
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }


    public void ShowText(string text, int seconds)
    {
        GameObject obj = Instantiate(floatingText, transform.position, Quaternion.identity,transform);
        obj.transform.position += new Vector3(0,3,0);
        obj.GetComponent<TextMesh>().text = text;
        Destroy(gameObject,seconds);
    }


   
}
