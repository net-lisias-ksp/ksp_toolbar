/*
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
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        static Texture2D GetDDSViaUnBlur(string path)
        {
            return UnBlur.UnBlur.LoadDDS(path, false);
        }
        //
        // The following function was initially copied from @JPLRepo's AmpYear mod, which is covered by the GPL, as is this mod
        //
        // This function will attempt to load either a PNG or a JPG from the specified path.  
        // It first checks to see if the actual file is there, if not, it then looks for either a PNG or a JPG
        //
        // easier to specify different cases than to change case to lower.  This will fail on MacOS and Linux
        // if a suffix has mixed case
        static string[] imgSuffixes = new string[] { ".png", ".jpg", ".gif", ".PNG", ".JPG", ".GIF", ".dds", ".DDS" };
        static Boolean LoadImageFromFile(out Texture2D tex, String fileNamePath)
        {

            Boolean blnReturn = false;
            tex = null;
            bool dds = false;
            try
            {
                string path = fileNamePath;
                if (!System.IO.File.Exists(fileNamePath))
                {
                    // Look for the file with an appended suffix.
                    for (int i = 0; i < imgSuffixes.Length; i++)

                        if (System.IO.File.Exists(fileNamePath + imgSuffixes[i]))
                        {
                            path = fileNamePath + imgSuffixes[i];
                            dds = imgSuffixes[i] == ".dds" || imgSuffixes[i] == ".DDS";
                            break;
                        }
                }

                //File Exists check
                if (System.IO.File.Exists(path))
                {
                    try
                    {
                        if (UnblurCheck.unBlurPresent)
                        {
                            if (dds)
                            {
                                tex = GetDDSViaUnBlur(path);
                                if (tex == null)
                                    throw new Exception("LoadDDS failed.");
                            }
                            else
                            {
                                tex = new Texture2D(16, 16, TextureFormat.ARGB32, false);
                                tex.LoadImage(System.IO.File.ReadAllBytes(path));
                            }
                        } else
                        {
                            tex = new Texture2D(16, 16, TextureFormat.ARGB32, false);
                            tex.LoadImage(System.IO.File.ReadAllBytes(path));
                        }
                        blnReturn = true;
                    }
                    catch (Exception ex)
                    {
                        Log.error("Failed to load the texture:" + path);
                        Log.error(ex.Message);
                    }
                }
                else
                {
                    Log.error("Cannot find texture to load:" + fileNamePath);
                }


            }
            catch (Exception ex)
            {
                Log.error("Failed to load (are you missing a file):" + fileNamePath);
                Log.error(ex.Message);
            }
            return blnReturn;
        }
        internal static bool TextureExists(string texturePath)
        {
            if (GameDatabase.Instance.ExistsTexture(texturePath))
                return true;
            string fileNamePath = TexPathname(texturePath);
            for (int i = 0; i < imgSuffixes.Length; i++)
                if (System.IO.File.Exists(fileNamePath + imgSuffixes[i]))
                    return true;
            return false;
        }
        internal static string TexPathname(string path)
        {
            string s =  KSPUtil.ApplicationRootPath + "GameData/" + path;
            Log.info("TexPathname: " + s);
            return s;
        }

        internal static Texture2D GetTextureViaUnBlur(string path, bool asNormalMap)
        {
            return UnBlur.UnBlur.Instance?.GetTexture(path, asNormalMap);
        }
        internal static Texture2D GetTexture(string path, bool asNormalMap)
        {
            Texture2D tex;
            if (UnblurCheck.unBlurPresent)
            {
                // ask unBlur to look for the texture in GameDatabase, remove mipmaps if necessary, and return it
                tex = GetTextureViaUnBlur(path, asNormalMap);
                if (tex != null) return tex;
            }

            // texture not found in GameDatabase
            LoadImageFromFile(out tex, TexPathname(path));
            return tex;
        }
    }
}
