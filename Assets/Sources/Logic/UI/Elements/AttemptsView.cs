using TMPro;
using UnityEngine;

namespace Sources.Logic.UI.Elements
{
    public class AttemptsView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;

        public void SetValue(int value)
        {
            _text.text = value.ToString();
        }
    }
}