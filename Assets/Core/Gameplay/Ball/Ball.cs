using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Balls
{
    public class Ball : MonoBehaviour
    {
        public string BallType; // Set in Inspector or dynamically
        private bool _isMatched = false;

        public void MarkAsMatched()
        {
            //if (_isMatched) return;
            //_isMatched = true;
        }
    }
}