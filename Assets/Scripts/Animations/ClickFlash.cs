using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Animations
{
    public class ClickFlash : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        public void Enter()
        {
            StartCoroutine(PlaySequence(1));
        }

        private IEnumerator PlaySequence(float s)
        {
            _animator.Play("FastBlink");
            yield return new WaitForSeconds(s);
            SceneManager.LoadScene(1);
        }
    }
}