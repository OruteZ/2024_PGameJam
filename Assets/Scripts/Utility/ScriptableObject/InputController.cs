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
        
        # region INPUT
        public bool GetKeyDown(string name)
        {
            return Input.GetKeyDown(inputDataDic[name]);
        }
        
        public bool GetKey(string name)
        {
            return Input.GetKey(inputDataDic[name]);
        }
        
        public bool GetKeyUp(string name)
        {
            return Input.GetKeyUp(inputDataDic[name]);
        }
        # endregion

        public void SetKey(string name, KeyCode keyCode)
        {
            if (inputDataDic.ContainsKey(name))
            {
                inputDataDic[name] = keyCode;
            }
            else
            {
                Debug.LogError("There is no input name \"" + name + "\"");
            }
        }
    }
}