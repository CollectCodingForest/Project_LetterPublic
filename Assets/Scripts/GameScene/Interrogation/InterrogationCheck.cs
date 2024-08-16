using UnityEngine;

public class InterrogationCheck
{
    public int CheckObjectType(TextObjectType type, bool regul)
    {
        int? num;

        if (regul)
        {
            num = 0;
        }
        else if (!regul)
        {
            if (type == TextObjectType.Name)
            {
                num = Random.Range(3010, 3013);
            }
            else if (type == TextObjectType.Address)
            {
                num = Random.Range(3020, 3023);
            }
            else if (type == TextObjectType.RRN)
            {
                num = Random.Range(3030, 3032);
            }
            else if (type == TextObjectType.IssueDate)
            {
                num = Random.Range(3000, 3009);
            }
            else if (type == TextObjectType.TrackingNumber)
            {
                num = Random.Range(3000, 3009);
            }
            else if (type == TextObjectType.Contact)
            {
                num = Random.Range(3000, 3009);
            }
            else if (type == TextObjectType.RecipientName)
            {
                num = Random.Range(3000, 3009);
            }
            else if (type == TextObjectType.RecipientAddress)
            {
                num = Random.Range(3000, 3009);
            }
            else if (type == TextObjectType.RecipientContact)
            {
                num = Random.Range(3000, 3009);
            }
            else if (type == TextObjectType.ParcelContents)
            {
                num = Random.Range(3000, 3009);
            }
            else if (type == TextObjectType.Quantity)
            {
                num = Random.Range(3000, 3009);
            }
            else if (type == TextObjectType.Weight)
            {
                num = Random.Range(3000, 3009);
            }
            else if (type == TextObjectType.Fare)
            {
                num = Random.Range(3000, 3009);
            }
            else if (type == TextObjectType.CustomsNumber)
            {
                num = Random.Range(3000, 3009);
            }
            else if (type == TextObjectType.CC_IssueDate)
            {
                num = Random.Range(3000, 3009);
            }
            else if (type == TextObjectType.CC_ExpiryDate)
            {
                num = Random.Range(3000, 3009);
            }
            else if (type == TextObjectType.PA_ApplicationDate)
            {
                num = Random.Range(3000, 3009);
            }
            else if (type == TextObjectType.PP_IssueDate)
            {
                num = Random.Range(3000, 3009);
            }
            else if (type == TextObjectType.PP_ExpiryDate)
            {
                num = Random.Range(3000, 3009);
            }
            else
            {
                num = null;
                Debug.Assert(false, "이 경고가 발생하면 안됨.");
            }
        }

        else
        {
            num = null;
            Debug.Assert(false, "이 경고가 발생하면 안됨.");
        }

        return (int)num;
    }
}
