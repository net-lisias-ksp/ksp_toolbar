using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using KSP.Localization;


namespace Toolbar
{
	internal class ScaleButtons : AbstractWindow
	{
		private Toolbar toolbar;
		internal event Action OnButtonScaleChanged;
		float scale = Button.MAX_SMALL_TEX_HEIGHT;
		bool followKSPAppScale = false;
		bool followKSPUIScale = true;
		bool ApplyToAllToolbars = true;

		internal ScaleButtons(Toolbar toolbar) : base()
		{
			Rect = new Rect(300, 300, 350, 0);
			// dictionary.cfg
			//

			// Need to add following to all localization files:

			// #TOOLBAR_UI_SCALE_VISIBLE_TITLE = "Adjust Button Scaling"
			// #TOOLBAR_UI_SCALE_FOLLOW_APP_SCALE = "Follow KSP App scale"
			// #TOOLBAR_UI_SCALE_FOLLOW_UI_SCALE = "Follow KSP UI Scale"
			// #TOOLBAR_UI_SCALE_USE_SLIDER = "Use slider to adjust scale"

			// #TOOLBAR_UI_CLOSE = "Close"
			//
			// eg : Localizer.Format("#ID")
			//
			Title = Localizer.Format("#TOOLBAR_UI_CONFIG_VISIBLE_TITLE");

			Dialog = true;
			this.toolbar = toolbar;

			scale = toolbar.savedScale;
			followKSPAppScale = toolbar.savedFollowKSPAppScale;
			followKSPUIScale = toolbar.savedFollowKSPUIScale;
		}



		internal override void drawContents()
		{
			//	GameSettings.UI_SCALE_APPS
			//	GameSettings.UI_SCALE

			GUILayout.BeginVertical();
			followKSPAppScale = GUILayout.Toggle(followKSPAppScale, Localizer.Format("#TOOLBAR_UI_SCALE_FOLLOW_APP_SCALE"));
			followKSPUIScale = GUILayout.Toggle(followKSPUIScale, Localizer.Format("#TOOLBAR_UI_SCALE_FOLLOW_UI_SCALE"));

			GUILayout.Label(Localizer.Format("#TOOLBAR_UI_SCALE_USE_SLIDER"));
			GUILayout.BeginHorizontal();
			if (GUILayout.Button(Button.MAX_SMALL_TEX_HEIGHT.ToString()))
			{
				scale = Button.MAX_SMALL_TEX_HEIGHT;
			}
			GUILayout.FlexibleSpace();
			GUILayout.Label(((int)scale).ToString());
			GUILayout.FlexibleSpace();
			if (GUILayout.Button("57"))
			{
				scale = 57;
			}
			GUILayout.EndHorizontal();

			scale = GUILayout.HorizontalSlider(scale, Button.MAX_SMALL_TEX_HEIGHT, 57);

			//ApplyToAllToolbars = GUILayout.Toggle(ApplyToAllToolbars, "Apply to all toolbars");
			GUILayout.BeginHorizontal();
			GUILayout.FlexibleSpace();
			if (GUILayout.Button(Localizer.Format("#TOOLBAR_UI_CLOSE")))
			{
				destroy();
			}
			GUILayout.FlexibleSpace();
			GUILayout.EndHorizontal();
			GUILayout.EndVertical();
			if (followKSPUIScale != toolbar.savedFollowKSPUIScale ||
				followKSPAppScale != toolbar.savedFollowKSPAppScale ||
				scale != toolbar.savedScale)
			{
				toolbar.savedScale = (int)scale;
				toolbar.savedFollowKSPAppScale = followKSPAppScale;
				toolbar.savedFollowKSPUIScale = followKSPUIScale;
				//if (ApplyToAllToolbars)
				{
					foreach (var toolbar in ToolbarManager.InternalInstance.toolbars)
					{
						toolbar.Value.savedScale = (int)scale;
						toolbar.Value.savedFollowKSPAppScale = followKSPAppScale;
						toolbar.Value.savedFollowKSPUIScale = followKSPUIScale;

						foreach (var folder in toolbar.Value.folders)
						{
							folder.Value.savedScale = (int)scale;
							folder.Value.savedFollowKSPAppScale = followKSPAppScale;
							folder.Value.savedFollowKSPUIScale = followKSPUIScale;
						}
						foreach (var folderButtons in toolbar.Value.folderButtons)
						{
							folderButtons.Value.savedScale = (int)scale;
							folderButtons.Value.savedFollowKSPAppScale = followKSPAppScale;
							folderButtons.Value.savedFollowKSPUIScale = followKSPUIScale;
						}
					}
				}
				RefreshButtonScale();
			}
		}

		void RefreshButtonScale()
		{
			fireButtonScaleChanged();
		}
		private void fireButtonScaleChanged()
		{
			if (OnButtonScaleChanged != null)
			{
				OnButtonScaleChanged();
			}
		}
	}
}
