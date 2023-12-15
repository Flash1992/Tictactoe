using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Tictactoe
{
	public class StartPanel : BasePanel
	{
		public Button startBtn;

		private void OnEnable()
		{
			this.startBtn.onClick.AddListener(StartBtnHandler);
		}

		private void OnDisable()
		{
			this.startBtn.onClick.RemoveListener(StartBtnHandler);
		}

		private void StartBtnHandler()
		{
			PanelMgr.Inst().OpenGamePanel();
		}
	}
}