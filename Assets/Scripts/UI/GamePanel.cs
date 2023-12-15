using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Tictactoe
{
	public enum GameState
	{
		Playing = 1,
		Finish = 2,
	}

	public enum ChessState
	{
		None = 0,
		Player = 1,
		Robot = 2,
	}

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

		private void OnEnable()
		{
			Button[] gridList = gridCont.GetComponentsInChildren<Button>();
			foreach (var grid in gridList)
			{
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
			if (_gameState == GameState.Finish)
				return;

			Image chess;
			string chessName = gridName.Replace("Grid", "Chess");
			_chessDict.TryGetValue(chessName, out chess);

			if (chess == null)
				return;

			ChessState curState = _chessState;

			if (curState == ChessState.Player)
			{
				chess.enabled = true;
				chess.sprite = playerChessRes;
				_chessState = ChessState.Robot;
			}
			else if (curState == ChessState.Robot)
			{
				chess.enabled = true;
				chess.sprite = robotChessRes;
				_chessState = ChessState.Player;
			}
			else
			{
				Debug.LogError("错误的棋子状态: " + curState);
				return;
			}

			_chessStateDict[chessName] = curState;

			if (CheckLinkLine(curState))
			{
				_gameState = GameState.Finish;

				bool isWin = curState == ChessState.Player;
				PanelMgr.Inst().OpenResultPanel(isWin);
			}
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