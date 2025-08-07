using UnityEngine;
using UUP.Utilities.GameObjectUtils;

public class ObjectController : MonoBehaviour
{
    public GameObject[] GameObjects;

    public void ToggleAll()
    {
        ArrayUtils.ToggleAll(GameObjects);
    }

    public void ActivateSpecificObject(Component sender, object data)
    {
        if(data is int)
        {
            int index = (int) data;
            ArrayUtils.ToggleSingle(GameObjects, index);
        }

        else
        {
            Debug.LogWarning("Wrong type of GameEvent-data!");
        }

    }
}
