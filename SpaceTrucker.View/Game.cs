﻿using SpaceTrucker.ViewModel;
using System;
using System.Threading;

namespace SpaceTrucker.View
{
	class Game
	{
		private DisplayManager display;
		private UserInput input;
		private EventBroadcaster eventBroadcaster;
		private GameManager gm;

		private bool gameOver = false;

		private MenuState currentMenuState = MenuState.MainMenu;
		private GameState currentGameState = GameState.ApplicationOpen;

		public Game()
		{
            Thread consoleResizeListner = new Thread(ConsoleResizeListner);
            eventBroadcaster = new EventBroadcaster();
			input = new UserInput();
			display = new DisplayManager(eventBroadcaster);
			gm = new GameManager(eventBroadcaster);

			eventBroadcaster.MenuState += ChangeMenuState;
			eventBroadcaster.GameState += ChangeGameState;

            consoleResizeListner.Start();
        }

		public void Run()
		{
			var action = ActionType.Invalid;

			while (!gameOver)
			{
				do
				{
					action = GetUserInput(currentMenuState);

				} while (action == ActionType.Invalid);

				gm.ActionUserInput(action);
				gm.GameStateCheck();
			}
		}

		private ActionType GetUserInput(MenuState currentGameState)
		{
			switch (currentGameState)
			{
				case MenuState.MainMenu:
				case MenuState.StartMenu:
				case MenuState.SaveConfirmationMenu:
				case MenuState.QuitMenu:
				case MenuState.TransactionConfirmationMenu:
				case MenuState.TravelConfirmationMenu:
                case MenuState.UpgradeConfirmationMenu:
					return input.AwaitUserKeyResponse(InputRequestType.MenuOnly);
				case MenuState.MarketMenu:
				case MenuState.TransactionMenu:
				case MenuState.GameMenu:
				case MenuState.TravelMenu:
					return input.AwaitUserKeyResponse(InputRequestType.FullSelectionInput);
				default:
					return ActionType.Invalid;
			}
		}

		private void ChangeMenuState(object sender, MenuState nextMenuState)
		{
			currentMenuState = nextMenuState;
		}

		private void ChangeGameState(object sender, GameState nextGameState)
		{
			if (nextGameState == GameState.GameOver)
			{
				gameOver = true;
			}
			currentGameState = nextGameState;
		}

        private void ConsoleResizeListner()
        {
            int height = Console.WindowHeight;
            int width = Console.WindowWidth;
            while (true)
            {
                if (height != Console.WindowHeight || width != Console.WindowWidth)
                {
                    try
                    {
                        Console.SetWindowSize(width, height);
                        display.Refresh();
                    }
                    catch
                    {
                        //Window size too small
                    }
                }
                Thread.Sleep(20);
            }
        }
    }
}
