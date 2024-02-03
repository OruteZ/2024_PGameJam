using UnityEngine;

public class ItemObj : MonoBehaviour
{
    [SerializeField] protected Player usingPlayer = null;//아이템을 사용하고 있는 플레이어

    protected Sprite mainSprite;
    public Sprite MainSprite { get => mainSprite; }

    private void Awake()
    {
        usingPlayer = null;
    }

    private void Start()
    {
        mainSprite = GetComponent<SpriteRenderer>().sprite;
    }


    /// <summary>
    /// 아이템을 사용합니다.
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    public virtual bool TryUse(Player player, out bool isDestroyed)
    {
        isDestroyed = false;

        return false;
    }

    /// <summary>
    /// 아이템을 줍습니다. Type이 Consumed면 그냥 바로 사용했다는 뜻입니다.
    /// </summary>
    /// <param name="owner"></param>
    /// <returns>아이템의 타입 여부를 반환합니다.</returns>
    public virtual ItemType PickItem(Player owner)
    {
        return ItemType.Consumed;
    }
}

public enum ItemType
{
    Equip,
    Consumed,
}
