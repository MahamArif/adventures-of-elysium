~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
TextFx Unity Plugin v3.x- developed by Fenderrio
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
!!!!!!!!!! UPGRADE FROM v2.x NOTES !!!!!!!!!!!!
!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

This is a big update with a lot of structural changes, so be sure to follow these instructions to ensure that your current TextFx animations work correctly after updating the package.
- Backup your project! Git, SVN, email yourself a ZIP, whatever.
- Delete the old TextFx project folder (Assets/TextFx)
- Import the new TextFx 3.x unity package in your project.
- Include the following namespace to any of your scripts that referenced EffectManager or other TextFx classes:
	"using TextFx.LegacyContent;"
- You should be done!


The TextFx codebase pre v3.x, is now legacy code, and is included only to allow old effects to work as they did, but it is expected that any new TextFx instances created will use the new codebase.

If you experience any problems updating your copy of TextFx to version 3.x, please get in touch; fenderrio@gmail.com




%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
%%%%%%% Getting Started with Unity GUI TextFx %%%%%%%
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

	TextFx 3.x comes with support for Unity's excellent new built-in GUI system (I refer to it as "UGUI").
	It works by overriding the functionality of the UI Text components.

	Create a New TextFxUGUI instance
	++++++++++++++++++++++++++++++++

		- Add a TextFxUGUI instance in exactly the same way you'd add a UGUI Text element to your canvas:
		- 		GameObject -> UI -> TextFx Text
		- This will create a TextFxUGUI component in your UGUI canvas as you'd expect Text to.
		- Click the "Open Animation Editor" button at the bottom of the TextFxUGUI inspector panel to open the TextFx Editor panel
		- Play around with adding Intro, Main and Outro animation phases with the Quick Setup editor, or get straight into the creating your own totally bespoke animations in the Full Editor!
		
	Convert an existing UI Text
	+++++++++++++++++++++++++++

		- You can easily convert any existing UI Text component into an equivalent TextFxUGUI component by using the TextFx conversion shortcut menu item
		- Just select your UI Text object in the editor, and go to:
		- 		Tools -> TextFx -> "Convert UGUI Text to TextFx"
		- This will keep your text exactly as it is, but setup it up as a TextFxUGUI instance instead!
		
	
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
%%%%%%%%% Getting Started with NGUI TextFx %%%%%%%%%%
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

	TextFx 3.x includes support for overriding NGUI UILabel components
	It is known to support all versions of NGUI since NGUI v3.7.2, but likely works with earlier versions as well, they just haven't been tested!
	
	Import the TextFxNGUI UnityPackage
	++++++++++++++++++++++++++++++++
	
		- Before you can start using TextFx animations with your NGUI UILabels, you'll need to import the supporting scripts from the included UnityPackage!
		- Open the unitypackage located at:
			"TextFx / 3rd Party Asset Support"
		- Make sure all the included scripts are added to your project.
	
	
	Create a New TextFxNGUI instance
	++++++++++++++++++++++++++++++++
	
		- Add a TextFxNGUI instance in exactly the same way you'd add an NGUI UILabel component to your NGUI setup
		- 		NGUI -> Create -> TextFx Label
		- This will create a TextFxNGUI component in your NGUI gui setup as you'd expect a UILabel to.
		- Click the "Open Animation Editor" button at the bottom of the TextFxNGUI inspector panel to open the TextFx Editor panel
		- Play around with adding Intro, Main and Outro animation phases with the Quick Setup editor, or get straight into the creating your own totally bespoke animations in the Full Editor!
		
	Convert an existing NGUI UI Label
	+++++++++++++++++++++++++++++++++

		- You can easily convert any existing NGUI UI Label component into an equivalent TextFxNGUI component by using the TextFx conversion shortcut menu item
		- Just select your NGUI UI Label object in the editor, and go to:
		- 		Tools -> TextFx -> "Convert NGUI Text to TextFx"
		- This will keep your text exactly as it is, but setup it up as a TextFxNGUI instance instead!


%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
%%%%%%%%% Setting up a TextFx Native Instance %%%%%%%%%%
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

	TextFx 3.x has it's own text rendering system included as well!
	This is useful for setting up text animations which are unrestricted by any GUI rendering system. It's more like the old Unity 3D Text, except with awesome animations!
	
	Create a New TextFxNative instance
	++++++++++++++++++++++++++++++++
	
		- Add a TextFxNGUI instance to your scene using the following menu item shortcut
		- 		GameObject -> TextFx -> Text
		- This will create a TextFxNative component in your scene.
		- Position it whereever you like!
		- Click the "Open Animation Editor" button at the bottom of the TextFxNative inspector panel to open the TextFx Editor panel
		- Play around with adding Intro, Main and Outro animation phases with the Quick Setup editor, or get straight into the creating your own totally bespoke animations in the Full Editor!





%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
%%%%%%%%%% Scripting with TextFx Animations %%%%%%%%%%%%
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

	Scripting with TextFx animations is easy!
		- Add the TextFx namespace to your script
		-		"using TextFx;"
		- Get a reference to your animation class (TextFxNative, TextFxUGUI, TextFxNGUI)
		- All key method calls are accessible through the AnimationManager property.
				* effect.AnimationManager.PlayAnimation();
		 		* effect.AnimationManager.ResetAnimation();
		 		* effect.AnimationManager.ContinuePastBreak();
		 		* effect.AnimationManager.ContinuePastLoop();
		- See http://www.codeimmunity.co.uk/TextFx/scripting_reference.php for a list of the other script Methods and Properties available.
			Note: EffectManager is the legacy name for the newer TextFxAnimationManager.




%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
%%%%%%%%%%%%%% Quick Setup TextFx Editor %%%%%%%%%%%%%%%
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

	 - Open the animation editor panel by either:
	 	* Clicking the "Open Animation Editor" button at the bottom of the inspector panel of any TextFx object
	 	* Window -> TextFx -> Animation Editor
	 - By Default the Quick Setup panel is selected.
	 - Play around with the Intro, Main and Outro drop downs in order to select different animations to apply.
	 - Play button is at the top of the Animation Editor window!


%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
%%%%%%%%%%%%%%%%% Full TextFx Editor %%%%%%%%%%%%%%%%%%%
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

	- Open the animation editor panel by either:
	 	* Clicking the "Open Animation Editor" button at the bottom of the inspector panel of any TextFx object
	 	* Window -> TextFx -> Animation Editor
	 - Use the tabs near the top of the panel to select "Full Editor"
	 - Here you are exposed to the bare bones of the animation editor, set free to create whatever you like!
	 - A good tip for learning what it all does is to import anims in the Quick Setup editor and then look at how it all works in the Full Editor. Maybe make modifications from there!
	 - See the online docs for more info on what it all means:
	 	http://www.codeimmunity.co.uk/TextFx/creating_an_animation.php





%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
%%%%%%%%%%%%%%%%% Unity Editor 5.2.1 PATCH Release Support %%%%%%%%%%%%%%%%%%%
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%


	- At the time of this package submission, Unity have released several patch editor version (5.2.1p1, 5.2.1p2, 5.2.1p3, 5.2.1p4),
		which all include breaking changes to the Unity UI api upon which TextFxs UGUI implementation depends.
	- As these patch releases don't have any unity compile flags provided with them, there's no neat way for me to automatically provide a
		fix for people using these editors.
	- This release does include a fix for these patch releases, but it requires a small code change to activate the new functionality:
		* Open up TextFxUGUI.cs in your text editor of choice, and uncomment the following code on line 3
			//#define UNITY_5_2_1p2_FIX
		* You should be left with the following on line 3
			#define UNITY_5_2_1p2_FIX
		* That's it! Your TextFxUGUI components should be working normally again!
	- Please note: If the UI api changes on Unity's end are not carried forward into the next proper editor release, this solution may no longer work.
		You may be required to re-comment the code on line 3, or update your TextFx package to a newer version to get it working again.
		Similarly, if you wish to run your project in an older version of the Unity Editor, you'll need to re-comment line 3
	- Any questions, please send an email to fenderrio@gmail.com






%%%%%% Thanks For Buying TextFx %%%%%%

Thank you for purchasing TextFx!
To report any bugs, request any support, or give a feature suggestion, please send an email to fenderrio@gmail.com



%%%%%% Support %%%%%%

Twitter: 			@fenderrio
Support Website: 	http://www.codeimmunity.com/TextFx
Support Email:		fenderrio@gmail.com



%%%%%% ChangeLog %%%%%%

19/10/2015 v3.2.1
+ Added fix for latest Unity 5.2.1 PATCH releases. See instructions above.
+ Fixed bug with prefabs not keeping local scene changes at runtime.
+ Fixed bug with deactivated objects while playing effects.

11/09/2015 v3.2
+ Fixed TextFxUGUI compatibility with Unity Editor v5.2

06/09/2015 v3.1
+ Fixed scripting pause functionality
+ Fixed some NGUI & UGUI implementation bugs (Remember to re-import the TextFxNGUI.unitypackage in '3rd Party Assets Support' to get these fixes when updating)
+ Fixed hard-coded folder hierarchy dependency; now able to move root TextFx folder without it breaking!

24/06/2015 v3.05
+ Fixed dynamic font related jumbled text bugs
+ Fixed and improved the functionality of SetText()
+ Re-exposed legacy OnFinish option field in editor panel
+ Fixed legacy Animate Per option.
+ Fixed TextFxNative alignment bug. Thanks to Lawrence Bishop for helping with this!


19/05/2015 v3.01
+ Fixed build errors caused by class being included in editor-only code section
+ Fixed a data reference serialisation bug causing Text colour data to be lost in editor occasionally
+ Populated TextFxNGUI & TextFxUGUI's SetText() interface methods.


06/05/2015 v3.0
+ Added support for TextFx animations on NGUI's UILabel instances
+ Added support for TextFx animations on UGUI's Text instances
+ Added Quick Setup animation editor for easy setup and tweaking of some core animations



%%%%%% Credits %%%%%%

https://www.assetstore.unity3d.com/#/content/5788 	- Boomlagoon JSON; a great light-weight free JSON library used for the Import/Export functionality
http://www.freesfx.co.uk    						- sound effects used in demo scenes
http://www.1001freefonts.com/  						- fonts used in demo scenes
