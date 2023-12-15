namespace Tictactoe
{
	public class PanelMgr
	{
		private static PanelMgr _instance;

		public static PanelMgr Inst()
		{
			if (_instance == null)
			{
				_instance = new PanelMgr();
			}

			return _instance;
		}

		private EntryPoint _entryPoint;
		private BasePanel _curPanel;

		public void SetEntryPoint(EntryPoint e)
		{
			_entryPoint = e;
		}

		public void OpenStartPanel()
		{
			OpenPanel(_entryPoint.startPanel);
		}

		public void OpenGamePanel()
		{
			OpenPanel(_entryPoint.gamePanel);
		}

		public void OpenResultPanel(GameResult result)
		{
			OpenPanel(_entryPoint.resultPanel);
			_entryPoint.resultPanel.SetResult(result);
		}

		private void OpenPanel(BasePanel panel)
		{
			if (_curPanel)
				_curPanel.gameObject.SetActive(false);

			panel.gameObject.SetActive(true);
			_curPanel = panel;
		}
	}
}