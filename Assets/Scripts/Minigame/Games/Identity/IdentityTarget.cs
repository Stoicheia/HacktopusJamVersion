using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Minigame.Games.Identity
{
    public class IdentityTarget : MonoBehaviour
    {
        [SerializeField] public List<IdentityElement> TargetElements;
        [SerializeField] public List<Image> PartsPlaces;

        public void Load(List<IdentityElement> elements)
        {
            TargetElements = elements;
            for (int i = 0; i < PartsPlaces.Count; i++)
            {
                PartsPlaces[i].sprite = TargetElements[i]._sprite;
            }
        }

        public float CheckCorrectness(List<IdentityElement> from)
        {
            float score = 0;
            for (int i = 0; i < from.Count; i++)
            {
                if (from[i].ID == TargetElements[i].ID)
                {
                    score += (float)1 / from.Count;
                }
            }

            return score;
        }
    }
}