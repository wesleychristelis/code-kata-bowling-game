using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScoringClass;

namespace ScoringBowling
{
    [TestClass]
    public class ScoringBowlingTests
    {
        private Game _game;

        [TestInitialize]
        public void Setup()
        {
            _game = new Game();
        }

        [TestMethod]
        public void can_instantiate_game_with_one_player_and_score_is_zero()
        {
            //Given
            //See Test Initialise

            //When
            int players = 1;
            _game.Start(players);
            var score = _game.GetPlayer(0).CurrentScore();

            //Then
            Assert.AreEqual(0, score, "The score should be 0.");
        }

        [TestMethod]
        public void test_gutter_game()
        {
            //Given
            int player = 0;
            _game.Start(1);

            //When
            // a player instance 
            var currentPlayer = _game.GetPlayer(player);

            //and we roll a gutte game
            for (int i = 0; i < 20; i++)
            {
                currentPlayer.Roll(0);
            }

            //Then
            Assert.AreEqual(0, currentPlayer.CurrentScore(), "The CurrentScore should be 0.");
        }

        [TestMethod]
        public void test_normal_score_in_game_no_spare_or_strike()
        {
            //Given
            int player = 0;
            _game.Start(1);

            //When
            // a player instance 
            var currentPlayer = _game.GetPlayer(player);

            //and we roll a gutte game
            for (int i = 0; i < 20; i++)
            {
                currentPlayer.Roll(3);
            }

            //Then
            Assert.AreEqual(60, currentPlayer.CurrentScore(), "The CurrentScore should be 60.");
        }

        [TestMethod]
        public void test_normal_frame_in_game_with_spare()
        {
            //Given
            int player = 0;
            _game.Start(1);

            //When
            // a player instance 
            var currentPlayer = _game.GetPlayer(player);

            currentPlayer.Roll(5);
            currentPlayer.Roll(5);

            //Then 
            Assert.IsTrue(currentPlayer.HasSpare(), "Player has spare");
        }

        [TestMethod]
        public void test_normal_frame_in_game_with_strike()
        {
            //Given
            int player = 0;
            _game.Start(1);

            //When
            // a player instance 
            var currentPlayer = _game.GetPlayer(player);

            currentPlayer.Roll(10);

            //Then 
            Assert.IsTrue(currentPlayer.HasStrikeOnPreviousFrame(), "Player has strike");
        }

        [TestMethod]
        public void test_has_spare_score()
        {
            //Given
            int player = 0;
            _game.Start(1);

            //When
            // a player instance 
            var currentPlayer = _game.GetPlayer(player);

            currentPlayer.Roll(5);
            currentPlayer.Roll(5);
            //Then 
            Assert.AreEqual(10, currentPlayer.CurrentScore(), "Player has spare score of 10");
            Assert.IsTrue(currentPlayer.HasSpare(), "Player has a spare");
            Assert.AreEqual(1, currentPlayer.CurrentFrameIndex(), "After 2 rolls frame should be 1 (ie Frame 2)");

            //And 
            currentPlayer.Roll(3);
            Assert.AreEqual(16, currentPlayer.CurrentScore(), "Player has spare score of 16");
            Assert.AreEqual(13, currentPlayer.Frames[currentPlayer.CurrentFrameIndex()-1].FrameScore, "After 3 rolls frame should be 1 (ie Frame 2)");
        }

        [TestMethod]
        public void test_frame_overflow()
        {
            //Given
            int player = 0;
            _game.Start(1);

            //When
            // a player instance rolls 21 times with a score less than 10
            var currentPlayer = _game.GetPlayer(player);

            for (int i = 0; i < 21; i++)
            {
                currentPlayer.Roll(3);
            }

            //Then
            //currentframe should not be more than 10
            Assert.AreEqual(9, currentPlayer.CurrentFrameIndex(), "After 21 rolls frame should be 9 (ie Frame 10)");
        }

        [TestMethod]
        public void test_is_last_frame_strike()
        {
            //Given
            int player = 0;
            _game.Start(1);

            //When
            // a player instance rolls 12 times with a score of 10
            var currentPlayer = _game.GetPlayer(player);

            for (int i = 0; i < 12; i++)
            {
                currentPlayer.Roll(10);
            }

            //Then
            //currentframe should not be more than 10
            Assert.AreEqual(300, currentPlayer.CurrentScore(), "Perfect game score 300");
        }

        [TestMethod]
        public void acceptance_heart_breaker()
        {
            //Given
            int player = 0;
            _game.Start(1);

            //When
            // a player instance rolls 12 times with a score of 10
            var currentPlayer = _game.GetPlayer(player);

            for (int i = 0; i < 9; i++)
            {
                currentPlayer.Roll(10);
            }

            currentPlayer.Roll(5);
            currentPlayer.Roll(5);

            currentPlayer.Roll(3);

            //Then
            //currentframe should not be more than 10
            Assert.AreEqual(283, currentPlayer.CurrentScore(), "Perfect heart break score 283");
        }

        [TestMethod]
        public void acceptance_all_spares()
        {
            //Given
            int player = 0;
            _game.Start(1);

            //When
            // a player instance rolls 12 times with a score of 10
            var currentPlayer = _game.GetPlayer(player);

            for (int i = 0; i < 21; i++)
            {
                currentPlayer.Roll(5);
            }

            //Then
            //currentframe should not be more than 10
            Assert.AreEqual(150, currentPlayer.CurrentScore(), "All spare score 150");
        }

        [TestMethod]
        public void acceptance_midway_score()
        {
            //Given
            int player = 0;
            _game.Start(1);

            //When
            // a player instance rolls 12 times with a score of 10
            var currentPlayer = _game.GetPlayer(player);

            for (int i = 0; i < 10; i++)
            {
                if (i % 2 == 0)
                {
                    currentPlayer.Roll(4);
                }
                else
                {
                    currentPlayer.Roll(5);
                }
            }

            //Then
            //currentframe should not be more than 10
            Assert.AreEqual(45, currentPlayer.CurrentScore(), "Midway score 45");
        }
    }
}
