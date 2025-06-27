using UnityEngine;

public class Bootstrapper : MonoBehaviour
{
    [SerializeField] private GameObject inputHandlerPrefab;

    private void Awake()
    {
        if (InputHandler.Instance == null)
        {
            var inputHandler = Instantiate(inputHandlerPrefab);
            inputHandler.name = "InputHandlerBootstrapper";
        }
    }
}
