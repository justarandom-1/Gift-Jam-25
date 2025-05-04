using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    [SerializeField] GameObject health;
    private RectTransform healthbar;
    private int state;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;

        healthbar = health.GetComponent<RectTransform>();
    }

    public void updateHealth()
    {
        if(Player.instance != null){
            float x = 357.5f - Player.instance.getHealth() * (35 + 357.5f);
            healthbar.offsetMin = new Vector2(x, healthbar.offsetMin.y);
            healthbar.offsetMax = new Vector2(-1 * x, healthbar.offsetMax.y);

            healthbar.gameObject.GetComponent<Animator>().Play("flash");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static float VectorToAngle(Vector2 v)
    {
        v.Normalize();
        float a = Mathf.Asin(v.y) * Mathf.Rad2Deg;
        if(v.x < 0)
            a = 180 - a;
        return a;
    }
}
