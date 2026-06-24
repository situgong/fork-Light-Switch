using System;
using System.Runtime.InteropServices;

namespace LightSwitch.Services
{
	internal class WindowsThemeChangeNotifier : IThemeChangeNotifier
	{
		private const int BroadcastHandle = 0xffff;
		private const uint SettingChangeMessage = 0x001a;
		private const uint AbortIfHung = 0x0002;
		private const uint TimeoutMilliseconds = 5000;
		private const string ThemeChangeSettingName = "ImmersiveColorSet";

		public void NotifyThemeChanged()
		{
			SendMessageTimeout(
				new IntPtr(BroadcastHandle),
				SettingChangeMessage,
				UIntPtr.Zero,
				ThemeChangeSettingName,
				AbortIfHung,
				TimeoutMilliseconds,
				out _);
		}

		[DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern IntPtr SendMessageTimeout(
			IntPtr windowHandle,
			uint message,
			UIntPtr wParam,
			string lParam,
			uint flags,
			uint timeout,
			out UIntPtr result);
	}
}
