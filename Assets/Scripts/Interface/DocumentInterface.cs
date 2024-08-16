using UnityEngine.EventSystems;

namespace DocumentInterface
{
    // Sticker 기능을 사용할 서류클래스에 상속.
    public interface IStickerable
    {
        public bool IsSticker { get; set; }

        /// <summary>
        /// 스티커를 붙이는 메서드
        /// </summary>
        public void AttachSticker();

        /// <summary>
        /// 스티커를 제거하는 메서드
        /// </summary>
        public void RemoveSticker();
    }

    public interface IReturnable
    {
        void ReturnObj(PointerEventData eventData);
    }

    public interface ICollectable
    {
        void Collect();
    }

    public interface ILegal
    {
        // 불일치한 랜덤정보를 덮어씌우는 메서드
        public void OverrideRandomInfo();
    }
}