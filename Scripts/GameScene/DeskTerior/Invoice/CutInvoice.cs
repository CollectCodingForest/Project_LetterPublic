using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class CutInvoice : MonoBehaviour
{
    public GameObject MainDesk;
    public GameObject InvoiceBottom;
    public GameObject InvoiceLower;

    public GameObject SubInvoice;

    private Image image;

    public SynkInvoice synkInvoice;

    private BasicInvoice basicInvoice;
    public Image mainInvoiceImage;
    public event Action OnCutEnd;

    private void Awake()
    {
        image = GetComponent<Image>();
        synkInvoice = InvoiceBottom.GetComponent<SynkInvoice>();
        MainDesk = this.gameObject.transform.parent.gameObject.transform.parent.gameObject;
        basicInvoice = UtilGeneric.FindTInHierarchy<BasicInvoice>(gameObject);

        SubInvoice = GameObject.Find("Sub_InvoiceLower").gameObject;
    }

    public void CuttingAnimation()  //송장 나누기
    {

        mainInvoiceImage.raycastTarget = false;
        basicInvoice.RaycastDisabled();

        transform.DOLocalMoveX(290, 1.5f).SetEase(Ease.InOutCubic).OnComplete(SetNextAnimation);
    }

    void SetNextAnimation()
    {
        MoveInvoiceParent();
    }

    void MoveInvoiceLower()
    {
        float transform = InvoiceLower.transform.position.y - 20;
        InvoiceLower.transform.DOLocalMoveY(transform, 0.8f).SetEase(Ease.OutExpo).OnComplete(SetOff);
    }

    void SetOff()
    {
        mainInvoiceImage.raycastTarget = true;
        basicInvoice.RaycastEnabled();
        OnCutEnd?.Invoke();

        image.enabled = false;
        synkInvoice.IsCut = true;

        MainObjectEvent main = InvoiceLower.AddComponent<MainObjectEvent>();
        SubObjectEvent sub = SubInvoice.AddComponent<SubObjectEvent>();

        SubInvoice.GetComponent<SubObjectEvent>().enabled = true;
        InvoiceLower.GetComponent<MainObjectEvent>().enabled = true;
        sub.Initialize();
        main.Initialize();
    }
    void MoveInvoiceParent()
    {
        InvoiceLower = InvoiceBottom.transform.parent.gameObject;
        InvoiceLower.transform.SetParent(MainDesk.transform);
        MoveInvoiceLower();
    }

    // 되돌리기
    public void ResetInvoice()
    {
        InvoiceLower.GetComponent<MainObjectEvent>().enabled = false;
        SubInvoice.GetComponent<SubObjectEvent>().enabled = false;

        Destroy(InvoiceLower.GetComponent<MainObjectEvent>());
        Destroy(SubInvoice.GetComponent<SubObjectEvent>());

        synkInvoice.IsCut = false;
        transform.localPosition = new Vector3(-277f, -175f, 0f);
        image.enabled = true;

        InvoiceLower.transform.SetParent(mainInvoiceImage.transform.parent);
        transform.SetAsLastSibling();
        InvoiceLower.transform.localPosition = new Vector3(0f, -300f, 0f);
        InvoiceBottom.gameObject.SetActive(true);
    }
}
