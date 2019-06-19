using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CreateHUScript : MonoBehaviour
{
    private string packagingMat   = "";
    private string workStation    = "";
    private string reference      = "";
    private string madeIn         = "";
    private string dateExpiration = "";
    private int quantity          =  0;

    public TMPro.TMP_InputField inputPackaging;
    public TMPro.TMP_InputField inputWorkStation;
    public TMPro.TMP_InputField inputReference;
    public TMPro.TMP_InputField inputMadeIn;
    public TMPro.TMP_InputField inputExpirationDate;
    public TMPro.TMP_InputField inputQuantity;

    public OngletManager om;
    public IWayInfoManager infoHU;

    private void Start()
    {
        inputPackaging.onValueChanged     .AddListener(delegate { SetPackaging     (inputPackaging     ); });
        inputWorkStation.onValueChanged   .AddListener(delegate { SetWorkStation   (inputWorkStation   ); });
        inputReference.onValueChanged     .AddListener(delegate { SetReference     (inputReference     ); });
        inputMadeIn.onValueChanged        .AddListener(delegate { SetMadeIn        (inputMadeIn        ); });
        inputExpirationDate.onValueChanged.AddListener(delegate { SetExpirationDate(inputExpirationDate); });
        inputQuantity.onValueChanged      .AddListener(delegate { SetQuantity      (inputQuantity      ); });
    }

    public void SetPackaging(TMPro.TMP_InputField input)
    {
        packagingMat = input.text;
    }

    public void SetWorkStation(TMPro.TMP_InputField input)
    {
        workStation = input.text;
    }

    public void SetReference(TMPro.TMP_InputField input)
    {
        reference = input.text;
    }

    public void SetMadeIn(TMPro.TMP_InputField input)
    {
        madeIn = input.text;
    }

    public void SetExpirationDate(TMPro.TMP_InputField input)
    {
        dateExpiration = input.text;
    }

    public void SetQuantity(TMPro.TMP_InputField input)
    {
        quantity = int.Parse(input.text);
    }

    public void ClickOK()
    {
        if(reference != null && quantity == 0)
        {
            infoHU.refStringIWay = reference;
            infoHU.pcbIntIWay = quantity;
            om.CreateHUOK();
        }
        else
        {

        }
    }

    public void ClickCancel()
    {
        om.CreateHUCancel();
    }
}
