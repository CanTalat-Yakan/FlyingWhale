using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetPivot : MonoBehaviour
{
    public static GetPivot Instance {get; private set;}

    void Awake(){
        if(Instance)
            Destroy(Instance.gameObject);

        Instance = this;
    }
}
