using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScoringClass
{
    public class Player
    {
        private int _currentFrameIndex;
        private readonly List<Frame> _frames;
        private Frame _spareFrame;

        public bool IsLastFrameComplete { get; set; }

        public Player()
        {
            _frames = new List<Frame>();
            _currentFrameIndex = 0;
            _spareFrame = new Frame();
        }

        public List<Frame> Frames
        {
            get
            {
                return _frames;
            }
        }

        public int CurrentScore()
        {
            return Frames.Sum(s => s.FrameScore);
        }

        public void Roll(int score)
        {
            if (!IsLastFrameComplete)
            {
                var frame = _frames.ElementAtOrDefault(_currentFrameIndex);

                if (frame == null)
                {
                    _frames.Add(new Frame());
                }

                var throwResult = _frames[_currentFrameIndex].Throw(score);

                //Logic to add extras from spare or strike
                if (HasStrikeOnPreviousFrame())
                {
                    _frames[_currentFrameIndex - 1].AddExtras(throwResult.PinsHit);
                    if (HasStrikeOnPreviousPreviousFrame())
                    {
                        _frames[_currentFrameIndex - 2].AddExtras(throwResult.PinsHit);
                    }
                }
                else if (HasSpare() && throwResult.ThrowIndex == 1)
                {
                    _frames[_currentFrameIndex - 1].AddExtras(throwResult.PinsHit);
                }

                // Current frame logic
                if (!_frames[_currentFrameIndex].IsStrike())
                {
                    if (throwResult.ThrowIndex % 2 == 0)
                        AdvanceFrame();
                }
                else
                {
                    AdvanceFrame();
                }
            }
            else
            {
                var throwResult = _spareFrame.Throw(score);

                _frames[_currentFrameIndex].AddExtras(throwResult.PinsHit);

                if (HasStrikeOnPreviousFrame() && throwResult.ThrowIndex  ==1 )
                {
                    _frames[_currentFrameIndex-1].AddExtras(10);
                }
            }
        }

        private void AdvanceFrame()
        {
            if (_currentFrameIndex < 9)
                _currentFrameIndex++;
            else
                IsLastFrameComplete = true;
        }

        public bool HasSpare()
        {
            if (_currentFrameIndex > 0)
            { 
                return _frames[_currentFrameIndex- 1].IsSpare();
            }

            return false;
        }

        public bool HasStrikeOnPreviousFrame()
        {
            if (_currentFrameIndex > 0)
            {
                return _frames[_currentFrameIndex - 1].IsStrike();
            }

            return false;
        }

        public bool HasStrikeOnPreviousPreviousFrame()
        {
            if (_currentFrameIndex > 1)
            {
                return _frames[_currentFrameIndex - 2].IsStrike();
            }

            return false;
        }

        public int CurrentFrameIndex()
        {
            return _currentFrameIndex;
        }

        public bool IsCurrentFrameComplete()
        {
            return _frames[_currentFrameIndex].IsFrameComplete();
        }
    }
}
