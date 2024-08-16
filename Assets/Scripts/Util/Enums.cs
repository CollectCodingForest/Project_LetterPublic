public enum HandlingOption // 처리 동의사항
{
    ReturnToSender, // 발송인에게 반송
    Dispose         // 폐기
}

public enum AgreementOption // 위험물 동의사항
{
    Agree,    // 동의
    Disagree  // 동의 안 함
}

public enum ReceiptReason // 배송 이유
{
    Gift,
    Documents,
    Food,
    UrgentGoods,
    PersonalUse
}

public enum TextObjectType
{
    Name,               // 이름
    RRN,                // 주민등록 번호
    Address,            // 주소
    IssueDate,          // 발급 일자
    TrackingNumber,     // 운송장 번호
    Contact,            // 연락처
    RecipientName,      // 받는 사람 이름
    RecipientAddress,   // 받는 사람 주소
    RecipientContact,   // 받는 사람 연락처
    ParcelContents,     // 택배 내용물
    Quantity,           // 수량
    Weight,             // 중량
    Fare,               // 요금
    CustomsNumber,      // 개인허가번호
    CC_IssueDate,       // CustomsClearance 발급일자
    CC_ExpiryDate,      // CustomsClearance 만료일자
    PA_ApplicationDate, // ParcelApplication 신청일자
    PP_IssueDate,       // ParcelPermit 발급일자
    PP_ExpiryDate,      // ParcelPermit 만료일자
    Regulation,         // 규정집
    ExpiryDate,         // 만료일자
    UseReason,
    Today,


}

public enum PoolKey
{
    Name,
    Address
}