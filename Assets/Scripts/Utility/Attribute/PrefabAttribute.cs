using System;

namespace Utility.Attribute
{
    /// <summary>
    /// 이 클래스가 프리팹으로 사용될 수 있음을 나타냅니다.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class PrefabAttribute : System.Attribute {
        public readonly string name;
        public readonly string path;
        public readonly bool isAsync;

        public PrefabAttribute(string name, string path = "Resources", bool isAsync = false) {
            this.name = name;
            this.path = path;
            this.isAsync = isAsync;
        }
    }
}