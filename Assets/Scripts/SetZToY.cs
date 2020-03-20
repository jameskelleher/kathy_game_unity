using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetZToY : MonoBehaviour
{
    public float offset = 0;
    // Start is called before the first frame update
    void Start()
    {   
        float x = this.transform.position.x;
        float y = this.transform.position.y;
        this.transform.position = new Vector3(x, y, y-offset);
    }

}
