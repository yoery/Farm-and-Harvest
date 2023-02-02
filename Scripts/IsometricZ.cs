using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsometricZ : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //To get all the isometric tiles on the Z easily without overlaying eachoter
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y / 100);
    }

}
