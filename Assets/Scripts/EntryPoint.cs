using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tictactoe
{
	public class EntryPoint : MonoBehaviour
	{
		public StartPanel startPanel;
		public GamePanel gamePanel;
		public ResultPanel resultPanel;

		private void Start()
		{
			PanelMgr.Inst().SetEntryPoint(this);
			PanelMgr.Inst().OpenStartPanel();
		}
	}
}