using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropellerShowManager : MonoBehaviour
{

    public GameObject propeller;
    
    public void OnPropeller()
    {
        propeller.GetComponent<SpinPropeller>().isTurn = true;
    }
    public void OffPropeller()
    {
        propeller.GetComponent<SpinPropeller>().isTurn = false;
    }

}
