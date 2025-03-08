using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace WinterUniverse
{
    [RequireComponent(typeof(Image))]
    public class TabButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, ISelectHandler, ISubmitHandler
    {
        [SerializeField] private Button _thisButton;
        [SerializeField] private TabGroup _group;
        [SerializeField] private Image _background;

        public Image Background => _background;

        public void OnPointerEnter(PointerEventData eventData)
        {
            _thisButton.Select();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _group.OnTabButtonExited(this);
        }

        public void OnSelect(BaseEventData eventData)
        {
            _group.OnTabButtonEntered(this);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _group.OnTabButtonPressed(this);
        }

        public void OnSubmit(BaseEventData eventData)
        {
            _group.OnTabButtonPressed(this);
        }

        public void OnPressed()
        {

        }

        public void OnDeselect()
        {

        }
    }
}