using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace GameTools
{
	public class Sound
	{
		public Sound()
		{

		}

		[DllImport("winmm.dll")]
		private static extern bool PlaySound(string szSound, int hModule, long dwFlags);

		public static void PlayAudioAsync( string wavefile )
		{
			PlaySound(wavefile, 0, 0x0001);
		}

		public static void PlayAudioSync( string wavefile )
		{
			PlaySound(wavefile, 0, 0x0000);
		}

		public static void FlushAudio ()
		{
			PlaySound(null,0,0x0000);
		}
	}
}
