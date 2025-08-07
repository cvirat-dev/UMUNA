using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UUP.UI
{
    public class ScrollbarValueController : MonoBehaviour
    {
        private Scrollbar _scrollBarComp;

        private void Awake()
        {
            _scrollBarComp = GetComponent<Scrollbar>();
        }

        public void ResetScrollBarValue()
        {
            _scrollBarComp.value = 1;
        }
    }
}