using UnityEngine;

public abstract class ItemObj : MonoBehaviour
{
    /// <summary>
    /// 아이템을 사용합니다.
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    public abstract bool TryUse(Player player);

    /// <summary>
    /// 아이템을 줍습니다. Type이 Consumed면 그냥 바로 사용했다는 뜻입니다.
    /// </summary>
    /// <param name="owner"></param>
    /// <returns>아이템의 타입 여부를 반환합니다.</returns>
    public abstract ItemType PickItem(Player owner);
}

public enum ItemType
{
    Equip,
    Consumed,
}