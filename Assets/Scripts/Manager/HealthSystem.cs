using UnityEngine;
public class HealthSystem : MonoBehaviour
{
    public static HealthSystem instance;

    private RectTransform rectTransform;
    

    private void Awake()
    {
        if(instance != null)
        {
            return;
        }

        instance = this;
    }

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        RefreshUI(3);
    }

    public void RefreshUI(int nbOfCurrentLife)
    {
        for (int i = 0; i < rectTransform.childCount; i++)
        {
            if(i < nbOfCurrentLife)
            {
                rectTransform.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                rectTransform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}
