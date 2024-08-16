using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Unity.Mathematics;
using UnityEngine;

public class UtilGeneric : MonoBehaviour    // 이름 어울리는거로 바꾸기
{
    private static System.Random random = new System.Random();

    // 어떤 리스트에서 랜덤한 값을 반환하기
    public static T GetRandomValueFromPool<T>(List<T> _pool)
    {
        int r = UnityEngine.Random.Range(0, _pool.Count);
        
        T v = _pool[r];
        
        return v;
    }

    // 랜덤선택한 값이 중복되어 사용되는걸 원하지 않을때 호출. 이름, 주민번호등 NoDuplicated
    public static T GetRandomValueFromPool<T>(List<T> _pool, List<T> usedPool)
    {
        T v;
        while (true)
        {
            int r = UnityEngine.Random.Range(0, _pool.Count);
            v = _pool[r];
            if (usedPool.Count >= _pool.Count)
            {
                Debug.Assert(false, "랜덤Pool을 다 소모함");
                break;
            }
            bool isDuplicate = false;
            foreach (T item in usedPool)
            {
                if(EqualityComparer<T>.Default.Equals(item, v))
                {
                    isDuplicate = true;
                    break;
                }
            }
            if (!isDuplicate)
            {
                break;
            }
        }

        usedPool.Add(v);
        return v;
    }

    // 원래값을 제외한 랜덤값도출
    public static float GetRandomValueExcludingOriginal(float origin, float min, float max)
    {
        float newValue;
        while (true)
        {
            float r = UnityEngine.Random.Range(min, max);
            
            if(r != origin)
            {
                newValue = r;
                break;
            }            
        }
        return newValue;
    }

    // 원하는 자릿수의 랜덤 숫자를 string반환하는 메서드
    public static string GenerateRandomNumber(int digits)
    {
        if (digits <= 0)
        {
            throw new ArgumentException("Number of digits must be greater than zero.");
        }

        System.Random random = new System.Random();
        char[] result = new char[digits];

        for (int i = 0; i < digits; i++)
        {
            result[i] = (char)('0' + random.Next(0, 10));
        }

        return new string(result);
    }

    // 범위 내에서 랜덤 연월일을 반환하는 메서드
    public static string GetRandomDate(DateTime startDate, DateTime endDate)
    {
        if (startDate >= endDate)
        {
            throw new ArgumentException("The start date must be earlier than the end date.");
        }

        System.Random random = new System.Random();
        int range = (endDate - startDate).Days;
        return startDate.AddDays(random.Next(range + 1)).ToString("yyyy/MM/dd");
    }



    // 클래스의 필드 값을 가져와 리스트에 넣어 그 리스트를 반환하는 메서드
    public static List<FieldInfo> GetFieldInfos<T>(T obj)
    {
        FieldInfo[] fields = typeof(T).GetFields(BindingFlags.Public | BindingFlags.Instance);
        return new List<FieldInfo>(fields);
    }

    // 리스트에서 랜덤한 값을 골라, 해당 타입의 리스트풀에서 그 값을 제외한 랜덤한 값을 반환하는 static메서드
    public static FieldInfo GetRandomFieldFromPools<T>(T obj, List<FieldInfo> fields,List<string> typePool)
    {
        int randomFieldIndex = UnityEngine.Random.Range(0, fields.Count);
        FieldInfo selectedField = fields[randomFieldIndex];
        object selectedFieldValue = selectedField.GetValue(obj);

        if(selectedFieldValue is string stringValue)
        {
            string newValue = GetRandomValueExcludingOriginal(typePool, stringValue);
            selectedField.SetValue(obj, newValue);
        }
        else
        {
            throw new Exception("Unsupported field type");
        }

        return selectedField;
    }

    // Pool에서 원래 값을 제외한 랜덤 값을 가져오는 제네릭 메서드
    public static T GetRandomValueExcludingOriginal<T>(List<T> pool, T excludeValue)
    {
        List<T> tempPool = new List<T>(pool);
        tempPool.Remove(excludeValue);
        int randomIndex = UnityEngine.Random.Range(0, tempPool.Count);
        return tempPool[randomIndex];
    }

    public static T[] GetRandomElements<T>(T[] array)
    {
        // 배열이 비어 있으면 빈 배열을 반환합니다.
        if (array == null || array.Length == 0)
        {
            return new T[0];
        }

        // 랜덤한 인덱스 수를 결정합니다.
        int count = random.Next(1, array.Length + 1);

        // 원본 배열의 인덱스를 랜덤하게 섞습니다.
        int[] indices = Enumerable.Range(0, array.Length).OrderBy(_ => random.Next()).ToArray();

        // 랜덤한 인덱스에 해당하는 요소를 선택합니다.
        T[] result = new T[count];
        for (int i = 0; i < count; i++)
        {
            result[i] = array[indices[i]];
        }

        return result;
    }

    public static T FindTInHierarchy<T>(GameObject obj)
    {
        if (obj != null)
        {
            Transform current = obj.transform;
            //Debug.Log(current.name);

            while (current != null)
            {
                T targetT = current.GetComponent<T>();
                if (targetT != null)
                {
                    //Debug.Log(current.name);

                    return targetT;
                }
                current = current.parent;
            }
            return default;
        }
        else
        {
            return default;
        }
    }
}
