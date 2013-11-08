using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public partial class bs{

	private GameObject __go;
	private Hashtable __twMove = new Hashtable(), __twScale = new Hashtable(), __twRotate = new Hashtable();
	private Vector3 __pos, __scale, __rotate = new Vector3();
	private Color __color;
	
	private bool __isPos = false, __isScale = false, __isRotate = false, __isColor = false,
		__isTwM = false, __isTwR = false, __isTwS = false,
		__isLight = false;
	
	private float __Lrange = float.NegativeInfinity, __Lintensity = float.NegativeInfinity, __LspotAngle = float.NegativeInfinity;
	
	private bs init( GameObject g ){
		__go = g;
		__pos = __go.transform.position;
		__rotate = __go.transform.eulerAngles;
		__scale = __go.transform.localScale;
		if( __go.renderer != null )	__color = __go.renderer.material.color;
		return this;
	}
	
	private void c( Color c ){ __go.renderer.material.color = __color = c; }
	private void cr( float v ){ __isColor = true; __color.r = v; }
	private void cg( float v ){ __isColor = true; __color.g = v; }
	private void cb( float v ){ __isColor = true; __color.b = v; }
	private void ca( float v ){ __isColor = true; __color.a = v; }
	private void brightness( float v ){
		float t0;
		__isColor = true;
		t0 = __color.r + v; __color.r = t0 > 1f ? 1f : t0 < 0f ? 0f : t0;
		t0 = __color.g + v; __color.g = t0 > 1f ? 1f : t0 < 0f ? 0f : t0;
		t0 = __color.b + v; __color.b = t0 > 1f ? 1f : t0 < 0f ? 0f : t0;
	}
	
	private void r( Vector3 v ){ __rotate = v; __go.transform.rotation = Quaternion.Euler( v ); }
	private void rx( float v ){ __isRotate = true; __rotate.x = v; }
	private void ry( float v ){ __isRotate = true; __rotate.y = v; }
	private void rz( float v ){ __isRotate = true; __rotate.z = v; }
	
	private void p( Vector3 v ){ __go.transform.position = __pos = v; }
	private void x( float v ){ __isPos = true; __pos.x = v; }
	private void y( float v ){ __isPos = true; __pos.y = v; }
	private void z( float v ){ __isPos = true; __pos.z = v; }
	
	private void s( Vector3 v ){ __go.transform.localScale = __scale = v; }
	private void sx( float v ){ __isScale = true; __scale.x = v; }
	private void sy( float v ){ __isScale = true; __scale.y = v; }
	private void sz( float v ){ __isScale = true; __scale.z = v; }
	
	
	private void lrange( float v ){ __isLight = true; __Lrange = v; }
	private void lintensity( float v ){ __isLight = true; __Lintensity = v; }
	private void lspotAngle( float v ){ __isLight = true; __LspotAngle = v; }
		
	private void name( string v ){
		if( _goSel.ContainsKey( __go.name ) ) _goSel.Remove( __go.name );
		if( v!= null && _goSel.ContainsKey( v ) ) _goSel.Remove( v );
		__go.name = v == null ? "NULL" : v;
	}
	private void text( string v, int f ){ //set = 0, after = 1, before = 2
		if( f == 0 ) __go.guiText.text = v;
		else if( f == 1 ) __go.guiText.text += v;
		else if( f == 2 ) __go.guiText.text = v + __go.guiText.text;
	}
	private void msg( string v ){ __go.SendMessage( v ); }
	private void active( bool v ){ log("aa");__go.SetActive( v );log("abb"); }
	private void com( string v ){ __go.AddComponent( v ); }
	private void com_( string v ){
		GameObject.Destroy( __go.GetComponent( v ) );
	}
	
	private int tw( int i, object[] arg ){
		__twMove.Clear();
		__twRotate.Clear();
		__twScale.Clear();
		int j = i;
		for( ; j < arg.Length ; ){
			string t0 = (string)(arg[j++]);
			if( t0 == ")" ) break;
			switch( t0 ){
			case"p": __isTwM = true; __twMove["position"] = arg[j++]; break;
			case"path": __isTwM = true; __twMove[t0] = arg[j++]; break;	
			case"x": __isTwM = true; __twMove[t0] = arg[j++]; break;
			case"y": __isTwM = true; __twMove[t0] = arg[j++]; break;
			case"z": __isTwM = true; __twMove[t0] = arg[j++]; break;
			case"r": __isTwR = true; __twRotate["rotation"] = arg[j++]; break;
			case"rx": __isTwR = true; __twRotate["x"] = arg[j++]; break;
			case"ry": __isTwR = true; __twRotate["y"] = arg[j++]; break;
			case"rz": __isTwR = true; __twRotate["z"] = arg[j++]; break;
			case"s": __isTwS = true; __twScale["scale"] = arg[j++]; break;
			case"sx": __isTwS = true; __twScale["x"] = arg[j++]; break;
			case"sy": __isTwS = true; __twScale["y"] = arg[j++]; break;
			case"sz": __isTwS = true; __twScale["z"] = arg[j++]; break;
			default:
				if( t0[0] == 'o' ){
					if( __isTwM ) __twMove[t0] = arg[j++];
					else if( __isTwR ) __twRotate[t0] = arg[j++];
					else if( __isTwS ) __twScale[t0] = arg[j++];
				}else{
					__twMove[t0] = __twScale[t0] = __twRotate[t0] = arg[j++];
				}
				break;
			}
		}
		return j;
	}
	private void flush(){
		if( __isPos ){
			__isPos = false;
			p( __pos );
		}
		if( __isRotate ){
			__isRotate = false;
			r( __rotate );
		}
		if( __isScale ){
			__isScale = false;
			s( __scale );
		}
		if( __isColor ){
			__isColor = false;
			c( __color );
		}
		if( __isTwM ){
			__isTwM = false;
			iTween.MoveTo( __go, __twMove );
		}
		if( __isTwR ){
			__isTwR = false;
			iTween.RotateTo( __go, __twRotate );
		}
		if( __isTwS ){
			__isTwS = false;
			iTween.ScaleTo( __go, __twScale );
		}
		if( __isLight ){
			__isLight = false;
			Light l = __go.GetComponent( "Light" ) as Light;
			if( __Lrange != float.NegativeInfinity ){
				l.range = __Lrange;
				__Lrange = float.NegativeInfinity;
			}
			if( __Lintensity != float.NegativeInfinity ){
				l.intensity = __Lintensity;
				__Lintensity = float.NegativeInfinity;
			}
			if( __LspotAngle != float.NegativeInfinity ){
				l.spotAngle = __LspotAngle;
				__LspotAngle = float.NegativeInfinity;
			}
		}
	}
	private void destroy(){
		GameObject.Destroy( __go );
	}
}
