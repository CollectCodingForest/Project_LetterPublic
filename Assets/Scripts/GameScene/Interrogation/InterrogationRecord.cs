using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class InterrogationRecord : MonoBehaviour
{
    public List<SampleObject> records = new List<SampleObject>(15);
   




    public void AddRecord(string str)
    {
        records.Add(ObjectPoolManager.instance.Pool2.Get());
        records[records.Count - 1].SetTmpText(str);
    }
    public void ClearRecord()   // TODO:: 신청자가 올때 호출하기
    {
        foreach (var record in records)
        {
            ObjectPoolManager.instance.Pool2.Release(record);
        }
        records.Clear();
    }
    
}
