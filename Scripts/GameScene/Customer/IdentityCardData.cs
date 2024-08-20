using System;
using DocumentInterface;

[Serializable]
public class IdentityCardData : ILegal
{
    public string name;
    public string RRN;
    public string address;
    public string issueDate;

    public IdentityCardData(string name, string rRN, string address, string issueDate)
    {
        this.name = name;
        RRN = rRN;
        this.address = address;
        this.issueDate = issueDate;
    }

    public void OverrideRandomInfo()
    {
        Random random = new Random();
        int num;
        if (GameManager.Instance.date <= 1)
        {
            num = random.Next(0, 2);
        }
        else
        {
            num = random.Next(0, 4);
        }
        
        switch (num)
        {
            case 0:
                {
                    name = UtilGeneric.GetRandomValueFromPool(GameSceneManager.Instance.pool.randomPool[PoolKey.Name.ToString()]);

                    GameSceneManager.Instance.wrongPart.Add($"신분증. 이름 다름");
                    break;
                }
            case 1:
                {
                    address = UtilGeneric.GetRandomValueFromPool(GameSceneManager.Instance.pool.randomPool[PoolKey.Address.ToString()]);

                    GameSceneManager.Instance.wrongPart.Add($"신분증. 주소 다름");
                    
                    break;
                }
            case 2:
                {
                    RRN = UtilGeneric.GenerateRandomNumber(6) + '-' + UtilGeneric.GenerateRandomNumber(7);

                    GameSceneManager.Instance.wrongPart.Add($"신분증. 주민번호 다름");
                    break;
                }
            case 3:
                {
                    issueDate = UtilGeneric.GetRandomDate(new DateTime(1999, 1, 1), new DateTime(2010, 12, 31));

                    GameSceneManager.Instance.wrongPart.Add($"신분증. 발급일자 다름");

                    break;
                }
        }


    }
}