using UnityEngine;

public class SeelScreen : MonoBehaviour
{
    public GameObject Parent;
    public GameObject seelContainer;

    public void GenerateSeelContainer()
    {
        for (int i = 0; i < SeelManager.seelCount; i++)
        {
            Instantiate(seelContainer, Parent.transform);
        }
    }

    void Start()
    {
        GenerateSeelContainer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
