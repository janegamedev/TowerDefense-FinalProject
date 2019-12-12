using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MeteorSkill : MonoBehaviour
{
    public int damage;
    public int range;

    public Button skillButton;
    public GameObject meteor;
    public float resetTime;
    private Vector3 _destination;
    private bool _isSelected;
    private bool _canFire;
    private Camera _camera;


    private Texture2D _texture2D;
    private Texture2D _progressTexture2D;
    private Color _color;
    private float _progress;
    private float _oldProgress;
    
    
    
    void Start()
    {
        _texture2D = skillButton.image.sprite.texture;
        _progressTexture2D = _texture2D;
        _camera = Camera.main;
        skillButton.onClick.AddListener(Select);
        resetTime -= Game.Instance._meteorCountDownDecrease;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isSelected)
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Road")))
            {
                _destination = hit.transform.position;
                _canFire = true;
            }
            else
            {
                _canFire = false;
            }

            if (Input.GetMouseButtonDown(0))
            {
                Fire();
            }

            if (Input.GetMouseButtonDown(1))
            {
                _isSelected = false;
                skillButton.interactable = true;
            }
        }
    }

    private void Select()
    {
        _isSelected = true;
        skillButton.interactable = false;
    }

    private void Fire()
    {
        if (_canFire)
        {
            skillButton.interactable = false;
            
            _isSelected = false;
            _canFire = false;
            StartCoroutine(Restart());

            Meteor m = Instantiate(meteor, _destination + Vector3.up * 150, Quaternion.identity).GetComponent<Meteor>();
            m.damage = damage * (1 + Game.Instance._meteorDamageIncrease);
            m.range = range * (1 + Game.Instance._meteorRangeIncrease);
            
            _destination = Vector3.zero;
        }
    }

    IEnumerator Restart()
    {
        yield return new WaitForSeconds(resetTime);
        skillButton.interactable = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(_destination,range);
    }
    
    /*
    private void ProgressUpdate( float progress,Color overlayColor)
    {
        var thisTex = new Texture2D(_texture2D.width, _texture2D.height);
        Vector2 centre = new Vector2(Mathf.Ceil(thisTex.width/2), Mathf.Ceil(thisTex.height/2));


        for (int y = 0; y < thisTex.height; y++)
        {
            for (int x = 0; x < thisTex.width; x++)
            {
                float angle = Mathf.Atan2(x-centre.x, y-centre.y)*Mathf.Rad2Deg;
                if (angle < 0)
                {
                    angle += 360;
                }

                Color pixColor = _texture2D.GetPixel(x, y);

                if (angle <= progress * 360)
                {
                    pixColor = new Color(pixColor.r, pixColor.g, pixColor.b, overlayColor.a);
                }

                thisTex.SetPixel(x, y, pixColor);
            }            
        }
        
        thisTex.Apply();
    }
    */

}
