namespace Tictactoe
{
	// 游戏状态
	public enum GameState
	{
		Playing = 1, // 进行中
		Finish = 2, // 已结束
	}

	// 棋子归属状态
	public enum ChessState
	{
		None = 0, // 无
		Player = 1, // 玩家
		Robot = 2, // 机器人
	}
	
	// 游戏结果
	public enum GameResult
	{
		Win = 1, // 胜利
		Lose = 2, // 失败
		Peace = 3, // 打平
	}
}