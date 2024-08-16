/*
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DocumentManager : MonoBehaviour
{
    [Header("DocumentPrefab")]
    public BasicInvoice basicInvoicePrefab;
    public CustomsClearance customsClearancePrefab;
    public IdentityCard identityCardPrefab;
    public ParcelApplication parcelApplicationPrefab;
    public ParcelPermit parcelPermitPrefab;
    public PremiumInvoice premiumInvoicePrefab;

    [Header("DocumentData")]
    public TextAsset basicInvoiceData;
    public TextAsset customsClearanceData;
    public TextAsset identityCardData;
    public TextAsset parcelApplicationData;
    public TextAsset parcelPermitData;
    public TextAsset premiumInvoiceData;
    public TextAsset documentData;

    private BasicInvoice basicInvoice;
    private CustomsClearance customsClearance;
    private IdentityCard identityCard;
    private ParcelApplication parcelApplication;
    private ParcelPermit parcelPermit;
    private PremiumInvoice premiumInvoice;
    private PersonInfo personInfo;

    public void OnEnable() // 손님이 찾아올 때 값 설정
    {
        InitializeDocuments();
    }

    public void OnDisable() // 손님이 나갈 때 값 없애기
    {
        ClearDocuments();
    }

    private void InitializeDocuments()
    {
        InitializeIdentityCard();

        // 기본 송장 or 프리미엄 송장 생성 조건문
        bool usePremiumInvoice = CheckPremiumInvoice();

        if (usePremiumInvoice)
        {
            InitializePremiumInvoice();
        }
        else
        {
            InitializeBasicInvoice();
        }

        InitializeCustomsClearance();
        InitializeParcelApplication();
        InitializeParcelPermit();
    }

    private bool CheckPremiumInvoice()
    {
        // 프리미엄 송장을 사용할 조건을 체크하는 로직 구현하기
        // true = 프리미엄 송장, false = 기본 송장
        return false;
    }

    private void InitializeIdentityCard()
    {
        identityCard = Instantiate(identityCardPrefab, transform);
        var data = LoadDocumentData();
        if (data != null && data.Count > 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, data.Count);
            string[] randomEntry = data[randomIndex];
            if (randomEntry.Length >= 3)
            {
                identityCard.Name = randomEntry[0];
                identityCard.RRN = randomEntry[1];
                identityCard.Address = randomEntry[2];
                identityCard.IssueDate = GenerateRandomDateString(2010, 2020);
                identityCard.UpdateUI();
                Debug.Log("identityCard 완료~");
            }
            else
            {
                Debug.LogError("랜덤으로 선택된 데이터의 형식이 잘못되었습니다.");
            }
        }
        else
        {
            Debug.LogError("로드된 데이터가 없습니다.");
        }
    }

    private void InitializeBasicInvoice()
    {
        basicInvoice = Instantiate(basicInvoicePrefab, transform);
        var data = LoadDocumentData();
        if (data != null && data.Count > 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, data.Count);
            string[] randomEntry = data[randomIndex];
            if (randomEntry.Length >= 9)
            {
                // 무작위로 지급 여부 설정
                bool senderPayment = UnityEngine.Random.Range(0, 2) == 0;
                bool recipientPayment = !senderPayment; // senderPayment의 반대 값으로 설정

                basicInvoice.TrackingNumber = GenerateRandomTrackingNumber();
                basicInvoice.Sender = new PersonInfo
                {
                    Name = randomEntry[0],
                    Address = randomEntry[2],
                    Contact = randomEntry[3],
                    IsPayment = senderPayment
                };
                basicInvoice.Recipient = new PersonInfo
                {
                    Name = randomEntry[0],
                    Address = randomEntry[2],
                    Contact = randomEntry[3],
                    IsPayment = recipientPayment
                };
                basicInvoice.ParcelContents = new List<string> { randomEntry[4] };
                basicInvoice.Quantity = new List<int> { int.Parse(randomEntry[5]) };
                basicInvoice.Weight = float.Parse(randomEntry[6]);
                basicInvoice.Fare = UnityEngine.Random.Range(10, 100);
                basicInvoice.HasSenderSignature = UnityEngine.Random.Range(0, 2) == 0;
                basicInvoice.UpdateUI();
                Debug.Log("basicInvoice 완료~");
            }
            else
            {
                Debug.LogError("랜덤으로 선택된 데이터의 형식이 잘못되었습니다.");
            }
        }
        else
        {
            Debug.LogError("로드된 데이터가 없습니다.");
        }
    }

    private void InitializePremiumInvoice()
    {
        premiumInvoice = Instantiate(premiumInvoicePrefab, transform);
        var data = LoadDocumentData();
        if (data != null && data.Count > 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, data.Count);
            string[] randomEntry = data[randomIndex];
            if (randomEntry.Length >= 9)
            {
                // 무작위로 지급 여부 설정
                bool senderPayment = UnityEngine.Random.Range(0, 2) == 0;
                bool recipientPayment = !senderPayment; // senderPayment의 반대 값으로 설정

                premiumInvoice.TrackingNumber = GenerateRandomTrackingNumber();
                premiumInvoice.Sender = new PersonInfo
                {
                    Name = randomEntry[0],
                    Address = randomEntry[2],
                    Contact = randomEntry[3],
                    IsPayment = senderPayment
                };
                premiumInvoice.Recipient = new PersonInfo
                {
                    Name = randomEntry[0],
                    Address = randomEntry[2],
                    Contact = randomEntry[3],
                    IsPayment = recipientPayment
                };
                premiumInvoice.ParcelContents = new List<string> { randomEntry[4] };
                premiumInvoice.Quantity = new List<int> { int.Parse(randomEntry[5]) };
                premiumInvoice.Weight = float.Parse(randomEntry[6]);
                premiumInvoice.Fare = UnityEngine.Random.Range(10, 100);
                premiumInvoice.HasSenderSignature = UnityEngine.Random.Range(0, 2) == 0;
                premiumInvoice.Size = new ParcelSize(int.Parse(randomEntry[7]), int.Parse(randomEntry[8]));
                premiumInvoice.Handling = (HandlingOption)UnityEngine.Random.Range(0, Enum.GetValues(typeof(HandlingOption)).Length);
                premiumInvoice.HazardAgreement = (AgreementOption)UnityEngine.Random.Range(0, Enum.GetValues(typeof(AgreementOption)).Length);
                premiumInvoice.UpdateUI();
                Debug.Log("premiumInvoice 완료~");
            }
            else
            {
                Debug.LogError("랜덤으로 선택된 데이터의 형식이 잘못되었습니다.");
            }
        }
        else
        {
            Debug.LogError("로드된 데이터가 없습니다.");
        }
    }

    private void InitializeCustomsClearance()
    {
        customsClearance = Instantiate(customsClearancePrefab, transform);
        var data = LoadDocumentData();
        if (data != null && data.Count > 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, data.Count);
            string[] randomEntry = data[randomIndex];
            if (randomEntry.Length >= 3)
            {
                customsClearance.Name = randomEntry[0];
                customsClearance.DateOfBirth = randomEntry[1]; // RRN 필드 사용
                customsClearance.CustomsNumber = GenerateCustomsNumber();
                customsClearance.IssueDate = GenerateRandomDateString(2010, 2020);
                customsClearance.ExpiryDate = GenerateRandomDateString(2021, 2030);
                customsClearance.OfficialSeal = UnityEngine.Random.Range(0, 2) == 0;
                customsClearance.UpdateUI();
                Debug.Log("customsClearance 완료~");
            }
            else
            {
                Debug.LogError("랜덤으로 선택된 데이터의 형식이 잘못되었습니다.");
            }
        }
        else
        {
            Debug.LogError("로드된 데이터가 없습니다.");
        }
    }

    private void InitializeParcelApplication()
    {
        parcelApplication = Instantiate(parcelApplicationPrefab, transform);

        var data = LoadParcelApplicationData();

        if (data != null && data.Count > 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, data.Count);

            if (randomIndex >= 0 && randomIndex < data.Count)
            {
                string[] randomEntry = data[randomIndex];

                if (randomEntry.Length >= 10) // 필요한 필드 수
                {
                    parcelApplication.CustomsNumber = randomEntry[0];
                    parcelApplication.TrackingNumber = randomEntry[1];
                    parcelApplication.Weight = float.Parse(randomEntry[2]);
                    parcelApplication.Recipient = new PersonInfo
                    {
                        Name = randomEntry[3],
                        // 값이 CSV에 없으므로 기본값으로 설정
                        Address = "Unknown", 
                        Contact = "Unknown", 
                        IsPayment = false 
                    };
                    parcelApplication.ApplicationDate = randomEntry[4];
                    parcelApplication.ParcelContents = new List<string>(randomEntry[5].Split(';'));
                    parcelApplication.Quantity = new List<int>(Array.ConvertAll(randomEntry[6].Split(';'), int.Parse));
                    parcelApplication.Purpose = (ReceiptReason)Enum.Parse(typeof(ReceiptReason), randomEntry[7]);
                    parcelApplication.PersonalUseReason = randomEntry[8];
                    parcelApplication.OfficialSeal = bool.Parse(randomEntry[9]);

                    parcelApplication.UpdateUI();
                    Debug.Log("parcelApplication 완료~");
                }
                else
                {
                    Debug.LogError("랜덤으로 선택된 데이터의 형식이 잘못되었습니다.");
                }
            }
            else
            {
                Debug.LogError("랜덤 인덱스가 데이터 배열의 범위를 벗어났습니다.");
            }
        }
        else
        {
            Debug.LogError("로드된 데이터가 없습니다.");
        }
    }

    private void InitializeParcelPermit()
    {
        parcelPermit = Instantiate(parcelPermitPrefab, transform);

        var data = LoadParcelPermitData();

        if (data != null && data.Count > 0)
        {
            int randomIndex =   UnityEngine.Random.Range(0, data.Count);

            if (randomIndex >= 0 && randomIndex < data.Count)
            {
                string[] randomEntry = data[randomIndex];

                if (randomEntry.Length >= 6) // 필요한 필드 수
                {
                    parcelPermit.Name = randomEntry[0];
                    parcelPermit.DateOfBirth = randomEntry[1];
                    parcelPermit.TrackinNumber = randomEntry[2];
                    parcelPermit.IssueDate = randomEntry[3];
                    parcelPermit.ExpiryDate = randomEntry[4];
                    parcelPermit.OfficialSeal = bool.Parse(randomEntry[5]);

                    parcelPermit.UpdateUI();
                    Debug.Log("parcelPermit 완료~");
                }
                else
                {
                    Debug.LogError("랜덤으로 선택된 데이터의 형식이 잘못되었습니다.");
                }
            }
            else
            {
                Debug.LogError("랜덤 인덱스가 데이터 배열의 범위를 벗어났습니다.");
            }
        }
        else
        {
            Debug.LogError("로드된 데이터가 없습니다.");
        }
    }

    private void ClearDocuments()
    {
        List<GameObject> documents = new List<GameObject>
        {
            // null이 아닌 경우 리스트에 추가, null인 경우 추가되지 않음
            identityCard?.gameObject,
            basicInvoice?.gameObject,
            premiumInvoice?.gameObject,
            customsClearance?.gameObject,
            parcelApplication?.gameObject,
            parcelPermit?.gameObject
        };

        foreach (var document in documents)
        {
            if (document != null)
            {
                Destroy(document);
            }
        }

        identityCard = null;
        basicInvoice = null;
        premiumInvoice = null;
        customsClearance = null;
        parcelApplication = null;
        parcelPermit = null;
    }

    private List<string[]> LoadIdentityCardData()
    {
        return LoadCSVData(identityCardData, 4); // IdentityCard는 4개의 필드를 필요로 함
    }

    private List<string[]> LoadBasicInvoiceData()
    {
        return LoadCSVData(basicInvoiceData, 14); // BasicInvoice는 14개의 필드를 필요로 함
    }

    private List<string[]> LoadPremiumInvoiceData()
    {
        return LoadCSVData(premiumInvoiceData, 18); // PremiumInvoice는 18개의 필드를 필요로 함
    }

    private List<string[]> LoadCustomsClearanceData()
    {
        return LoadCSVData(customsClearanceData, 6); // CustomsClearance는 6개의 필드를 필요로 함
    }

    private List<string[]> LoadParcelApplicationData()
    {
        return LoadCSVData(parcelApplicationData, 10); // ParcelApplication은 13개의 필드를 필요로 함
    }

    private List<string[]> LoadParcelPermitData()
    {
        return LoadCSVData(parcelPermitData, 6); // ParcelPermit은 6개의 필드를 필요로 함
    }

    string GenerateRandomDateString(int startYear, int endYear) // 랜덤 날짜 뽑기
    {
        System.Random random = new System.Random();
        int year = random.Next(startYear, endYear + 1);
        int month = random.Next(1, 13);
        int day = random.Next(1, DateTime.DaysInMonth(year, month) + 1);

        DateTime randomDate = new DateTime(year, month, day);
        return randomDate.ToString("yyyy-MM-dd");
    }

    private string GenerateRandomTrackingNumber() // 랜덤 운송장 번호 뽑기
    {
        System.Random random = new System.Random();
        string trackingNumber = random.Next(1, 10).ToString(); // 첫 자리 숫자는 1-9 중에서 선택
        for (int i = 1; i < 12; i++) // 나머지 자리 숫자는 0-9 중에서 선택
        {
            trackingNumber += random.Next(0, 10).ToString();
        }
        return trackingNumber;
    }

    private string GenerateCustomsNumber() // 랜덤 통관 번호 뽑기
    {
        return "P" + GenerateRandomTrackingNumber();
    }

    private List<string[]> LoadDocumentData()
    {
        return LoadCSVData(documentData, 13);
    }


    private List<string[]> LoadCSVData(TextAsset csvData, int requiredFields)
    {
        List<string[]> dataEntries = new List<string[]>();

        if (csvData != null)
        {
            string[] data = csvData.text.Split('\n');
            for (int i = 1; i < data.Length; i++) // 헤더 스킵
            {
                string line = data[i].Trim();
                if (string.IsNullOrEmpty(line))
                {
                    continue; // 빈 행 건너뛰기
                }

                string[] entry = line.Split(',');

                if (entry.Length >= requiredFields) // 필요한 필드 수
                {
                    dataEntries.Add(entry);
                }
                else
                {
                    Debug.LogError($"잘못된 데이터 형식: {line}");
                }
            }
        }
        else
        {
            Debug.LogError("CSV 데이터가 null입니다.");
        }

        return dataEntries;
    }
}
*/