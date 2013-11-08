using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public partial class bs{
	
	public enum COM{
		Gtext,
		Gtexture,
		Mbox,
		Ldirect,
		Lspot,
		Lpoint,
		Larea
	}
	
	
	static private bs _goCurr = new bs();
	static private Vector3 _goInstance = new Vector3();
	static private Dictionary<string,GameObject> _goSel = new Dictionary<string,GameObject>();
	static private Dictionary<string,Texture> _texture = new Dictionary<string,Texture>();
	static public void texturLoad( string subfolder ){
		object[] t0 = Resources.LoadAll( subfolder );
		for( int i = 0 ; i < t0.Length ; i++ ){
			Texture t1 = t0[i] as Texture;
			_texture[t1.name] = t1;
		}
	}
	static public Texture texture( string v ){
		return _texture[v];
	}
	static public void texturLoad( MonoBehaviour m, string key, string url, A<Texture> end, A<float> prog = null, float t = 10f ){
		if( _texture.ContainsKey( key ) ){
			end( _texture[key] );
		}else{
			m.StartCoroutine( texturLoad_( key, url, end, prog, t ) );
		}
	}
	static private IEnumerator texturLoad_( string key, string v, A<Texture> end, A<float> prog, float t ){
		WWW www = new WWW( v );
		float elapsedTime = 0.0f;
		while( !www.isDone ){
			if( prog != null ) prog( www.progress );
			elapsedTime += Time.deltaTime;
			if( elapsedTime >= t) break;
			yield return null;
		}
		if( prog != null ) prog( 1f );
		if( !www.isDone || !string.IsNullOrEmpty(www.error) ){
			log( string.Format("Fail Whale!\n{0}", www.error) );
			end( null );
			yield break;
		}
		_texture[key] = www.texture;
		end( _texture[key] );
	}
	static public void goLoad( string subfolder ){
		object[] t0 = Resources.LoadAll( subfolder );
		for( int i = 0 ; i < t0.Length ; i++ ){
			GameObject t1 = (GameObject)(t0[i]);
			_goSel[t1.name] = t1;
		}
	}
	static private GameObject goSel( string selector ){
		if( !_goSel.ContainsKey( selector ) ){
			if( selector[0] == '@' ) selector = selector.Substring( 1 );
			_goSel[selector] = GameObject.Find( selector );
		}
		return _goSel[selector];
	}
	static public GameObject go( string g, params object[] arg ){
		return go( goSel( g ), arg );
	}
	static public GameObject go( string g, float x, float y, float z, params object[] arg ){
		_goInstance.x = x;
		_goInstance.y = y;
		_goInstance.z = z;
		return go( (GameObject)(GameObject.Instantiate( _goSel[g], _goInstance, Quaternion.identity )), arg );
	}
	static public GameObject go( COM type, params object[] arg ){
		bsRSCLoad();
		GameObject t0 = (GameObject)(GameObject.Instantiate( go( "bs.GO" ), V3[0], Quaternion.identity ));
		Light l0;
		switch( type ){
		case COM.Gtexture:
			t0.AddComponent( "GUITexture" );
			t0.AddComponent( "bsGUITexture" );
			t0.SendMessage( "init" );
			break;
		case COM.Gtext:
			t0.AddComponent( "GUIText" );
			break;
		case COM.Ldirect:
			l0 = t0.AddComponent( "Light" ) as Light;
			l0.type = LightType.Directional;
			break;
		case COM.Lspot:
			l0 = t0.AddComponent( "Light" ) as Light;
			l0.type = LightType.Spot;
			break;
		case COM.Lpoint:
			l0 = t0.AddComponent( "Light" ) as Light;
			l0.type = LightType.Point;
			break;
		case COM.Larea:
			l0 = t0.AddComponent( "Light" ) as Light;
			l0.type = LightType.Area;
			break;
		}
		return go( t0, arg );
	}
	static public GameObject go( GameObject g, params object[] arg ){
		if( arg == null ){
			GameObject.Destroy( g );
			return null;
		}else if( arg.Length == 0 ){
			return g;
		}else{
			_goCurr.init( g );
			return go( arg );
		}
	}
	public enum G{
		texture,uv,width,height,grid,active,
		x,y,left,right,top,bottom,center,middle,
		down0,down1,down2,up0,up1,up2,
		screen,pixel,screenPos,pixelPos,screenSize,pixelSize,
		pivot,lt,lm,lb,ct,cm,cb,rt,rm,rb
	}
	public enum L{
		range,spotAngle,intensity
	}
	static private GameObject go( object[] arg ){
		for( int i = 0 ; i < arg.Length ; ){
			object key = arg[i++];
			if( key is G ){
				switch( (G)key ){
				case G.x: _goCurr.__go.SendMessage( "x", arg[i++] ); break;
				case G.y: _goCurr.__go.SendMessage( "y", arg[i++] ); break;
				
				case G.texture: _goCurr.__go.SendMessage( "tex", _texture[(string)arg[i++]] ); break;
				case G.uv: _goCurr.__go.SendMessage( "uv", arg[i++] ); break;
				case G.width: _goCurr.__go.SendMessage( "w", arg[i++] ); break;
				case G.height: _goCurr.__go.SendMessage( "h", arg[i++] ); break;
				case G.active: _goCurr.__go.SendMessage( "active", arg[i++] ); break;
				case G.pivot: _goCurr.__go.SendMessage( "pivot", arg[i++] ); break;
				//case G.grid: _goCurr.__go.SendMessage( "grid", arg[i++] ); break;
					
				case G.down0: _goCurr.__go.SendMessage( "down0", arg[i++] ); break;
				case G.down1: _goCurr.__go.SendMessage( "down1", arg[i++] ); break;
				case G.down2: _goCurr.__go.SendMessage( "down2", arg[i++] ); break;
				case G.up0: _goCurr.__go.SendMessage( "up0", arg[i++] ); break;
				case G.up1: _goCurr.__go.SendMessage( "up1", arg[i++] ); break;
				case G.up2: _goCurr.__go.SendMessage( "up2", arg[i++] ); break;
					
				case G.screen: _goCurr.__go.SendMessage( "screen" ); break;
				case G.pixel: _goCurr.__go.SendMessage( "pixel" ); break;
				case G.screenPos: _goCurr.__go.SendMessage( "screenPos" ); break;
				case G.pixelPos: _goCurr.__go.SendMessage( "pixelPos" ); break;
				case G.screenSize: _goCurr.__go.SendMessage( "screenSize" ); break;
				case G.pixelSize: _goCurr.__go.SendMessage( "pixelSize" ); break;
				}
			}else if( key is L ){
				switch( (L)key ){
				case L.range: _goCurr.lrange( (float)arg[i++] ); break;
				case L.spotAngle: _goCurr.lspotAngle( (float)arg[i++] ); break;
				case L.intensity: _goCurr.lintensity( (float)arg[i++] ); break;
				}
			}else{
				switch( (string)key ){
				case"p": _goCurr.p( (Vector3)arg[i++] ); break;
				case"x": _goCurr.x( (float)arg[i++] ); break;
				case"y": _goCurr.y( (float)arg[i++] ); break;
				case"z": _goCurr.z( (float)arg[i++] ); break;
				case"r": _goCurr.r( (Vector3)arg[i++] ); break;
				case"rx": _goCurr.rx( (float)arg[i++] ); break;
				case"ry": _goCurr.ry( (float)arg[i++] ); break;
				case"rz": _goCurr.rz( (float)arg[i++] ); break;
				case"s": _goCurr.s( (Vector3)arg[i++] ); break;
				case"sx": _goCurr.sx( (float)arg[i++] ); break;
				case"sy": _goCurr.sy( (float)arg[i++] ); break;
				case"sz": _goCurr.sz( (float)arg[i++] ); break;
				case"name": _goCurr.name( (string)arg[i++] ); break;
				case"com": _goCurr.com( (string)arg[i++] ); break;
				case"com-": _goCurr.com_( (string)arg[i++] ); break;
				case"msg": _goCurr.msg( (string)arg[i++] ); break;
				case"C": _goCurr.c( (Color)arg[i++] ); break;
				case"R": _goCurr.cr( (float)arg[i++] ); break;
				case"G": _goCurr.cg( (float)arg[i++] ); break;
				case"B": _goCurr.cb( (float)arg[i++] ); break;
				case"A": _goCurr.ca( (float)arg[i++] ); break;
				case"brightness": _goCurr.brightness( (float)arg[i++] ); break;
				case"t(": i = _goCurr.tw( i, arg ); break;
				case"text": _goCurr.text( (string)arg[i++], 0 ); break;
				case"text+": _goCurr.text( (string)arg[i++], 1 ); break;
				case"+text": _goCurr.text( (string)arg[i++], 2 ); break;
				case"active": _goCurr.active( (bool)arg[i++] ); break;
				}
			}
		}
		_goCurr.flush();
		return _goCurr.__go;
	}
	//-------------------------------------------------------------------------------------
	static private Camera _c = Camera.main;
	static private Vector3 _c0 = new Vector3();
	static private Vector3 _c1 = new Vector3();
	static private float _scrWidth, _scrHeight, _scrW, _scrH, _scrRatio;
	static private SCREEN _scrMode, _scrAlign;
	public enum SCREEN{
		zoom,vertical,horizontal,
		middle,top,bottom,
		left,center,right
	}
	static public void screen( float w, float h, SCREEN mode, SCREEN align ){
		_scrWidth = w;
		_scrW = Screen.width / _scrWidth;
		_scrHeight = h;
		_scrH = Screen.height / _scrHeight;
		_scrMode = mode;
		_scrAlign = align;
		if( _scrMode == SCREEN.vertical ){
			_scrRatio = ( Screen.width - _scrWidth * _scrH ) * ( _scrAlign == SCREEN.left ? 0f : _scrAlign == SCREEN.center ? .5f : 1f );
		}else if( _scrMode == SCREEN.horizontal ){
			_scrRatio = ( Screen.height - _scrHeight * _scrW ) * ( _scrAlign == SCREEN.top ? 0f : _scrAlign == SCREEN.middle ? .5f : 1f );
		}
	}
	static public float screenX( float v ){
		if( _scrMode == SCREEN.vertical ){
			return v * _scrH + _scrRatio;
		}else{
			return v * _scrW;
		}
	}
	static public float screenY( float v ){
		if( _scrMode == SCREEN.horizontal ){
			return v *_scrW + _scrRatio;
		}else{
			return v * _scrH;
		}
	}
	static public float cWidth(){
		return _c.pixelWidth;
	}
	static public float cHeight(){
		return _c.pixelHeight;
	}
	static public Vector3 c3( Vector3 v ){
		return _c.ScreenToWorldPoint( v );
	}
	static public Vector3 c3( float x, float y ){
		_c0.x = x;
		_c0.y = y;
		_c0.z = -_c.transform.position.z;
		return _c.ScreenToWorldPoint( _c0 );
	}
	static public Vector3 c3( float x, float y, float z ){
		_c0.x = x;
		_c0.y = y;
		_c0.z = z;
		return _c.ScreenToWorldPoint( _c0 );
	}
	static public Vector3 c2( Vector3 v ){
		return _c.WorldToScreenPoint( v );
	}
	static public Vector3 c2( float x, float y, float z ){
		_c1.x = x;
		_c1.y = y;
		_c1.z = z;
		return _c.WorldToScreenPoint( _c1 );
	}
	static public Rect cBound(){
		return cBound( -_c.transform.position.z );
	}
	static public Rect cBound( float z ){
		_c0.x = 0;
		_c0.y = 0;
		_c0.z = z;
		Vector3 t1 = _c.ScreenToWorldPoint( _c0 );
		_c0.x = _c.pixelWidth;
		_c0.y = _c.pixelHeight;
		_c0.z = z;
		Vector3 t2 = _c.ScreenToWorldPoint( _c0 );
		return new Rect( t1.x, t2.y, t2.x - t1.x, t2.y - t1.y );
	}
}