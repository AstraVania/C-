using System;
using System.Collections.Generic;
namespace Project
{

	internal class Tic_Tac
	{
		static char O = 'O', X = 'X';
		public static void Main(string[] args)
		{
			char[,] Board = new char[,] { { '-', '-', '-' }, { '-', '-', '-' }, { '-', '-', '-' } };
			int rowW = 1, colW = 2, COL, ROW ,END = 0;
			while (isMovesLeft(Board))
			{
				while (true)
				{
					ROW = ScanMove(rowW);
					COL = ScanMove(colW);
					if (Board[ROW, COL] != '-')
						Console.WriteLine("THIS SLOT IS CLIMBED");
					else
					{
						Board[ROW, COL] = X;
						END++;
						break;
					}
				}
				
				PrintBoard(Board);
				if (END == 5)
				{
					Console.WriteLine("IT'S A DRAW");
					System.Threading.Thread.Sleep(3000);
					break;
				}
				Move bestMove = findBestMove(Board);
				Console.WriteLine("My Move is ROW: {0} COL:{1}\n\n", bestMove.row + 1, bestMove.col + 1);
				Board[bestMove.row, bestMove.col] = O;
			
				PrintBoard(Board);
				if (evaluate(Board) == 10)
				{
					Console.WriteLine("I WIN GOOD GAME");
					System.Threading.Thread.Sleep(3000);
					break ;
				}
			}

		}

		public static void PrintBoard(char[,] Board)
		{
			for (int i = 0; i < 3; i++)
			{
				Console.WriteLine("|" + Board[i, 0] + "|" + Board[i, 1] + "|" + Board[i, 2] + "|");
				Console.WriteLine("-------");
			}

		}
		public static int ScanMove(int T)
		{
			string R = "Row", C = "Column", NOW;
			int P;
			if (T == 1)
				NOW = R;
			else NOW = C;
			do
			{
				Console.WriteLine("Please enter " + NOW + " number from 1 to 3");
				bool test = int.TryParse(Console.ReadLine(), out P);
				if (test == false || !(P > 0 && P < 4))
					Console.WriteLine("There is no such " + NOW + " try again");
			}
			while (!(P > 0 && P < 4));
			P--;
			return P;
		}
		class Move
		{
			public int row, col;
		};
		static Boolean isMovesLeft(char[,] board)
		{
			for (int i = 0; i < 3; i++)
				for (int j = 0; j < 3; j++)
					if (board[i, j] == '-')
						return true;
			return false;
		}

		
		static int evaluate(char[,] b)
		{
			for (int row = 0; row < 3; row++)
			{
				if (b[row, 0] == b[row, 1] &&
					b[row, 1] == b[row, 2])
				{
					if (b[row, 0] == O)
						return +10;
					else if (b[row, 0] == X)
						return -10;
				}
			}

			for (int col = 0; col < 3; col++)
			{
				if (b[0, col] == b[1, col] &&
					b[1, col] == b[2, col])
				{
					if (b[0, col] == O)
						return +10;

					else if (b[0, col] == X)
						return -10;
				}
			}
			if (b[0, 0] == b[1, 1] && b[1, 1] == b[2, 2])
			{
				if (b[0, 0] == O)
					return +10;
				else if (b[0, 0] == X)
					return -10;
			}

			if (b[0, 2] == b[1, 1] && b[1, 1] == b[2, 0])
			{
				if (b[0, 2] == O)
					return +10;
				else if (b[0, 2] == X)
					return -10;
			}
			return 0;
		}
		static int minimax(char[,] board,
						int depth, Boolean isMax)
		{
			int score = evaluate(board);
			if (score == 10)
				return score;
			if (score == -10)
				return score;
			if (isMovesLeft(board) == false)
				return 0;
			if (isMax)
			{
				int best = -1000;

				for (int i = 0; i < 3; i++)
				{
					for (int j = 0; j < 3; j++)
					{
						if (board[i, j] == '-')
						{
							board[i, j] = O;
							best = Math.Max(best, minimax(board,
											depth + 1, !isMax));
							board[i, j] = '-';
						}
					}
				}
				return best;
			}
			else
			{
				int best = 1000;
				for (int i = 0; i < 3; i++)
				{
					for (int j = 0; j < 3; j++)
					{
						if (board[i, j] == '-')
						{
							
							board[i, j] = X;
							best = Math.Min(best, minimax(board,
											depth + 1, !isMax));

							board[i, j] = '-';
						}
					}
				}
				return best;
			}
		}

		static Move findBestMove(char[,] board)
		{
			int bestVal = -1000;
			Move bestMove = new Move();
			bestMove.row = -1;
			bestMove.col = -1;

			for (int i = 0; i < 3; i++)
			{
				for (int j = 0; j < 3; j++)
				{
					if (board[i, j] == '-')
					{
						board[i, j] = O;
						int moveVal = minimax(board, 0, false);
						board[i, j] = '-';
						if (moveVal > bestVal)
						{
							bestMove.row = i;
							bestMove.col = j;
							bestVal = moveVal;
						}
					}
				}
			}
			return bestMove;
		}
	}
}



