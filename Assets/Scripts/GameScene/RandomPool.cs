using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPool
{
    // 파싱한 랜덤Pool 데이터를 모아놓은 클래스
    // 디버그용 풀
    
    public List<string> usedNamePool = new List<string>();    
    //public List<ItemData> itemPool = new List<ItemData>() {new ItemData("구두", 0.8f, true), new ItemData("덤벨", 8f, true),
    //                                                        new ItemData("책", 1f, true), new ItemData("의류", 2.1f, true) };
    public Dictionary<string, ItemData> itemPool = new Dictionary<string, ItemData>() 
    {
        {"구두", new ItemData("구두", 1f, true) },
        { "덤벨", new ItemData("덤벨", 8f, true) },
        { "책", new ItemData("책", 1f, true) }, 
        { "의류", new ItemData("의류", 2f, true) } 
    };
    public List<string> itemNamePool = new List<string>() { "구두", "덤벨", "책", "의류" };


    /// <summary>
    /// Name, Address
    /// </summary>
    public Dictionary<string, List<string>> randomPool = new Dictionary<string, List<string>>();

}
