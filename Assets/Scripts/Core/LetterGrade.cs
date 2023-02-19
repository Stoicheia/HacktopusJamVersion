using UnityEngine;

namespace Minigame.Games.Core
{
    [CreateAssetMenu(fileName = "Grade", menuName = "Grade", order = 0)]
    public class LetterGrade : ScriptableObject
    {
        public string Text;
        public int MinTimeSeconds;
        public string Description;
        public Color Color;
    }
}