using UnityEngine.UIElements;

public static class VisualElementExtensions
{
    public static void SetActive(this VisualElement element, bool active)
    {
        element.style.display = active ? DisplayStyle.Flex : DisplayStyle.None;
    }
}
