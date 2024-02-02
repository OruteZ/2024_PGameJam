using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemStorage", menuName = "ScriptableObject/Item/ItemStorage")]
public class ItemStorage : ScriptableObject//사용할 수 있는 아이템들 관리하는 저장소
{
    [SerializeField] List<ItemObj> items = new List<ItemObj>();

    public GameObject GetItem(Vector3 pos = default)//무작위로 아이템 얻는 함수
    {
        GameObject obj = items[Random.Range(0, items.Count)].gameObject;

        obj = Instantiate(obj, pos, Quaternion.identity);

        return obj;
    }
}
