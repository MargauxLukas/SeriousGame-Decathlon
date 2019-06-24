using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FicheInfoIway : MonoBehaviour
{
    public Text pcbText;
    public Text refText;

    public BigMonitor mainMonitor;
    public IWayInfoManager managerWay;

    public float coef;

    private Vector3 startPositionMonitor;
    private Vector3 myStartPosition;

    // Start is called before the first frame update
    void Start()
    {
        myStartPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(startPositionMonitor, mainMonitor.gameObject.transform.position) > 0.2f)
        {
            transform.position = myStartPosition + new Vector3(0, Vector3.Distance(startPositionMonitor, mainMonitor.gameObject.transform.position) * coef, 0);
        }

        if(mainMonitor.monitorOpening)
        {
            pcbText.text = managerWay.pcbIntIWay.ToString();
            refText.text = managerWay.refIntIWay.ToString();
        }
    }
}
