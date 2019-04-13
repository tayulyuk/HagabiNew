using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropellerShowManager : MonoBehaviour
{

    public GameObject propeller;
    
    public void OnPropeller()
    {
        propeller.GetComponent<SpinPropeller>().speed = 150;
    }
    public void OffPropeller()
    {
        propeller.GetComponent<SpinPropeller>().speed = 0;
    }

}
