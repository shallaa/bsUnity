﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;

public partial class bs{

	public class JSON {
	
		public Dictionary<string, object> fields = new Dictionary<string, object>();
		
		// Indexer to provide read/write access to the fields
		public object this[string fieldName]   
		{
			// Read one byte at offset index and return it.
			get 
			{
				if (fields.ContainsKey(fieldName))
					return(fields[fieldName]);
				return null;
			}
			// Write one byte at offset index and return it.
			set 
			{
				if (fields.ContainsKey(fieldName))
					fields[fieldName] = value;
				else
					fields.Add(fieldName,value);
			}
		}		
		
		public string ToString(string fieldName)
		{
			if (fields.ContainsKey(fieldName))
				return System.Convert.ToString(fields[fieldName]);
			else
				return "";
		}
		public int ToInt(string fieldName)
		{
			if (fields.ContainsKey(fieldName))
				return System.Convert.ToInt32(fields[fieldName]);
			else
				return 0;
		}
		public float ToFloat(string fieldName)
		{
			if (fields.ContainsKey(fieldName))
				return System.Convert.ToSingle(fields[fieldName]);
			else
				return 0.0f;
		}
		public bool ToBoolean(string fieldName)
		{
			if (fields.ContainsKey(fieldName))
				return System.Convert.ToBoolean(fields[fieldName]);
			else
				return false;
		}
				
		public string serialized
		{
			get
			{
				return _JSON.Serialize(this);
			}
			set
			{
				JSON o = _JSON.Deserialize(value);
				if (o!=null)
					fields = o.fields;
			}
		}
		
		public JSON ToJSON(string fieldName)
		{
			if (!fields.ContainsKey(fieldName))
				fields.Add(fieldName, new JSON());
				
			return (JSON)this[fieldName];
		}
		
		// to serialize/deserialize a Vector2
		public static implicit operator Vector2(JSON value)
		{
		   return new Vector3(
				System.Convert.ToSingle(value["x"]),
				System.Convert.ToSingle(value["y"]));
		}	
		public static explicit operator JSON(Vector2 value)
		{
			checked
			{
				JSON o = new JSON();
				o["x"] = value.x;
				o["y"] = value.y;
				return o;
			}
	
	   }		
	
			
		// to serialize/deserialize a Vector3
		public static implicit operator Vector3(JSON value)
		{
		   return new Vector3(
				System.Convert.ToSingle(value["x"]),
				System.Convert.ToSingle(value["y"]),
				System.Convert.ToSingle(value["z"]));
		}				
		
		public static explicit operator JSON(Vector3 value)
		{
			checked
			{
				JSON o = new JSON();
				o["x"] = value.x;
				o["y"] = value.y;
				o["z"] = value.z;
				return o;
			}
	   }		
		
		// to serialize/deserialize a Quaternion
		public static implicit operator Quaternion(JSON value)
		{
		   return new Quaternion(
				System.Convert.ToSingle(value["x"]),
				System.Convert.ToSingle(value["y"]),
				System.Convert.ToSingle(value["z"]),
				System.Convert.ToSingle(value["w"])
				);
		}				
		public static explicit operator JSON(Quaternion value)
		{
			checked
			{
				JSON o = new JSON();
				o["x"] = value.x;
				o["y"] = value.y;
				o["z"] = value.z;
				o["w"] = value.w;
				return o;
			}
	   }		
		
		// to serialize/deserialize a Color
		public static implicit operator Color(JSON value)
		{
		   return new Color(
				System.Convert.ToSingle(value["r"]),
				System.Convert.ToSingle(value["g"]),
				System.Convert.ToSingle(value["b"]),
				System.Convert.ToSingle(value["a"])
				);
		}				
		public static explicit operator JSON(Color value)
		{
			checked
			{
				JSON o = new JSON();
				o["r"] = value.r;
				o["g"] = value.g;
				o["b"] = value.b;
				o["a"] = value.a;
				return o;
			}
	   }		
		
		// to serialize/deserialize a Color32
		public static implicit operator Color32(JSON value)
		{
		   return new Color32(
				System.Convert.ToByte(value["r"]),
				System.Convert.ToByte(value["g"]),
				System.Convert.ToByte(value["b"]),
				System.Convert.ToByte(value["a"])
				);
		}				
		public static explicit operator JSON(Color32 value)
		{
			checked
			{
				JSON o = new JSON();
				o["r"] = value.r;
				o["g"] = value.g;
				o["b"] = value.b;
				o["a"] = value.a;
				return o;
			}
	   }		
	
		// to serialize/deserialize a Rect
		public static implicit operator Rect(JSON value)
		{
		   return new Rect(
				System.Convert.ToByte(value["left"]),
				System.Convert.ToByte(value["top"]),
				System.Convert.ToByte(value["width"]),
				System.Convert.ToByte(value["height"])
				);
		}				
		public static explicit operator JSON(Rect value)
		{
			checked
			{
				JSON o = new JSON();
				o["left"] = value.xMin;
				o["top"] = value.yMax;
				o["width"] = value.width;
				o["height"] = value.height;
				return o;            }
	   }
		
		
	   // get typed array out of the object as object[] 
	   public T[] ToArray<T>(string fieldName)
	   {
			if (fields.ContainsKey(fieldName))
			{				
				if (fields[fieldName] is IEnumerable)
				{
					List<T> l = new List<T>();
					foreach (object o in (fields[fieldName] as IEnumerable))
					{
						if (l is List<string>)
							(l as List<string>).Add(System.Convert.ToString(o));
						else
						if (l is List<int>)
							(l as List<int>).Add(System.Convert.ToInt32(o));
						else
						if (l is List<float>)
							(l as List<float>).Add(System.Convert.ToSingle(o));
						else
						if (l is List<bool>)
							(l as List<bool>).Add(System.Convert.ToBoolean(o));
						else
						if (l is List<Vector2>)
							(l as List<Vector2>).Add((Vector2)((JSON)o));
						else
						if (l is List<Vector3>)
							(l as List<Vector3>).Add((Vector3)((JSON)o));
						else
						if (l is List<Rect>)
							(l as List<Rect>).Add((Rect)((JSON)o));
						else
						if (l is List<Color>)
							(l as List<Color>).Add((Color)((JSON)o));
						else
						if (l is List<Color32>)
							(l as List<Color32>).Add((Color32)((JSON)o));
						else
						if (l is List<Quaternion>)
							(l as List<Quaternion>).Add((Quaternion)((JSON)o));
						else
						if (l is List<JSON>)
							(l as List<JSON>).Add((JSON)o);
					}
					return l.ToArray();
				}
			}
			return new T[]{};
	   }
		sealed class _JSON {
			public static JSON Deserialize(string json) {
				if (json == null) {
					return null;
				}
		
				return Parser.Parse(json);
			}
		
			sealed class Parser : IDisposable {
				const string WHITE_SPACE = " \t\n\r";
				const string WORD_BREAK = " \t\n\r{}[],:\"";
		
				enum TOKEN {
					NONE,
					CURLY_OPEN,
					CURLY_CLOSE,
					SQUARED_OPEN,
					SQUARED_CLOSE,
					COLON,
					COMMA,
					STRING,
					NUMBER,
					TRUE,
					FALSE,
					NULL
				};
		
				StringReader json;
		
				Parser(string jsonString) {
					json = new StringReader(jsonString);
				}
		
				public static JSON Parse(string jsonString) {
					using (var instance = new Parser(jsonString)) {
						return (instance.ParseValue() as JSON);
					}
				}
		
				public void Dispose() {
					json.Dispose();
					json = null;
				}
				
				JSON ParseObject() {
					Dictionary<string, object> table = new Dictionary<string, object>();
					JSON obj = new JSON();
					obj.fields = table;
		
					// ditch opening brace
					json.Read();
					
					// {
					while (true) {
						switch (NextToken) {
						case TOKEN.NONE:
							return null;
						case TOKEN.COMMA:
							continue;
						case TOKEN.CURLY_CLOSE:
							return obj;
						default:
							// name
							string name = ParseString();
							if (name == null) {
								return null;
							}
		
							// :
							if (NextToken != TOKEN.COLON) {
								return null;
							}
							// ditch the colon
							json.Read();
		
							// value
							table[name] = ParseValue();
							break;
						}
					}												
				}
		
				List<object> ParseArray() {
					List<object> array = new List<object>();
		
					// ditch opening bracket
					json.Read();
		
					// [
					var parsing = true;
					while (parsing) {
						TOKEN nextToken = NextToken;
		
						switch (nextToken) {
						case TOKEN.NONE:
							return null;
						case TOKEN.COMMA:
							continue;
						case TOKEN.SQUARED_CLOSE:
							parsing = false;
							break;
						default:
							object value = ParseByToken(nextToken);
		
							array.Add(value);
							break;
						}
					}
		
					return array;
				}
		
				object ParseValue() {
					TOKEN nextToken = NextToken;
					return ParseByToken(nextToken);
				}
		
				object ParseByToken(TOKEN token) {
					switch (token) {
					case TOKEN.STRING:
						return ParseString();
					case TOKEN.NUMBER:
						return ParseNumber();
					case TOKEN.CURLY_OPEN:
						return ParseObject();
					case TOKEN.SQUARED_OPEN:
						return ParseArray();
					case TOKEN.TRUE:
						return true;
					case TOKEN.FALSE:
						return false;
					case TOKEN.NULL:
						return null;
					default:
						return null;
					}
				}
		
				string ParseString() {
					StringBuilder s = new StringBuilder();
					char c;
		
					// ditch opening quote
					json.Read();
		
					bool parsing = true;
					while (parsing) {
		
						if (json.Peek() == -1) {
							parsing = false;
							break;
						}
		
						c = NextChar;
						switch (c) {
						case '"':
							parsing = false;
							break;
						case '\\':
							if (json.Peek() == -1) {
								parsing = false;
								break;
							}
		
							c = NextChar;
							switch (c) {
							case '"':
							case '\\':
							case '/':
								s.Append(c);
								break;
							case 'b':
								s.Append('\b');
								break;
							case 'f':
								s.Append('\f');
								break;
							case 'n':
								s.Append('\n');
								break;
							case 'r':
								s.Append('\r');
								break;
							case 't':
								s.Append('\t');
								break;
							case 'u':
								var hex = new StringBuilder();
		
								for (int i=0; i< 4; i++) {
									hex.Append(NextChar);
								}
		
								s.Append((char) Convert.ToInt32(hex.ToString(), 16));
								break;
							}
							break;
						default:
							s.Append(c);
							break;
						}
					}
		
					return s.ToString();
				}
		
				object ParseNumber() {
					string number = NextWord;
		
					if (number.IndexOf('.') == -1) {
						long parsedInt;
						Int64.TryParse(number, out parsedInt);
						return parsedInt;
					}
		
					double parsedDouble;
					Double.TryParse(number, out parsedDouble);
					return parsedDouble;
				}
		
				void EatWhitespace() {
					while (WHITE_SPACE.IndexOf(PeekChar) != -1) {
						json.Read();
		
						if (json.Peek() == -1) {
							break;
						}
					}
				}
		
				char PeekChar {
					get {
						return Convert.ToChar(json.Peek());
					}
				}
		
				char NextChar {
					get {
						return Convert.ToChar(json.Read());
					}
				}
		
				string NextWord {
					get {
						StringBuilder word = new StringBuilder();
		
						while (WORD_BREAK.IndexOf(PeekChar) == -1) {
							word.Append(NextChar);
		
							if (json.Peek() == -1) {
								break;
							}
						}
		
						return word.ToString();
					}
				}
		
				TOKEN NextToken {
					get {
						EatWhitespace();
		
						if (json.Peek() == -1) {
							return TOKEN.NONE;
						}
		
						char c = PeekChar;
						switch (c) {
						case '{':
							return TOKEN.CURLY_OPEN;
						case '}':
							json.Read();
							return TOKEN.CURLY_CLOSE;
						case '[':
							return TOKEN.SQUARED_OPEN;
						case ']':
							json.Read();
							return TOKEN.SQUARED_CLOSE;
						case ',':
							json.Read();
							return TOKEN.COMMA;
						case '"':
							return TOKEN.STRING;
						case ':':
							return TOKEN.COLON;
						case '0':
						case '1':
						case '2':
						case '3':
						case '4':
						case '5':
						case '6':
						case '7':
						case '8':
						case '9':
						case '-':
							return TOKEN.NUMBER;
						}
		
						string word = NextWord;
		
						switch (word) {
						case "false":
							return TOKEN.FALSE;
						case "true":
							return TOKEN.TRUE;
						case "null":
							return TOKEN.NULL;
						}
		
						return TOKEN.NONE;
					}
				}
			}
		
			/// <summary>
			/// Converts a IDictionary / IList object or a simple type (string, int, etc.) into a JSON string
			/// </summary>
			/// <param name="json">A Dictionary&lt;string, object&gt; / List&lt;object&gt;</param>
			/// <returns>A JSON encoded string, or null if object 'json' is not serializable</returns>
			public static string Serialize(JSON obj) {
				return Serializer.Serialize(obj);
			}
		}
		sealed class Serializer {
			StringBuilder builder;
	
			Serializer() {
				builder = new StringBuilder();
			}
	
			public static string Serialize(JSON obj) {
				var instance = new Serializer();
	
				instance.SerializeValue(obj);
	
				return instance.builder.ToString();
			}
	
			void SerializeValue(object value) {
				if (value == null) {
					builder.Append("null");
				}
				else if (value as string != null) {
					SerializeString(value as string);
				}
				else if (value is bool) {
					builder.Append(value.ToString().ToLower());
				}
				else if (value as JSON != null) {
					SerializeObject(value as JSON);
				}
				else if (value as IDictionary != null) {
					SerializeDictionary(value as IDictionary);
				}
				else if (value as IList != null) {
					SerializeArray(value as IList);
				}
				else if (value is char) {
					SerializeString(value.ToString());
				}
				else {
					SerializeOther(value);
				}
			}
	
			void SerializeObject(JSON obj) {
				SerializeDictionary(obj.fields);
			}
			
			void SerializeDictionary(IDictionary obj) {
				bool first = true;
	
				builder.Append('{');
	
				foreach (object e in obj.Keys) {
					if (!first) {
						builder.Append(',');
					}
	
					SerializeString(e.ToString());
					builder.Append(':');
	
					SerializeValue(obj[e]);
	
					first = false;
				}
	
				builder.Append('}');
			}
	
			void SerializeArray(IList anArray) {
				builder.Append('[');
	
				bool first = true;
	
				foreach (object obj in anArray) {
					if (!first) {
						builder.Append(',');
					}
	
					SerializeValue(obj);
	
					first = false;
				}
	
				builder.Append(']');
			}
	
			void SerializeString(string str) {
				builder.Append('\"');
	
				char[] charArray = str.ToCharArray();
				foreach (var c in charArray) {
					switch (c) {
					case '"':
						builder.Append("\\\"");
						break;
					case '\\':
						builder.Append("\\\\");
						break;
					case '\b':
						builder.Append("\\b");
						break;
					case '\f':
						builder.Append("\\f");
						break;
					case '\n':
						builder.Append("\\n");
						break;
					case '\r':
						builder.Append("\\r");
						break;
					case '\t':
						builder.Append("\\t");
						break;
					default:
						int codepoint = Convert.ToInt32(c);
						if ((codepoint >= 32) && (codepoint <= 126)) {
							builder.Append(c);
						}
						else {
							builder.Append("\\u" + Convert.ToString(codepoint, 16).PadLeft(4, '0'));
						}
						break;
					}
				}
	
				builder.Append('\"');
			}
	
			void SerializeOther(object value) {
				if (value is float
					|| value is int
					|| value is uint
					|| value is long
					|| value is double
					|| value is sbyte
					|| value is byte
					|| value is short
					|| value is ushort
					|| value is ulong
					|| value is decimal) {
					builder.Append(value.ToString());
				}
				else {
					SerializeString(value.ToString());
				}
			}
		}
	}
}
