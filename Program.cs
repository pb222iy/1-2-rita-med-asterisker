using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assignment
{
	class Program
	{
		/// <summary>
		///		Maximum number of asterisks allowed in the diamond middle row.
		/// </summary>
		const int MAX_WIDTH = 79;

		public static void Main(string[] args)
		{
			do
			{
				// Read value from user
				byte width = readOddByte( Assignment.Resource.promptEnterWidth, MAX_WIDTH );

				// Render everything (life and universe come extra)
				renderDiamond( width );

			} while ( isContinuing() );

			// That's all, folks!
		}

		/// <summary>
		///		Read value from user to determine how large the diamond should be
		/// </summary>
		/// <param name="prompt">text prompt to display with query</param>
		/// <param name="maxValue">max allowed value</param>
		/// <returns>user's chosen middle row width</returns>
		private static byte readOddByte( string prompt = null, byte maxValue = 255 )
		{
			byte value = 0;

			while ( true )
			{
				try
				{
					Console.Write( prompt, maxValue);

					value = byte.Parse( Console.ReadLine() );

					// Ensure value is at least 1; 0 is not an odd number (though there are those who argue it's not even, either)
					if ( value < 1 )
					{
						printMessage( Assignment.Resource.errorMustEqualOrGreaterOne, true );
						continue;
					}

					// Value may not exceed our chosen max value
					if ( value > maxValue )
					{
						printMessage( Assignment.Resource.errorValueOverMax, true );
						continue;
					}

					// Value must also be odd
					if ( ( value & 1 ) == 0 )
					{
						printMessage( Assignment.Resource.errorValueNotOdd, true );
						continue;
					}

					// Everything fine? Great.
					break;
				}
				catch ( FormatException )
				{
					printMessage( Assignment.Resource.errorFormatException, true );
				}
				catch ( OverflowException )
				{
					printMessage( Assignment.Resource.errorOverflowException, true );
				}
			}

			// Clearline after query
			Console.WriteLine();

			// We have a winner
			return value;
		}

		/// <summary>
		///		Base method for diamond rendering.
		/// </summary>
		/// <param name="maxCount">width of diamond middle row</param>
		private static void renderDiamond( byte maxCount )
		{
			//Console.BackgroundColor = ConsoleColor.DarkBlue;

			// Draw top half of diamond, including middle row
			for ( int i = 1; i <= maxCount; i += 2 )
			{
				renderRow( maxCount, i );
			}

			// Draw bottom part of diamond
			for ( int i = maxCount - 2; i > 0; i -= 2 )
			{
				renderRow( maxCount, i );
			}

			//Console.ResetColor();
		}

		/// <summary>
		///		Render one row of the diamond.
		/// </summary>
		/// <param name="maxCount">total width of diamond</param>
		/// <param name="asteriskCount">asterisk count of current row</param>
		private static void renderRow( int maxCount, int asteriskCount )
		{
			// Calculate how many spaces should be on current row
			int numberOfSpaces = ( maxCount - asteriskCount ) / 2;

			for ( int i = 0; i < maxCount; i++ )
			{
				// Print spaces before and after each block of asterisks
				if ( i < numberOfSpaces || maxCount - i <= numberOfSpaces )
					Console.Write( " " );
				// Or an asterisk
				else
					Console.Write( "*" );
			}

			Console.WriteLine();
		}

		/// <summary>
		///		Query user whether program should continue.
		/// </summary>
		private static bool isContinuing()
		{
			printMessage( Assignment.Resource.promptContinue );

			// If next key pressed is esc (key code 27), return false - do not continue
			bool value = Console.ReadKey().KeyChar != 27;

			// Clear line after query
			Console.WriteLine();

			return value;
		}

		/// <summary>
		///		Standard method to print status messages to the user.
		/// </summary>
		private static void printMessage( string message, bool isError = false )
		{
			Console.WriteLine( "" );

			Console.ForegroundColor = ConsoleColor.White;

			if ( isError )
				Console.BackgroundColor = ConsoleColor.Red;
			else
				Console.BackgroundColor = ConsoleColor.DarkGreen;

			Console.WriteLine( message );
			Console.ResetColor();
			Console.WriteLine( "" );
		}
	}
}
