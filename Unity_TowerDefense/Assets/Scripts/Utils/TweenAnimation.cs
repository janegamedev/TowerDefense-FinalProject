using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class TweenAnimation : MonoBehaviour
{
   private GameObject _objectToAnimate;

   public bool fx;
   public GameObject fxPrefab;
   private float _delay;
   public float probability;

   public bool move;
   public int minAmount;
   public int maxAmount;

   public bool rotate;
   public int rotationSpeed;
   public int direction;

   private void Start()
   {
      _objectToAnimate = this.gameObject;

      if (fx)
      {
         float r = Random.value;
         if (r <= probability)
         {
            _delay = Random.Range(0f, 2f);
            StartCoroutine(Delay());
         }
         else
         {
            fxPrefab.SetActive(false);
         }
      }

      if (move)
      {
         LeanTween.moveY(_objectToAnimate, _objectToAnimate.transform.position.y + Random.Range(minAmount, maxAmount),
            Random.Range(1f, 2.5f)).setEaseLinear().setLoopPingPong();
      }
      
      if (rotate)
      {
         LeanTween.rotateAround(_objectToAnimate, Vector3.up,  direction * 360, rotationSpeed).setLoopClamp();
      }
   }

   IEnumerator Delay()
   {
      yield return new WaitForSeconds(_delay);
      fxPrefab.SetActive(true);
   }
}
