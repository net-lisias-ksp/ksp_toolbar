using System;
using L = KSPe.Util.Log;
using System.Diagnostics;

namespace Toolbar
{
	internal static class Log
	{
		static readonly L.Logger log = L.Logger.CreateForType<InstallChecker>();

		public enum LEVEL
		{
			OFF = L.Level.OFF,
			ERROR = L.Level.ERROR,
			WARNING = L.Level.WARNING,
			INFO = L.Level.INFO,
			DETAIL = L.Level.DETAIL,
			TRACE = L.Level.TRACE
		};

		internal static int Level {
			get => (int)log.level;
			set => log.level = (KSPe.Util.Log.Level)(value % 6);
		}

		internal static void info(string msg, params object[] @params)
		{
			log.info(msg, @params);
		}

		internal static void warn(string msg, params object[] @params)
		{
			log.warn(msg, @params);
		}

		internal static void trace(string msg, params object[] @params)
		{
			log.detail(msg, @params);
		}

		internal static void error(string msg, params object[] @params)
		{
			log.error(msg, @params);
		}

		internal static void error(Exception e, string msg, params object[] @params)
		{
			log.error(e, msg, @params);
		}

		[ConditionalAttribute("DEBUG")]
		internal static void debug(string msg, params object[] @params)
		{
			log.detail(msg, @params);
		}
	}
}
