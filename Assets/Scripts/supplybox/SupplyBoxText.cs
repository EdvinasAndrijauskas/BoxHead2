using TMPro;
using UnityEngine;

namespace supplybox
{
    public class SupplyBoxText : MonoBehaviour
    {
        private TextMeshPro _textMeshPro;
        private float _disappearTime;
        private Color _textColor;

        private void Awake()
        {
            _textMeshPro = transform.GetComponent<TextMeshPro>();
            gameObject.layer = LayerMask.NameToLayer ("Ignore Raycast");

        }

        public void Setup(string supplyText)
        {
            string ammo = "Ammo";
            _textMeshPro.text = supplyText + " " + ammo;
            _textColor = _textMeshPro.color;
            _disappearTime = 0.3f;
        
        }

        private void Update()
        {
            transform.position += new Vector3(0, 15 ) * Time.deltaTime;
            _disappearTime -= Time.deltaTime;
            if (_disappearTime < 0)
            {
                float disappearSpeed = 3f;
                _textColor.a -= disappearSpeed * Time.deltaTime;
                _textMeshPro.color = _textColor;
                if (_textColor.a < 0)
                {
                    Destroy(gameObject);

                }
            }
        }
    
    
    }
}

