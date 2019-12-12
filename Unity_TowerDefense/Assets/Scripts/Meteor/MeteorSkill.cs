using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MeteorSkill : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float range;
    [SerializeField] private float resetTime;
    
    [SerializeField] private GameObject meteor;
    [SerializeField] private GameObject possibleParticle;

    [SerializeField] private Button skillButton;
    
    private Vector3 _destination;
    private bool _isSelected;
    private bool _canFire;
    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
        skillButton.onClick.AddListener(Select);
        
        resetTime -= Game.Instance._meteorCountDownDecrease;
        damage *= (1 + Game.Instance._meteorDamageIncrease);
        range *= (1 + Game.Instance._meteorRangeIncrease);
        
        possibleParticle.SetActive(false);
    }

    private void Update()
    {
        //If skill is selected make a raycast to fire destination
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
                possibleParticle.SetActive(false);
            }

            if (Input.GetMouseButtonDown(0))
            {
                Fire();
            }

            if (Input.GetMouseButtonDown(1))
            {
                _isSelected = false;
                skillButton.interactable = true;
                possibleParticle.SetActive(false);
            }

            //Activate particles
            if (_canFire)
            {
                possibleParticle.SetActive(true);
                possibleParticle.transform.position = _destination + Vector3.up * 0.3f;
            }
            else
            {
                possibleParticle.SetActive(false);
            }
        }
    }

    private void Select()
    {
        _isSelected = true;
        skillButton.interactable = false;
        
        //TODO: sound of selection skill
    }

    private void Fire()
    {
        if (_canFire)
        {
            skillButton.interactable = false;
            
            _isSelected = false;
            _canFire = false;

            //Start cooldown for meteor skill
            StartCoroutine(Cooldown());

            //Instantiate meteor prefab and set values to it
            Meteor m = Instantiate(meteor, _destination + Vector3.up * 35, Quaternion.identity).GetComponent<Meteor>();
            m.SetValues(range * (1 + Game.Instance._meteorRangeIncrease), damage * (1 + Game.Instance._meteorDamageIncrease));

            _destination = Vector3.zero;
            possibleParticle.SetActive(false);
        }
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(resetTime);
        skillButton.interactable = true;
        
        //TODO: sound of cooldown ends
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(_destination,range);
    }
}
