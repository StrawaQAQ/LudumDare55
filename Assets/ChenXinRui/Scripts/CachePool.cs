using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CacheObjectData
{
    public int preLoadingCount
    {
        get
        {
            return reallyPreLoadingCount;
        }
        set
        {
            if (!preLoadingCountHasChange)
            {
                reallyPreLoadingCount = value;
                preLoadingCountHasChange = true;
            }
        }

    }
    private int reallyPreLoadingCount;
    private bool preLoadingCountHasChange = false;

    public CacheObjectData()
    {
        reallyMaxNumber = 0;
        maxNumberArrivalTime = Time.time;
    }
    /// <summary>
    /// The largest number ever reached
    /// </summary>
    public int maxNumber
    {
        get
        {
            return reallyMaxNumber;
        }
        set
        {
            if (reallyMaxNumber <= value)
            {
                reallyMaxNumber = value;
                maxNumberArrivalTime = Time.time;
            }
        }
    }
    private int reallyMaxNumber;

    private float maxNumberArrivalTime;
    public float MaxNumberArrivalTime
    {
        get
        {
            return maxNumberArrivalTime;
        }
    }


}
[System.Serializable]
public class PreLoadingData
{

    public int loadCount;
    public GameObject prefab;

}

public class CachePool : MonoBehaviour
{
    private static CachePool instance;
    public const float DynamicCheckTime = 30f;
    public List<PreLoadingData> allPrefabs = new List<PreLoadingData>();

    public Dictionary<string, List<GameObject>> allPassiveObjects = new Dictionary<string, List<GameObject>>();

    public Dictionary<string, List<GameObject>> allActiveObjects = new Dictionary<string, List<GameObject>>();

    public Dictionary<string, CacheObjectData> allCacheObjectDatas = new Dictionary<string, CacheObjectData>();

    private void Awake()
    {

        if (instance != null)
        {
            GameObject.Destroy(gameObject);
            return;
        }
        else instance = this;
        DontDestroyOnLoad(gameObject);

        StartCoroutine(DynamicCheck());
        StartCoroutine(PreLoading());

    }
    private void OnDestroy()
    {
        StopAllCoroutines();
    }


    /// <summary>
    /// 通过与gameObject同名的规则在对象池中寻找物体
    /// </summary>
    /// <param name="gameObject">需要建立的对象</param>
    public static GameObject Instantiate(GameObject gameObject, Transform parentTran = null)
    {
        return instance.GetGameObject(gameObject, parentTran);
    }
    public static void Destroy(GameObject gameObject)
    {
        instance.ReturnPool(gameObject);
    }


    private IEnumerator DynamicCheck()// I think need remake check method
    {
        yield return new WaitForSeconds(DynamicCheckTime);
        while (true)
        {
            foreach (string name in allCacheObjectDatas.Keys)
            {
                if (allCacheObjectDatas[name].MaxNumberArrivalTime + DynamicCheckTime < Time.time &&
                     allPassiveObjects[name].Count > 0 && allActiveObjects[name].Count > 0 && allPassiveObjects[name].Count + allActiveObjects[name].Count > allCacheObjectDatas[name].preLoadingCount)
                {
                    GameObject mid = allPassiveObjects[name][0];
                    allPassiveObjects[name].Remove(mid);
                    GameObject.Destroy(mid);
                }
            }
            yield return new WaitForSeconds(DynamicCheckTime);
        }
    }
    private IEnumerator PreLoading()
    {
        int creatDeltaCount = 0;
        string name;

        for (; allPrefabs.Count > 0;)
        {
            for (int i = 0; i < allPrefabs.Count; i++)
            {
                GameObject selectedObject;
                name = allPrefabs[i].prefab.name.Split("(Clone)", ' ')[0];

                if (allCacheObjectDatas.ContainsKey(name))
                {
                    selectedObject = GameObject.Instantiate(allPrefabs[i].prefab);
                }
                else
                {
                    allCacheObjectDatas.Add(name, new CacheObjectData());
                    allActiveObjects.Add(name, new List<GameObject>());
                    allPassiveObjects.Add(name, new List<GameObject>());
                    selectedObject = GameObject.Instantiate(allPrefabs[i].prefab);


                }

                ReturnPool(selectedObject);

                // save PreLoadingCount in CacheObjectData
                allCacheObjectDatas[name].preLoadingCount = allPrefabs[i].loadCount;

                allPrefabs[i].loadCount--;
                if (allPrefabs[i].loadCount <= 0) allPrefabs.Remove(allPrefabs[i]);
                creatDeltaCount++;
                if (creatDeltaCount == 5)
                {
                    yield return new WaitForSeconds(0.5f);
                    creatDeltaCount = 0;
                }
            }
        }

    }
    ///////////////////////////////////////////////////////////////////////////////////////

    private GameObject GetGameObject(GameObject gameObject, Transform parentTran = null)
    {
        if (gameObject == null) return null;
        string name = gameObject.name.Split("(Clone)", ' ')[0];
        GameObject selectedObject;
        if (allCacheObjectDatas.ContainsKey(name))
        {
            if (allPassiveObjects[name].Count > 0)
            {
                selectedObject = allPassiveObjects[name][0];
                allPassiveObjects[name].Remove(selectedObject);
                selectedObject.SetActive(true);
            }
            else selectedObject = GameObject.Instantiate(gameObject);

        }
        else
        {
            allCacheObjectDatas.Add(name, new CacheObjectData());
            allActiveObjects.Add(name, new List<GameObject>());
            allPassiveObjects.Add(name, new List<GameObject>());
            selectedObject = GameObject.Instantiate(gameObject);


        }

        if (parentTran != null) selectedObject.transform.parent = parentTran;

        allActiveObjects[name].Add(selectedObject);

        allCacheObjectDatas[name].maxNumber = allActiveObjects[name].Count;



        return selectedObject;
    }
    private void ReturnPool(GameObject gameObject)
    {
        if (gameObject == null) return;
        string name = gameObject.name.Split("(Clone)", ' ')[0];
        if (!allCacheObjectDatas.ContainsKey(name))
        {
            GameObject.Destroy(gameObject);
            return;
        }
        allActiveObjects[name].Remove(gameObject);
        gameObject.transform.parent = transform;
        allPassiveObjects[name].Add(gameObject);
        gameObject.SetActive(false);

    }
    ///////////////////////////////////////////////////////////////////////////////////////////

}


