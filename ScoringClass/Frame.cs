using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScoringClass
{
    public class Frame
    {
        private readonly Throw[] _throws = { new Throw(), new Throw() };
        private int _currentThrow;

        public int FrameScore { get; set; }

        public Frame()
        {
        }

        public Throw[] Throws
        {
            get
            {
                return _throws;
            }
        }

        public void AddExtras(int extras)
        {
            FrameScore += extras;
        }

        public bool IsSpare()
        {
            return (_throws[0].Pins + _throws[1].Pins) == 10;
        }

        public bool IsStrike()
        {
            return _throws[0].Pins == 10;
        }

        public bool IsFrameComplete()
        {
            return _currentThrow == 2;
        }

        public ThrowResult Throw(int score)
        {
            var pins = Throws[_currentThrow++].Hit(score);

            FrameScore += pins;

            return new ThrowResult { ThrowIndex = _currentThrow, PinsHit = pins };
        }
    }
}
