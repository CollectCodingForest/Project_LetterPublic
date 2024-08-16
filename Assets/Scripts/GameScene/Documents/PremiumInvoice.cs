using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PremiumInvoice : BasicInvoice // 프리미엄 송장
{
    public ParcelSize Size { get; set; }                 // 택배 사이즈
    public HandlingOption Handling { get; set; }         // 처리 동의사항
    public AgreementOption HazardAgreement { get; set; } // 위험물 동의사항

    [Header("Toggles")]
    public Toggle returnToSenderTogs;
    public Toggle disposeTogs;
    public Toggle agreeTogs;
    public Toggle disagreeTogs;

    private void Awake()
    {
        if (textElements == null)
        {
            textElements = new TMP_Text[12];
        }
    }

    protected override void UpdateUI()
    {
        textElements[11].text = $"크기: {Size.Width} * {Size.Height}";

        base.UpdateUI();

        // HandlingOption에 따라 체크박스 설정
        if (returnToSenderTogs != null && disposeTogs != null)
        {
            returnToSenderTogs.isOn = (Handling == HandlingOption.ReturnToSender);
            disposeTogs.isOn = (Handling == HandlingOption.Dispose);

            // 비활성화 처리
            returnToSenderTogs.interactable = false;
            disposeTogs.interactable = false;
        }

        // HazardAgreement에 따라 체크박스 설정
        if (agreeTogs != null && disagreeTogs != null)
        {
            agreeTogs.isOn = (HazardAgreement == AgreementOption.Agree);
            disagreeTogs.isOn = (HazardAgreement == AgreementOption.Disagree);

            // 비활성화 처리
            agreeTogs.interactable = false;
            disagreeTogs.interactable = false;
        }
    }
}