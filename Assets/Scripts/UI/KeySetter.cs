using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utility.Manager;
using Utility.ScriptableObject;

public class KeySetter : MonoBehaviour
{
    private Coroutine gettingInputCoroutine;
    
    [SerializeField] private InputController inputController;
    
    List<KeyPair> keyPairs = new ();
    
    public void GetTypingKey(GameObject target)
    {
        string key = target.transform.GetChild(0).GetComponent<TMP_Text>().text;
        
        SoundManager.Instance.PlaySFX("menu-select");
        
        gettingInputCoroutine = StartCoroutine(GetTypingKeyCoroutine(key));
    }
    
    private IEnumerator GetTypingKeyCoroutine(string keyName)
    {
        while (true)
        {
            foreach (KeyCode keyCode in Enum.GetValues(typeof(KeyCode)))
            {
                if(keyCode is KeyCode.Mouse0 or KeyCode.Mouse1 or KeyCode.Mouse2) continue;
                if(keyCode is KeyCode.Mouse3 or KeyCode.Mouse4 or KeyCode.Mouse5) continue;
                if(keyCode is KeyCode.Escape or  KeyCode.Backspace) continue;
                
                if (Input.GetKeyDown(keyCode))
                {
                    inputController.SetKey(keyName, keyCode);
                    SoundManager.Instance.PlaySFX("menu-select");
                    Reload();
                    yield break;
                }
            }
            yield return null;
        }
    }

    private void Awake()
    {
        Reload();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Backspace) || Input.GetMouseButtonDown(1))
        {
            if (gettingInputCoroutine != null)
            {
                StopCoroutine(gettingInputCoroutine);
            }
        }
    }

    private void Reload()
    {
        keyPairs.Clear();
        foreach (Transform keyPairObject in transform)
        {
            //if keyPairObject doesn't have 2 child continue
            if (keyPairObject.childCount != 2) continue;
            
            keyPairs.Add(new KeyPair(keyPairObject.gameObject, inputController));
        }
    }
}

internal struct KeyPair
{
    public KeyPair(GameObject keyPairObject, InputController inputController)
    {
        keyText = keyPairObject.transform.GetChild(0).GetComponent<TMP_Text>();
        keyCodeText = keyPairObject.transform.GetChild(1).GetComponent<TMP_Text>();
        
        keyName = keyText.text;
        keyCode = inputController.SearchKey(keyName);
        
        keyCodeText.text = keyCode.ToString();
    }
    
    public TMP_Text keyText;
    public TMP_Text keyCodeText;
    
    public string keyName;
    public KeyCode keyCode;
}
