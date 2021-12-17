using UnityEngine;

public class Sewing : MonoBehaviour
{
    public int stage;
    public GameObject popUp;

    public void PopUp()
    {
        var canvas = GameObject.Find("PopUpCanvas").transform;
        Instantiate(popUp, canvas);
    }
}
