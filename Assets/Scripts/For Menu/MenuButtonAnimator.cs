using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuButtonAnimator : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    bool isIn;

    [SerializeField] GameObject lightObj;
    [SerializeField] GameObject centerObj;
    [SerializeField] GameObject textObj;

    // Start is called before the first frame update
    void Start()
    {
        isIn = false;

        lightObj.SetActive(false);
        textObj.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isIn)
        {
            lightObj.SetActive(true);
            textObj.SetActive(true);
        }
        else
        {
            lightObj.SetActive(false);
            textObj.SetActive(false);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //tr.localScale = Vector3.Lerp(tr.localScale, Vector3.one * highlightScale, animSpeed * Time.deltaTime);
        isIn = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //tr.localScale = Vector3.Lerp(tr.localScale, Vector3.one, animSpeed * Time.deltaTime);
        isIn = false;
    }
}
