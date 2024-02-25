using System;
using System.Threading;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace GameTools
{
	public class ThreadedSound
	{
		public ThreadedSound()
		{

		}

		[DllImport("winmm.dll")]
		protected static extern long PlaySound(String lpszName, long hModule, long dwFlags);

		private static Thread audioThread = null;	
		private static string m_strCurrentAudioFile = "shuffle.wav";
  	
		protected static void PlayAudio()
		{
			if( m_strCurrentAudioFile != "" )
				PlaySound(@m_strCurrentAudioFile, 0, 0 );

			m_strCurrentAudioFile = ""; 

			if( audioThread != null )
				audioThread.Abort();
		}

		public static void PlayAudioAsync( string wavefile )
		{
			m_strCurrentAudioFile = wavefile;
			audioThread = new Thread( new ThreadStart(PlayAudio) );
			audioThread.Start();
		}

		public static void PlayAudioSync( string wavefile )
		{
			m_strCurrentAudioFile = wavefile;
			PlayAudio();
		}
	}
}
