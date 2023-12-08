using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// Allows moving between UI components using tab and shift-tab.
/// Doesn't work :<
/// </summary>
public class TabNavigator : MonoBehaviour
{
    EventSystem system;

    bool isShiftHeld
    {
        get { return Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift); }
    }

    void Start()
    {
        system = EventSystem.current;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            FocusNextSelectable();
        }
    }

    void FocusNextSelectable()
    {
        var nextSelectable = GetNextSelectable();

        if (!nextSelectable)
        {
            return;
        }

        SimulateClick(nextSelectable);
        system.SetSelectedGameObject(nextSelectable.gameObject);
    }

    Selectable GetNextSelectable()
    {
        if (!system.currentSelectedGameObject)
        {
            return null;
        }

        var currentSelectable = system.currentSelectedGameObject.GetComponent<Selectable>();

        if (!currentSelectable)
        {
            return null;
        }

        return isShiftHeld ?
            currentSelectable.FindSelectableOnUp() :
            currentSelectable.FindSelectableOnDown();
    }

    void SimulateClick(Selectable selectable)
    {
        var clickableElement = selectable.GetComponent<IPointerClickHandler>();

        if (clickableElement == null)
        {
            return;
        }

        var eventData = new PointerEventData(system);
        clickableElement.OnPointerClick(eventData);
    }
}