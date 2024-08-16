using System;
using System.Collections.Generic;
using System.Xml.Linq;

public class RandomMethod
{
    public static bool Bool()
    {
        return UnityEngine.Random.value > 0.5f;
    }
    public static string Name()
    {
        return UtilGeneric.GetRandomValueFromPool(GameSceneManager.Instance.pool.randomPool[PoolKey.Name.ToString()], GameSceneManager.Instance.pool.usedNamePool);
    }
    public static string Address()
    {
        return UtilGeneric.GetRandomValueFromPool(GameSceneManager.Instance.pool.randomPool[PoolKey.Address.ToString()]);
    }
    public static string RRN()
    {
        return UtilGeneric.GenerateRandomNumber(6) + "-" + UtilGeneric.GenerateRandomNumber(6);
    }
    public static PersonInfo PersonInfo()
    {
        return new PersonInfo(Name(), Address(), "contact", Bool());
    }
    public static string TrackingNumber()
    {
        return UtilGeneric.GenerateRandomNumber(12);
    }
    public static List<string> ParcelContents()
    {
        return new List<string>() { UtilGeneric.GetRandomValueFromPool(GameSceneManager.Instance.pool.itemNamePool), UtilGeneric.GetRandomValueFromPool(GameSceneManager.Instance.pool.itemNamePool) };
    }
    public static List<int> ParcelQuantity()
    {
        return new List<int>() { UnityEngine.Random.Range(1, 3), UnityEngine.Random.Range(1, 3) };
    }
    public static ParcelSize ParcelSize()
    {
        return new ParcelSize(UnityEngine.Random.Range(100, 700), UnityEngine.Random.Range(100, 700));
    }
    public static string CustomsNumber()
    {
        return UtilGeneric.GenerateRandomNumber(6);
    }
    public static string IssueDate()
    {
        return UtilGeneric.GetRandomDate(new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1), new DateTime(DateTime.Now.Year, DateTime.Now.Month, 31));
    }
    public static string ExpiryDate()
    {
        return UtilGeneric.GetRandomDate(new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1), new DateTime(DateTime.Now.Year, DateTime.Now.Month, 31));
    }
    public static ReceiptReason ReceiptReason()
    {
        return (ReceiptReason)UnityEngine.Random.Range(0, Enum.GetValues(typeof(ReceiptReason)).Length);
    }
    public static float Weight()
    {
        float result = UnityEngine.Random.Range(0, 10f);
        return result;
    }

}