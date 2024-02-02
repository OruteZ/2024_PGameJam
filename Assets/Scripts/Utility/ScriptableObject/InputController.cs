using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Utility.Generic;

namespace Utility.ScriptableObject
{
    //create scriptable object
    [CreateAssetMenu(fileName = "InputController", menuName = "ScriptableObject/InputController")]
    public class InputController : UnityEngine.ScriptableObject
    {
        [SerializeField]
        private SerializableDictionary<string, KeyCode> inputDataDic;
        public bool canInput = true;
        
        public KeyCode SearchKey(string name)
        {
            return inputDataDic[name];
        }
        
        # region INPUT
        public bool GetKeyDown(string name)
        {
            //if can input
            if (!canInput) return false;
            return Input.GetKeyDown(inputDataDic[name]);
        }
        
        public bool GetKey(string name)
        {
            if (!canInput) return false;
            return Input.GetKey(inputDataDic[name]);
        }
        
        public bool GetKeyUp(string name)
        {
            if (!canInput) return false;
            return Input.GetKeyUp(inputDataDic[name]);
        }
        # endregion

        public void SetKey(string name, KeyCode keyCode)
        {
            if (inputDataDic.ContainsKey(name) == false)
            {
                Debug.LogError("There is no input name \"" + name + "\"");
                return;
            }
            if(inputDataDic[name] == keyCode) return;
            
            //if there is a key in the dictionary values that is same as the input key code change each other
            foreach (var key in inputDataDic.Keys)
            {
                if (inputDataDic[key] == keyCode)
                {
                    inputDataDic.Remove(key);
                    
                    inputDataDic[key] = inputDataDic[name];
                    inputDataDic[name] = keyCode;
                    return;
                }
            }
            inputDataDic[name] = keyCode;
            
        }
    }
}
