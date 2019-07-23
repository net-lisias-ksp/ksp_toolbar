﻿/*
Disclaimer: this file was heavily modified by LisiasT, 2019

Copyright (c) 2013-2016, Maik Schreiber
All rights reserved.

Redistribution and use in source and binary forms, with or without modification,
are permitted provided that the following conditions are met:

1. Redistributions of source code must retain the above copyright notice, this
   list of conditions and the following disclaimer.

2. Redistributions in binary form must reproduce the above copyright notice,
   this list of conditions and the following disclaimer in the documentation
   and/or other materials provided with the distribution.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE
FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY,
OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*/
using System;
using UnityEngine;


namespace Toolbar {
	internal static class Utils {
		internal static Vector2 getMousePosition() {
			Vector3 mousePos = Input.mousePosition;
			return new Vector2(mousePos.x, Screen.height - mousePos.y).clampToScreen();
		}

		internal static bool isPauseMenuOpen() {
			// PauseMenu.isOpen may throw NullReferenceException on occasion, even if HighLogic.LoadedScene==GameScenes.FLIGHT
			try {
				return (HighLogic.LoadedScene == GameScenes.FLIGHT) && PauseMenu.isOpen;
			} catch {
				return false;
			}
		}

		internal static Boolean LoadImageFromFile(out Texture2D tex, String fileNamePath)
		{
			try
			{ 
				tex = KSPe.Util.Image.Texture2D.LoadFromFile(fileNamePath);
				return true;
			}
			catch (KSPe.Util.Image.Error e)
			{
				tex = null;
				Log.error(e, e.ToString());
				return false;
			}
		}

		internal static Texture2D GetTexture(string path, bool mipmap)
		{
			return KSPe.Util.Image.Texture2D.Get(path, mipmap);
		}

		internal static bool TextureExists(string texturePath)
		{
			return KSPe.Util.Image.Texture2D.Exists(texturePath);
		}
	}
}
