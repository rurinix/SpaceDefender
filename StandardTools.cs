using System;

namespace SpaceDefender
{
	/// <summary>
	/// Summary description for StandardTools.
	/// </summary>
	public class StandardTools
	{
		public StandardTools()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public static void recordError(String writeString)
		{
			System.IO.StreamWriter saveFile = new System.IO.StreamWriter("errorLog.txt", true);

			saveFile.WriteLine(writeString);

			saveFile.Close();
		}
	}
}
