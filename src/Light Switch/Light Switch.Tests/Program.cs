using System.Reflection;
using LightSwitch.Services;

var tests = new (string Name, Action Test)[]
{
	("WindowsThemeChangeNotifier broadcasts ImmersiveColorSet", WindowsThemeChangeNotifierBroadcastsImmersiveColorSet),
};

var failures = 0;

foreach (var (name, test) in tests)
{
	try
	{
		test();
		Console.WriteLine($"PASS {name}");
	}
	catch (Exception exception)
	{
		failures++;
		Console.WriteLine($"FAIL {name}");
		Console.WriteLine(exception.Message);
	}
}

return failures == 0 ? 0 : 1;

static void WindowsThemeChangeNotifierBroadcastsImmersiveColorSet()
{
	var method = typeof(WindowsThemeChangeNotifier).GetMethod(
		nameof(WindowsThemeChangeNotifier.NotifyThemeChanged),
		BindingFlags.Instance | BindingFlags.Public);

	Assert(method is not null, "NotifyThemeChanged should be public.");

	var notifyValue = typeof(WindowsThemeChangeNotifier).GetField(
		"ThemeChangeSettingName",
		BindingFlags.NonPublic | BindingFlags.Static)?.GetValue(null);

	Assert(
		Equals("ImmersiveColorSet", notifyValue),
		"Windows 11 theme refresh should broadcast the ImmersiveColorSet setting name.");
}

static void Assert(bool condition, string message)
{
	if (!condition)
	{
		throw new InvalidOperationException(message);
	}
}
