using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public float startDelay = 5f;

    public ItemStorage itemStorage;
    public float itemCreateDur = 3f;

    public Transform spawnPosTr;
    List<Transform> spawnPos = new List<Transform>();

    [Header("Else Obj")]
    public float objCreateDur = 4f;
    public List<GameObject> elseObjs = new List<GameObject>();
    public float offset = 10f;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform tr in spawnPosTr) spawnPos.Add(tr);

        StartCoroutine(SpawnItem());
        StartCoroutine(SpawnElseObj());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnItem()
    {
        yield return new WaitForSeconds(startDelay);

        WaitForSeconds wait = new WaitForSeconds(itemCreateDur);
        while (true)
        {
            Quaternion rot = Quaternion.Euler(0, 0, Random.Range(0f, 360f));

            itemStorage.GetItem(spawnPos[Random.Range(0, spawnPos.Count)].position).transform.rotation = rot;

            yield return wait;
        }
    }

    IEnumerator SpawnElseObj()
    {
        yield return new WaitForSeconds(startDelay);

        WaitForSeconds wait = new WaitForSeconds(objCreateDur);
        while (true)
        {
            Quaternion rot = Quaternion.Euler(0, 0, Random.Range(0f, 360f));

            Vector3 pos = transform.position + Vector3.right * Random.Range(-offset, offset);

            Instantiate(elseObjs[Random.Range(0, elseObjs.Count)], pos, rot);

            yield return wait;
        }
    }

}
