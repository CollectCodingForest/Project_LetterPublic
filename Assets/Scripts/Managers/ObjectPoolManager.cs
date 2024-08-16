using UnityEngine;
using UnityEngine.Pool;

public class ObjectPoolManager : MonoBehaviour
{
    // TODO:: 제네릭
    // 유니티가 지원하는 오브젝트풀을 이용한 오브젝트풀 매니저
    public static ObjectPoolManager instance;

    public int defaultCapacity = 10;
    public int maxSize = 20;
    public ChatBubble bubblePrefab;

    public IObjectPool<ChatBubble> Pool;    // 유니티 지원 오브젝트풀


    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        Init();
        Init2();
    }

    private void Init()
    {
        Pool = new ObjectPool<ChatBubble>(CreatePooledItem, ActionOnGet, ActionOnRelease, ActionOnDestroy, true, defaultCapacity, maxSize);

        for (int i = 0; i < defaultCapacity; ++i)
        {
            ChatBubble chatBubble = CreatePooledItem();
            Pool.Release(chatBubble);
        }
    }

    private ChatBubble CreatePooledItem()
    {
        ChatBubble poolGO = Instantiate(bubblePrefab, gameObject.transform);
        //poolGO.GetComponent<ChatBubble>().Pool = this.Pool;
        
        return poolGO;
    }
    private void ActionOnGet(ChatBubble poolGO)
    {
        poolGO.gameObject.SetActive(true);
    }
    private void ActionOnRelease(ChatBubble poolGO)
    {
        poolGO.gameObject.SetActive(false);
    }
    private void ActionOnDestroy(ChatBubble poolGO)
    {
        Destroy(poolGO);
    }
    /// <summary>
    /// /////////////////////////
    /// </summary>

    public IObjectPool<SampleObject> Pool2;
    public SampleObject prefab2;
    public Transform interrogation;
    private void Init2()
    {
        Pool2 = new ObjectPool<SampleObject>(CreatePooledItem2, ActionOnGet, ActionOnRelease, ActionOnDestroy, true, defaultCapacity, maxSize);

        for (int i = 0; i < defaultCapacity; ++i)
        {
            SampleObject go = CreatePooledItem2();
            //go.AddComponent<SampleObject>();
            Pool2.Release(go);
        }
    }

    private SampleObject CreatePooledItem2()
    {
        SampleObject go = Instantiate(prefab2, interrogation);

        return go;
    }
    private void ActionOnGet(SampleObject poolGO)
    {
        poolGO.gameObject.SetActive(true);
    }
    private void ActionOnRelease(SampleObject poolGO)
    {
        poolGO.gameObject.SetActive(false);
    }
    private void ActionOnDestroy(SampleObject poolGO)
    {
        Destroy(poolGO);
    }
}