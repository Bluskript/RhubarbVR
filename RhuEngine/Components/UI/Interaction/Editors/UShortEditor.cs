﻿using System;

using RhuEngine.WorldObjects;
using RhuEngine.WorldObjects.ECS;

using StereoKit;

namespace RhuEngine.Components
{
	[Category(new string[] { "UI\\Interaction\\Editors" })]
	public class UShortEditor : Editor
	{
		public Linker<ushort> Linker;

		[BindProperty(nameof(EditStringText))]
		public SyncProperty<string> EditString;

		public string EditStringText
		{
			get {
				try {
					return GetValue.Target is not null ? ((short)GetValue.Target.Invoke()).ToString() : Linker.Linked ? Linker.LinkedValue.ToString() : string.Empty;
				}
				catch { return string.Empty; }
			}
			set {
				try {
					if (SetValue.Target is not null) {
						SetValue.Target.Invoke(ushort.Parse(value));
					}
					else {
						if (Linker.Linked) {
							Linker.LinkedValue = ushort.Parse(value);
						}
					}
				}
				catch { }
			}
		}

		public ushort Number
		{
			get {
				try {
					return GetValue.Target is not null ? ((ushort)GetValue.Target.Invoke()) : Linker.Linked ? Linker.LinkedValue : ushort.MaxValue;
				}
				catch { return 0; }
			}
			set {
				try {
					if (SetValue.Target is not null) {
						SetValue.Target.Invoke(value);
					}
					else {
						if (Linker.Linked) {
							Linker.LinkedValue = value;
						}
					}
				}
				catch { }
			}
		}

		[Exsposed]
		public void AddOne() {
			Number++;
		}

		[Exsposed]
		public void RemoveOne() {
			Number--;
		}

		public override void OnAttach() {
			base.OnAttach();
			var buttonOne = Entity.AttachComponent<UIButton>();
			buttonOne.Text.Value = "-";
			buttonOne.onClick.Target = RemoveOne;
			Entity.AttachComponent<UISameLine>();
			var textInput = Entity.AttachComponent<UITextInput>();
			textInput.Value.Target = EditString;
			textInput.NullButton.Value = false;
			textInput.Size.Value = new Vec2(0.07f, 0);
			Entity.AttachComponent<UISameLine>();
			var buttonTwo = Entity.AttachComponent<UIButton>();
			buttonTwo.Text.Value = "+";
			buttonTwo.onClick.Target = AddOne;
		}
	}
}
