using UnityEngine;

namespace Michsky.UI.ModernUIPack
{
    public class PBFilled : MonoBehaviour
    {
        public ProgressBar proggresBar;

        [Header("SETTINGS")]
        public Animator barAnimatior;
        [Range(0, 100)] public int transitionAfter = 50;

        void Update()
        {
            if (proggresBar.currentPercent >= transitionAfter)
                barAnimatior.Play("Radial PB Filled");

            if (proggresBar.currentPercent <= transitionAfter)
                barAnimatior.Play("Radial PB Empty");
        }
    }
}