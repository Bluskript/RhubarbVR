﻿using System;
using System.Collections.Generic;
using System.Text;

using RhuEngine.Linker;

using RNumerics;

namespace RhuEngine.Components.PrivateSpace
{
	public class ProgramTaskBarItem:ITaskBarItem
	{
		public Program Program;

		public Type ProgramType;

		public TaskBar TaskBar;

		public ProgramTaskBarItem(TaskBar taskBar,Program program = null){
			TaskBar = taskBar;
			Program = program;
			if (program != null) {
				ProgramType = program.GetType();
				ShowOpenFlag = true;
				ProgramID = program.ProgramID;
				Icon = program.Icon;
				Texture = program.Texture;
				Name = program.ProgramName;
			}
		}
		public ProgramTaskBarItem(TaskBar taskBar, Type programLink) {
			TaskBar = taskBar;
			if (typeof(Program).IsAssignableFrom(programLink)) {
				var program = (Program)Activator.CreateInstance(programLink);
				ShowOpenFlag = false;
				ProgramID = program.ProgramID;
				Icon = program.Icon;
				Texture = program.Texture;
				Name = program.ProgramName;
				ProgramType = programLink;
			}
			else {
				throw new Exception("Not a program");
			}
		}
		public bool ShowOpenFlag { get; set; }

		public string ProgramID { get; set; }

		public Vector2i? Icon { get; set; }

		public RTexture2D Texture { get; set; }

		public string Name { get; set; }

		public void Clicked() {
			if(Program is null) {
				TaskBar.OpenProgram(ProgramID,ProgramType);
			}
			else {
				Program.ClickedButton();
			}
		}
	}

	public interface ITaskBarItem
	{
		public bool ShowOpenFlag { get; }
		
		public string ProgramID { get; }

		public Vector2i? Icon { get; }

		public RTexture2D Texture { get; }

		public string Name { get; }

		public void Clicked();
	}
}