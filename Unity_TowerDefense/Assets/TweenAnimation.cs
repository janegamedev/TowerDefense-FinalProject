using UnityEngine;
using Random = UnityEngine.Random;

public class TweenAnimation : MonoBehaviour
{
   private GameObject _objectToAnimate;
   public int minAmount;
   public int maxAmount;

   private void Start()
   {
      _objectToAnimate = this.gameObject;
      LeanTween.moveY(_objectToAnimate, _objectToAnimate.transform.position.y + Random.Range(minAmount, maxAmount),
         Random.Range(0.5f, 1.5f)).setEaseLinear().setLoopPingPong();
   }
}
