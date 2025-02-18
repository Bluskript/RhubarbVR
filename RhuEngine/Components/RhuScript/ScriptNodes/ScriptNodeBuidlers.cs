﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using MessagePack;
using RhuEngine.WorldObjects;
using System.Linq;

namespace RhuEngine.Components.ScriptNodes
{
	public static class ScriptNodeBuidlers
	{
		public static ScriptNodeMethod[] GetNodeMethods(this IScriptNode node) {
			var methods = new List<ScriptNodeMethod>();
			if (node.ReturnType is not null) {
				foreach (var item in node.ReturnType.GetMethods()) {
					try {
						methods.Add(new ScriptNodeMethod(node, item));
					}
					catch {

					}
				}
			}
			return methods.ToArray();
		}
		public static ScriptNodeMethod[] GetNodeMethods(Type node,string name) {
			var methods = new List<ScriptNodeMethod>();
			if (node is not null) {
				foreach (var item in node.GetMethods()) {
					if (item.Name == name) {
						try {
							methods.Add(new ScriptNodeMethod(node, item));
						}
						catch { }
					}
				}
			}
			return methods.ToArray();
		}
		public static ScriptNodeMethod[] GetNodeMethods(Type node) {
			var methods = new List<ScriptNodeMethod>();
			if (node is not null) {
				foreach (var item in node.GetMethods()) {
					try {
						methods.Add(new ScriptNodeMethod(node, item));
					}
					catch { }
				}
			}
			return methods.ToArray();
		}
		public static ScriptNodeWriteField[] GetNodeFieldsWrite(this IScriptNode node) {
			var fields = new List<ScriptNodeWriteField>();
			if (node.ReturnType is not null) {
				foreach (var item in node.ReturnType.GetFields()) {
						try {
							fields.Add(new ScriptNodeWriteField(node, item));
						}
						catch { }
				}
			}
			return fields.ToArray();
		}
		public static ScriptNodeWriteField[] GetNodeFieldsWrite(Type node) {
			var fields = new List<ScriptNodeWriteField>();
			if (node is not null) {
				foreach (var item in node.GetFields()) {
					try {
						fields.Add(new ScriptNodeWriteField(node, item));
					}
					catch { }
				}
			}
			return fields.ToArray();
		}
		public static ScriptNodeReadField[] GetNodeFieldsRead(this IScriptNode node) {
			var fields = new List<ScriptNodeReadField>();
			if (node.ReturnType is not null) {
				foreach (var item in node.ReturnType.GetFields()) {
						try {
							fields.Add(new ScriptNodeReadField(node, item));
						}
						catch { }
				}
			}
			return fields.ToArray();
		}
		public static ScriptNodeReadField[] GetNodeFieldsRead(Type node) {
			var fields = new List<ScriptNodeReadField>();
			if (node is not null) {
				foreach (var item in node.GetFields()) {
					try {
						fields.Add(new ScriptNodeReadField(node, item));
					}
					catch { }
				}
			}
			return fields.ToArray();
		}
		public static ScriptNodeWriteField[] GetNodeFieldsWrite(this IScriptNode node, string fieldName) {
			var fields = new List<ScriptNodeWriteField>();
			if (node.ReturnType is not null) {
				foreach (var item in node.ReturnType.GetFields()) {
					if (item.Name == fieldName) {
						try {
							fields.Add(new ScriptNodeWriteField(node, item));
						}
						catch { }
					}
				}
			}
			return fields.ToArray();
		}
		public static ScriptNodeReadField[] GetNodeFieldsRead(this IScriptNode node, string fieldName) {
			var fields = new List<ScriptNodeReadField>();
			if (node.ReturnType is not null) {
				foreach (var item in node.ReturnType.GetFields()) {
					if (item.Name == fieldName) {
						try {
							fields.Add(new ScriptNodeReadField(node, item));
						}
						catch { }
					}
				}
			}
			return fields.ToArray();
		}
		public static ScriptNodeMethod[] GetNodeMethods(this IScriptNode node, string method) {
			var methods = new List<ScriptNodeMethod>();
			if (node.ReturnType is not null) {
				foreach (var item in node.ReturnType.GetMethods()) {
					if (item.Name == method) {
						try {
							methods.Add(new ScriptNodeMethod(node, item));
						}
						catch { }
					}
				}
			}
			return methods.ToArray();
		}

		public static IScriptNode[] GetScriptNodes(Type typeRequirement = null) {
			var list = new List<IScriptNode> {
				new ScriptNodeConst(new short()),
				new ScriptNodeConst(new int()),
				new ScriptNodeConst(new long()),
				new ScriptNodeConst(new ushort()),
				new ScriptNodeConst(new uint()),
				new ScriptNodeConst(new ulong()),
				new ScriptNodeConst(new float()),
				new ScriptNodeConst(new double()),
				new ScriptNodeConst(""),
				new ScriptNodeConst(new bool()),
				new ScriptNodeConst(new byte()),
				new ScriptNodeConst(new sbyte()),
				new ScriptNodeRoot(),
				new ScriptNodeWorld(),
				new ScriptNodeThrow(),
			};
			list.AddRange(GetNodeMethods(typeof(RhuScriptStatics)));
			if (typeRequirement is not null) {
				list = (from e in list
						where e.ReturnType == typeRequirement
						select e).ToList();
			}
			return list.ToArray();
		}
	}

}
