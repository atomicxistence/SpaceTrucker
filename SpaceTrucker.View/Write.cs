﻿using System;

namespace SpaceTrucker.View
{
	static class Write
	{
		public static ConsoleColor ColorDefaultFG => ConsoleColor.White;
		public static ConsoleColor ColorDefaultBG => ConsoleColor.Black;
		public static ConsoleColor ColorSurfaceFG => ConsoleColor.Black;
		public static ConsoleColor ColorSurfaceBG => ConsoleColor.Gray;
		public static ConsoleColor ColorBevelBG => ConsoleColor.DarkGray;
		public static ConsoleColor ColorDisplayFG => ConsoleColor.Green;
		public static ConsoleColor ColorDisplayBG => ConsoleColor.DarkGreen;

		public static void EmptySpace(int width)
		{
			for (int i = 0; i < width; i++)
			{
				Console.Write(" ");
			}
		}
	}
}