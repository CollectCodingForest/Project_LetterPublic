using System;

[Serializable]
public class PersonInfo // 보내는 사람, 받는 사람 정보
{
    public string Name;   // 이름
    public string Address;// 주소
    public string Contact; // 연락처
    public bool IsPayment; // 운송료 청구인

    public PersonInfo() { }
    public PersonInfo(string name, string address, string contact, bool isPayment)
    {
        Name = name;
        Address = address;
        Contact = contact;
        IsPayment = isPayment;
    }
}