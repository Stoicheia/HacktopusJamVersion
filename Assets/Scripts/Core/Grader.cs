using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Minigame.Games.Core
{
    public class Grader : MonoBehaviour
    {
        [SerializeField] private List<LetterGrade> _grades;

        public LetterGrade CalculateGrade(int time)
        {
            _grades.Sort((x, y) => x.MinTimeSeconds - y.MinTimeSeconds);
            foreach (var g in _grades)
            {
                if (time < g.MinTimeSeconds) return g;
            }

            return _grades.Last();
        }
    }
}