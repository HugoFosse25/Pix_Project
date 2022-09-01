using UnityEngine;

public class BulletSystem : MonoBehaviour
{
    public static BulletSystem instance;

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

        RefreshUI(5);
    }

    public void RefreshUI(int nbOfCurrentBullet)
    {
        for (int i = 0; i < rectTransform.childCount; i++)
        {
            if (i < nbOfCurrentBullet)
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
