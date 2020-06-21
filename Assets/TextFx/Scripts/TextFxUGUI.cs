#if !UNITY_3_5 && !UNITY_4_0 && !UNITY_4_0_1 && !UNITY_4_1 && !UNITY_4_2 && !UNITY_4_3 && !UNITY_4_5

//#define UNITY_5_2_1p2_FIX

#if UNITY_4_6 || UNITY_5_0 || UNITY_5_1

#define UGUI_VERSION_1

#endif

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
using System.Reflection;
#endif

namespace TextFx
{

	[AddComponentMenu("UI/TextFx Text", 12)]
	public class TextFxUGUI : Text , TextFxAnimationInterface
	{
#if UNITY_5_2_1p2_FIX
		public static bool USING_UGUI_API_PATCH_FIX = true;
#else
		public static bool USING_UGUI_API_PATCH_FIX = false;
#endif


#if UGUI_VERSION_1 || UNITY_5_2_1p2_FIX
		// Interface class for accessing the required text vertex data in the required format
		public class UGUITextDataHandler : TextFxAnimationManager.GuiTextDataHandler
		{
			List<UIVertex> m_vertData;
			
			public int NumVerts { get { return m_vertData.Count; } }
			
			public UGUITextDataHandler(List<UIVertex> vertData)
			{
				m_vertData = vertData;
			}
			
			public Vector3 GetVertPosition(int index)
			{
				return m_vertData [index].position;
			}
			
			public Color GetVertColour(int index)
			{
				return m_vertData [index].color;
			}
		}
#else
		// Interface class for accessing the required text vertex data in the required format
		public class UGUITextDataHandler : TextFxAnimationManager.GuiTextDataHandler
		{
			Mesh m_mesh;
			
			public int NumVerts { get { return m_mesh.vertexCount; } }
			
			public UGUITextDataHandler(Mesh mesh)
			{
				m_mesh = mesh;
			}
			
			public Vector3 GetVertPosition(int index)
			{
				return m_mesh.vertices [index];
			}
			
			public Color GetVertColour(int index)
			{
				return m_mesh.colors[index];
			}
		}
#endif




		// Editor TextFx conversion menu options
#if UNITY_EDITOR
		[MenuItem ("Tools/TextFx/Convert UGUI Text to TextFx", false, 200)]
		static void ConvertToTextFX ()
		{
			GameObject activeGO = Selection.activeGameObject;
			Text uguiText = activeGO.GetComponent<Text> ();
			TextFxUGUI textfxUGUI = activeGO.GetComponent<TextFxUGUI> ();

			if(textfxUGUI != null)
				return;

			GameObject tempObject = new GameObject("temp");
			textfxUGUI = tempObject.AddComponent<TextFxUGUI>();

			TextFxUGUI.CopyComponent(uguiText, textfxUGUI);

			DestroyImmediate (uguiText);

			TextFxUGUI newUGUIEffect = activeGO.AddComponent<TextFxUGUI> ();

			TextFxUGUI.CopyComponent (textfxUGUI, newUGUIEffect);

			DestroyImmediate (tempObject);

			Debug.Log (activeGO.name + "'s Text component converted into a TextFxUGUI component");
		}
		
		[MenuItem ("Tools/TextFx/Convert UGUI Text to TextFx", true)]
		static bool ValidateConvertToTextFX ()
		{
			if(Selection.activeGameObject != null && Selection.activeGameObject.GetComponent<Text>() != null)
				return true;
			else
				return false;
		}


		static void CopyComponent(Text textFrom, Text textTo)
		{
			textTo.text = textFrom.text;
			textTo.font = textFrom.font;
			textTo.fontSize = textFrom.fontSize;
			textTo.fontStyle = textFrom.fontStyle;
			textTo.lineSpacing = textFrom.lineSpacing;
			textTo.supportRichText = textFrom.supportRichText;
			textTo.alignment = textFrom.alignment;
			textTo.resizeTextForBestFit = textFrom.resizeTextForBestFit;
			textTo.color = textFrom.color;
			textTo.material = textFrom.material;
			textTo.enabled = textFrom.enabled;
		}
#endif

		// Animation Interface Properties

		public string AssetNameSuffix { get { return "_UGUI"; } }
		public float MovementScale { get { return 26f; } }
		public int LayerOverride { get { return 5; } }			// Renders objects on the UI layer
		public TEXTFX_IMPLEMENTATION TextFxImplementation { get { return TEXTFX_IMPLEMENTATION.UGUI; } }

		[HideInInspector, SerializeField]
		TextFxAnimationManager m_animation_manager;
		public TextFxAnimationManager AnimationManager { get { return m_animation_manager; } }

		[HideInInspector, SerializeField]
		GameObject m_gameobject_reference;
		public GameObject GameObject { get { if( m_gameobject_reference == null) m_gameobject_reference = gameObject; return m_gameobject_reference; } }

		public UnityEngine.Object ObjectInstance { get { return this; } }

		public System.Action OnMeshUpdateCall { get; set; }


		// TextFxUGUI specific variables 

#if UGUI_VERSION_1 || UNITY_5_2_1p2_FIX
		[HideInInspector] //, SerializeField]		// UIVertex list doesn't seem to serialise even if marked to 'SerializeField'
		List<UIVertex> m_cachedVerts;
        List<UIVertex> m_currentMeshVerts = new List<UIVertex>();
#else
		[HideInInspector, SerializeField]
		VertexHelper m_cachedVertsHelper;
		[HideInInspector, SerializeField]
		List<Vector3> m_currentMeshVerts = new List<Vector3>();
		[HideInInspector, SerializeField]
		List<Color32> m_currentMeshCols = new List<Color32>();
#endif

        bool m_textFxUpdateGeometryCall = false;	// Set to highlight Geometry update calls triggered by TextFx
		bool m_textFxAnimDrawCall = false;		// Denotes whether the subsequent OnFillVBO call has been triggered by TextFx or not


		protected override void OnEnable()
		{
			base.OnEnable ();

			if (m_animation_manager == null)
				m_animation_manager = new TextFxAnimationManager (new int[]{1,0,3,2});
			
			m_animation_manager.SetParentObjectReferences (gameObject, transform, this);
		}

		protected override void Start()
		{
			if(!Application.isPlaying)
			{
				return;
			}

			m_animation_manager.OnStart ();
		}

		void Update()
		{
			if(!Application.isPlaying || !m_animation_manager.Playing)
			{
				return;
			} 

			m_animation_manager.UpdateAnimation ();

			// Call to update mesh rendering
			TextFxUpdateGeometry ();
		}

		// Interface Method: To redraw the mesh with the current animated mesh state
		public void UpdateTextFxMesh()
		{
			// Call to update mesh rendering
			TextFxUpdateGeometry ();
		}

		// Interface Method: To set the text of the text renderer
		public void SetText(string new_text)
		{
			text = new_text;
		}

        // Interface Method: Returns the number of verts used for the current rendered text
        public int NumMeshVerts
        {
            get
            {
                return m_currentMeshVerts.Count;
            }
        }

        // Interface Method: Access to a specified vert from the current state of the rendered text
        public Vector3 GetMeshVert(int index)
        {
            if (m_currentMeshVerts.Count <= index)
            {
                Debug.LogWarning("Requested vertex index '" + index + "' is out of range");
                return Vector3.zero;
            }

#if UGUI_VERSION_1 || UNITY_5_2_1p2_FIX
			return m_currentMeshVerts[index].position;
#else
            return m_currentMeshVerts[index];
#endif
        }

        // Interface Method: Access to a specified vert colour from the current state of the rendered text
        public Color GetMeshColour(int index)
        {
            if (m_currentMeshVerts.Count <= index)
            {
                Debug.LogWarning("Requested vert colour index '" + index + "' is out of range");
                return Color.white;
            }

#if UGUI_VERSION_1 || UNITY_5_2_1p2_FIX
			return m_currentMeshVerts[index].color;
#else
            return m_currentMeshCols[index];
#endif
        }

        // Wrapper to catch all TextFx related calls to UI.Text's UpdateGeometry.
        void TextFxUpdateGeometry()
		{
			m_textFxUpdateGeometryCall = true;

			UpdateGeometry ();
		}

		protected override void UpdateGeometry()
		{
			if(m_textFxUpdateGeometryCall)
			{
				// TextFx Triggering Geometry Update
				m_textFxAnimDrawCall = true;
			}
			else
			{
				// System calling Geometry Update
				m_textFxAnimDrawCall = false;
			}

			m_textFxUpdateGeometryCall = false;

			base.UpdateGeometry ();
		}



#if UGUI_VERSION_1 || UNITY_5_2_1p2_FIX

		/// <summary>
		/// Draw the Text.
		/// </summary>

#if UNITY_5_2_1p2_FIX
		protected override void OnPopulateMesh(VertexHelper vHelper)
#else
		protected override void OnFillVBO(List<UIVertex> vbo)
#endif
		{
			if (font == null)
			{
				m_textFxAnimDrawCall = false;
				return;
			}


			if (!m_textFxAnimDrawCall || m_cachedVerts == null)
			{
#if UNITY_5_2_1p2_FIX
				if (m_cachedVerts == null)
					m_cachedVerts = new List<UIVertex>();

				base.OnPopulateMesh(vHelper);

				// Update UIVertex cache
				m_cachedVerts.Clear();

				UIVertex new_vert = new UIVertex();
				for (int idx = 0; idx < vHelper.currentVertCount; idx++)
				{
					vHelper.PopulateUIVertex(ref new_vert, idx);

					m_cachedVerts.Add(new_vert);
				}

				// Update current mesh state values
				m_currentMeshVerts = m_cachedVerts.GetRange(0, m_cachedVerts.Count);
#else
		
				base.OnFillVBO (vbo);

				// Update caches
				m_cachedVerts = vbo.GetRange(0, vbo.Count);

				// Update current mesh state values
                m_currentMeshVerts = vbo.GetRange(0, vbo.Count);
#endif


				// Call to update animation letter setups
				m_animation_manager.UpdateText(text, new UGUITextDataHandler(m_cachedVerts), white_space_meshes: true);

				if (Application.isPlaying
				   || m_animation_manager.Playing					// Animation is playing, so will now need to render this new mesh in the current animated state
#if UNITY_EDITOR
				   || TextFxAnimationManager.ExitingPlayMode		// To stop text being auto rendered in to default position when exiting play mode in editor.
#endif
				   )
				{
					// The verts need to be set to their current animated states
					// So recall OnFillVBO, getting it to populate with whatever available animate mesh data for this frame
					m_textFxAnimDrawCall = true;

					// First check if default data needs setting
					m_animation_manager.PopulateDefaultMeshData();

#if UNITY_5_2_1p2_FIX
					OnPopulateMesh(vHelper);
#else
		
					vbo.Clear();
					
					OnFillVBO(vbo );
#endif

					return;
				}
				else
				{
					m_animation_manager.PopulateDefaultMeshData(true);
				}
			}
			else
			{
				// TextFx render call. Use cached text mesh data
				UIVertex new_vert = new UIVertex();

#if UNITY_5_2_1p2_FIX
				vHelper.Clear();
				if (m_currentMeshVerts == null)
					m_currentMeshVerts = new List<UIVertex>();
				else
				    m_currentMeshVerts.Clear();

				// Add each cached vert into the VBO buffer. Verts seem to need to be added one by one using Add(), can't just copy the list over
				for (int idx = 0; idx < m_cachedVerts.Count; idx += 4)
				{
					vHelper.AddUIVertexQuad(new UIVertex[] { m_cachedVerts[idx], m_cachedVerts[idx + 1], m_cachedVerts[idx + 2], m_cachedVerts[idx + 3] });

					m_currentMeshVerts.Add(m_cachedVerts[idx]);
					m_currentMeshVerts.Add(m_cachedVerts[idx+1]);
					m_currentMeshVerts.Add(m_cachedVerts[idx+2]);
					m_currentMeshVerts.Add(m_cachedVerts[idx+3]);

					if (m_animation_manager.MeshVerts != null && idx < m_animation_manager.MeshVerts.Length)
					{
						for (int qidx = 0; qidx < 4; qidx++)
						{
							vHelper.PopulateUIVertex(ref new_vert, idx + qidx);
							new_vert.position = m_animation_manager.MeshVerts[idx + qidx];
							new_vert.color = m_animation_manager.MeshColours[idx + qidx];
							vHelper.SetUIVertex(new_vert, idx + qidx);

							m_currentMeshVerts[idx + qidx] = new_vert;
                        }
					}
				}
#else
				// Add each cached vert into the VBO buffer. Verts seem to need to be added one by one using Add(), can't just copy the list over
				for (int idx=0; idx < m_cachedVerts.Count; idx++)
				{
					vbo.Add( m_cachedVerts[idx]);

					if(m_animation_manager.MeshVerts != null && idx < m_animation_manager.MeshVerts.Length)
					{
						new_vert = vbo[vbo.Count - 1];
						new_vert.position = m_animation_manager.MeshVerts[idx];
						new_vert.color = m_animation_manager.MeshColours[idx];
						vbo[vbo.Count - 1] = new_vert;
					}
				}

                // Update current mesh state values
                m_currentMeshVerts = vbo.GetRange(0, vbo.Count);
#endif



#if UNITY_EDITOR
				// Set object dirty to trigger sceneview redraw/update. Calling SceneView.RepaintAll() doesn't work for some reason.
				EditorUtility.SetDirty( GameObject );
#endif
			}

			m_textFxAnimDrawCall = false;

			if (OnMeshUpdateCall != null)
				OnMeshUpdateCall();
		}

#else

		protected override void OnPopulateMesh(Mesh toFill)
		{
			if (font == null)
			{
				m_textFxAnimDrawCall = false;
				return;
			}

			if (!m_textFxAnimDrawCall || m_cachedVertsHelper == null)
			{
				// Native unity render call

				base.OnPopulateMesh (toFill);

				// Update caches
				m_cachedVertsHelper = new VertexHelper(toFill);

                // Update current mesh state values
                m_currentMeshVerts.Clear();
                m_currentMeshVerts.AddRange(toFill.vertices);
                m_currentMeshCols.Clear();
                m_currentMeshCols.AddRange(toFill.colors32);

                // Call to update animation letter setups
                m_animation_manager.UpdateText (text, new UGUITextDataHandler(toFill), white_space_meshes: true);


				if (Application.isPlaying 
					|| m_animation_manager.Playing					// Animation is playing, so will now need to render this new mesh in the current animated state
#if UNITY_EDITOR
				   || TextFxAnimationManager.ExitingPlayMode		// To stop text being auto rendered in to default position when exiting play mode in editor.
#endif
				   )
				{
					// The verts need to be set to their current animated states
					// So recall OnPopulateMesh, getting it to populate with whatever available animate mesh data for this frame
					m_textFxAnimDrawCall = true;

					// First check if default data needs setting
					m_animation_manager.PopulateDefaultMeshData();

					OnPopulateMesh( toFill );
					return;
				}
				else
				{
					m_animation_manager.PopulateDefaultMeshData(true);
				}
			}
			else
			{
				// TextFx render call. Use cached text mesh data

				// Fill mesh with the cached text state
				m_cachedVertsHelper.FillMesh(toFill);

				if(m_animation_manager.MeshVerts != null)
				{
					m_currentMeshVerts.Clear();
					m_currentMeshVerts.AddRange(toFill.vertices);
					m_currentMeshCols.Clear();
					m_currentMeshCols.AddRange(toFill.colors32);

					// Loop through each vert setting its new animated state
					for(int idx=0; idx < toFill.vertexCount; idx++)
					{
						if(idx < m_animation_manager.MeshVerts.Length)
						{
							m_currentMeshVerts[idx] = m_animation_manager.MeshVerts[idx];
							m_currentMeshCols[idx] = (Color32) m_animation_manager.MeshColours[idx];
						}
					}

					toFill.SetVertices(m_currentMeshVerts);
					toFill.SetColors(m_currentMeshCols);

					toFill.RecalculateBounds();
				}

				
#if UNITY_EDITOR
				// Set object dirty to trigger sceneview redraw/update. Calling SceneView.RepaintAll() doesn't work for some reason.
				EditorUtility.SetDirty( GameObject );
#endif
			}
			
			m_textFxAnimDrawCall = false;

			if (OnMeshUpdateCall != null)
				OnMeshUpdateCall();
		}

#endif

	}
}
#endif
