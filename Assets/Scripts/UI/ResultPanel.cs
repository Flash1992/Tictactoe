using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Tictactoe
{
	public class ResultPanel : BasePanel
	{
		public Text resultTxt;
		public Button restartBtn;
		public Button backBtn;

		private void OnEnable()
		{
			this.restartBtn.onClick.AddListener(RestartBtnHandler);
			this.backBtn.onClick.AddListener(BackBtnHandler);
		}

		private void OnDisable()
		{
			this.restartBtn.onClick.RemoveListener(RestartBtnHandler);
			this.backBtn.onClick.AddListener(BackBtnHandler);
		}

		private void RestartBtnHandler()
		{
			PanelMgr.Inst().OpenGamePanel();
		}

		private void BackBtnHandler()
		{
			PanelMgr.Inst().OpenStartPanel();
		}

		public void SetResult(bool isWin)
		{
			if (isWin)
				resultTxt.text = "You Win!";
			else
				resultTxt.text = "You Lose...";
		}	
	}
}