using System.Collections;
using UnityEngine;
using TMPro;

namespace UUP.UI.WindowManagement
{
    public class LogManager : MonoBehaviour
    {
        public TextMeshProUGUI tmpText;

        private string _defaultMessage = " ";

        private void Start()
        {
            tmpText.text = _defaultMessage;
        }

        public void ChangeLogMessage(Component sender, object data)
        {
            if(data is string)
            {
                tmpText.text = (string) data;
            }

            else
            {
                Debug.LogWarning("Wrong Game-Event format!");
            }
            
        } 
    }
}