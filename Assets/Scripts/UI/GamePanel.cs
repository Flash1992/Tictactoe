using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Tictactoe
{
	public class GamePanel : BasePanel
	{
		public GridLayoutGroup gridCont;
		public GridLayoutGroup chessCont;

		public Sprite playerChessRes;
		public Sprite robotChessRes;

		private Dictionary<string, Image> _chessDict = new Dictionary<string, Image>();
		private Dictionary<string, ChessState> _chessStateDict = new Dictionary<string, ChessState>();

		private GameState _gameState;
		private ChessState _chessState;

		private byte _stepIdx;
		private readonly byte MAX_STEP = 9;

		private void OnEnable()
		{
			Button[] gridList = gridCont.GetComponentsInChildren<Button>();
			foreach (var grid in gridList)
			{
				grid.enabled = true;
				grid.onClick.AddListener(() => GridHandler(grid.name));
			}

			Image[] chessList = chessCont.GetComponentsInChildren<Image>();
			foreach (var chess in chessList)
			{
				chess.enabled = false;

				_chessDict.Add(chess.name, chess);
				_chessStateDict.Add(chess.name, ChessState.None);
			}

			_gameState = GameState.Playing;
			_chessState = ChessState.Player;

			_stepIdx = 0;
		}

		private void OnDisable()
		{
			Button[] gridList = gridCont.GetComponentsInChildren<Button>();
			foreach (var grid in gridList)
			{
				grid.onClick.RemoveAllListeners();
			}

			_chessDict.Clear();
			_chessStateDict.Clear();
		}

		private void GridHandler(string gridName)
		{
			if (_gameState != GameState.Playing)
				return;
			if (_chessState != ChessState.Player)
				return;

			string chessName = gridName.Replace("Grid", "Chess");
			DoChess(chessName);
		}

		private async void DoChess(string chessName)
		{
			Image chess;
			_chessDict.TryGetValue(chessName, out chess);

			if (chess == null)
				return;

			ChessState lastState = _chessState;

			if (lastState == ChessState.Player)
			{
				chess.enabled = true;
				chess.sprite = playerChessRes;
				_chessState = ChessState.Robot;
			}
			else if (lastState == ChessState.Robot)
			{
				chess.enabled = true;
				chess.sprite = robotChessRes;
				_chessState = ChessState.Player;
			}
			else
			{
				Debug.LogError("错误的棋子状态: " + lastState);
				return;
			}

			_chessStateDict[chessName] = lastState;

			if (CheckLinkLine(lastState))
			{
				var result = lastState == ChessState.Player ? GameResult.Win : GameResult.Lose;
				FinishChess(result);
				return;
			}

			_stepIdx++;
			if (_stepIdx >= MAX_STEP)
			{
				FinishChess(GameResult.Peace);
				return;
			}

			if (_chessState == ChessState.Robot)
			{
				await Task.Delay(500);
				DoChess(CalRobotChessName());
			}
		}

		private string CalRobotChessName()
		{
			List<string> list = new List<string>();

			foreach (var val in _chessStateDict)
			{
				if (val.Value == ChessState.None)
				{
					list.Add(val.Key);
				}
			}

			if (list.Count > 0)
			{
				return list[Random.Range(0, list.Count)];
			}

			return "";
		}

		private async void FinishChess(GameResult result)
		{
			_gameState = GameState.Finish;

			Button[] gridList = gridCont.GetComponentsInChildren<Button>();
			foreach (var grid in gridList)
			{
				grid.enabled = false;
			}

			await Task.Delay(500);
			PanelMgr.Inst().OpenResultPanel(result);
		}

		private bool CheckLinkLine(ChessState state)
		{
			var d = _chessStateDict;

			// 横向
			if (d["Chess_0_0"] == state && d["Chess_0_1"] == state && d["Chess_0_2"] == state)
				return true;
			if (d["Chess_1_0"] == state && d["Chess_1_1"] == state && d["Chess_1_2"] == state)
				return true;
			if (d["Chess_2_0"] == state && d["Chess_2_1"] == state && d["Chess_2_2"] == state)
				return true;
			// 纵向
			if (d["Chess_0_0"] == state && d["Chess_1_0"] == state && d["Chess_2_0"] == state)
				return true;
			if (d["Chess_0_1"] == state && d["Chess_1_1"] == state && d["Chess_2_1"] == state)
				return true;
			if (d["Chess_0_2"] == state && d["Chess_1_2"] == state && d["Chess_2_2"] == state)
				return true;
			// 斜向
			if (d["Chess_0_0"] == state && d["Chess_1_1"] == state && d["Chess_2_2"] == state)
				return true;
			if (d["Chess_0_2"] == state && d["Chess_1_1"] == state && d["Chess_2_0"] == state)
				return true;

			return false;
		}
	}
}