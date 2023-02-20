using System;
using System.Collections;
using LootLocker.Requests;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Minigame.Games.Core
{
    public class EndScreen : MonoBehaviour
    {
        [SerializeField] private Grader _grader;
        private float _finalTime;
        public float FinalTime
        {
            get => _finalTime;
            set
            {
                _finalTime = value;
                _timeField.text = $"{(int)_finalTime / 60:00}:{(int)_finalTime % 60:00}";
                var grade = _grader.CalculateGrade((int)_finalTime);
                _gradeField.text = grade.Text;
                _gradeDescriptionField.text = grade.Description;
                _gradeField.color = grade.Color;
                _successSubmit.gameObject.SetActive(false);
                _failSubmit.gameObject.SetActive(false);
                _submitButton.enabled = true;
            } 
        }
        private float _bestFinalTime;
        public float BestFinalTime
        {
            get => _bestFinalTime;
            set
            {
                _bestFinalTime = value;
                _bestTimeField.text = $"{_bestFinalTime / 60:00}:{_bestFinalTime % 60:00}";
            } 
        }
        [SerializeField] private TextMeshProUGUI _timeField;
        [SerializeField] private TextMeshProUGUI _bestTimeField;
        [SerializeField] private TextMeshProUGUI _gradeField;
        [SerializeField] private TextMeshProUGUI _gradeDescriptionField;

        [SerializeField] private TMP_InputField _nameField;
        [SerializeField] private RectTransform _successSubmit;
        [SerializeField] private RectTransform _failSubmit;
        [SerializeField] private Button _submitButton;

        public void SubmitScore()
        {
            StartCoroutine(SubmitScoreSequence());
        }
        private IEnumerator SubmitScoreSequence()
        {
            bool done = false;
            LootLockerSDKManager.SetPlayerName(_nameField.text, (response) =>
            {
                done = true;
            });
            yield return new WaitWhile(() => done == false);
            string playerID = PlayerPrefs.GetString("PlayerID"); 
            LootLockerSDKManager.SubmitScore(playerID, (int)(_finalTime*100), "time", (r) =>
            {
                if (r.statusCode == 200)
                {
                    _successSubmit.gameObject.SetActive(true);
                }
                else
                {
                    _failSubmit.gameObject.SetActive(true);
                }

                _submitButton.enabled = false;
            });
        }
    }
}