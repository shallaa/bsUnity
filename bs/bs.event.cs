using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public partial class bs{

	static private bsEvent _touch = new bsEvent();
	static private Vector2[] _touchDelta = new Vector2[5];
	static public event A<bsEvent> touchstart, touchend, touchmove;
	static public event A<string,int,float,float,float,float> down0, down1, down2, down3, down4,up0, up1, up2, up3, up4,move0, move1, move2, move3, move4;
	
	static private bool _touchStat = false;
	static public void touchOn(){ _touchStat = true; }
	static public void touchOff(){ _touchStat = false; }
	static private void touch(){
		int count = Input.touchCount;
		_touch.length = count;
		bool s, e, m;
		s = m = e = false;
		for( int i = 0 ; i < count ; i++ ){
			Touch t0 = Input.GetTouch(i);
			int id = t0.fingerId;
			_touch.touch[id] = t0;
			float x, y, dx, dy;
			x = t0.position.x;
			y = t0.position.y;
			if( t0.phase == TouchPhase.Began ){
				_touchDelta[id] = t0.position;
				dx = dy = 0;
				if( id == 0 && down0 != null ) down0( "down", id, x, y, dx, dy );
				else if( id == 1 && down1 != null ) down1( "down", id, x, y, dx, dy );
				else if( id == 2 && down2 != null ) down2( "down", id, x, y, dx, dy );
				else if( id == 3 && down3 != null ) down3( "down", id, x, y, dx, dy );
				else if( id == 4 && down4 != null ) down4( "down", id, x, y, dx, dy );
				s = true;
			}else{
				dx = x - _touchDelta[id].x;
				dy = y - _touchDelta[id].y;
				if( t0.phase == TouchPhase.Moved ){
					m = true;
					if( id == 0 && move0 != null ) move0( "move", id, x, y, dx, dy );
					else if( id == 1 && move1 != null ) move1( "move", id, x, y, dx, dy );
					else if( id == 2 && move2 != null ) move2( "move", id, x, y, dx, dy );
					else if( id == 3 &&  move3 != null ) move3( "move", id, x, y, dx, dy );
					else if( id == 4 && move4 != null ) move4( "move", id, x, y, dx, dy );
				}else if( t0.phase == TouchPhase.Ended ){
					e = true;
					if( id == 0 && up0 != null ) up0( "up", id, x, y, dx, dy );
					else if( id == 1 && up1 != null ) up1( "up", id, x, y, dx, dy );
					else if( id == 2 && up2 != null ) up2( "up", id, x, y, dx, dy );
					else if( id == 3 && up3 != null ) up3( "up", id, x, y, dx, dy );
					else if( id == 4 && up4 != null ) up4( "up", id, x, y, dx, dy );
				}
			}
		}
		if( s && touchstart != null ) touchstart( _touch );
		if( e && touchmove != null ) touchend( _touch );
		if( m && touchend != null ) touchmove( _touch );
	}
	
	static private bool _mouseStat = false;
	static private Vector3[] _mouseDelta = new Vector3[3];
	static private bool[] _mouseButton = new bool[]{false,false,false};
	static public void mouseOn(){ _mouseStat = true; }
	static public void mouseOff(){ _mouseStat = false; }
	static private void mouse(){
		Vector3 pos = Input.mousePosition;
		float x = pos.x, y = pos.y;
		for( int i = 0 ; i < 3 ; i++ ){
			bool t0 = Input.GetMouseButton(i);
			if( t0 != _mouseButton[i] ){
				if( t0 ){
					if( i == 0 && down0 != null ) down0( "down", i, x, y, 0, 0 );
					else if( i == 1 && down1 != null ) down1( "down", i, x, y, 0, 0 );
					else if( i == 2 && down2 != null ) down2( "down", i, x, y, 0, 0 );
					_mouseDelta[i] = pos;
				}else{
					if( i == 0 && up0 != null ) up0( "up", i, x, y, x - _mouseDelta[i].x, y - _mouseDelta[i].y );
					else if( i == 1 && up1 != null ) up1( "up", i, x, y, x - _mouseDelta[i].x, y - _mouseDelta[i].y );
					else if( i == 2 && up2 != null ) up2( "up", i, x, y, x - _mouseDelta[i].x, y - _mouseDelta[i].y );
					
				}
				_mouseButton[i] = t0;
			}else if( t0 ){
				if( i == 0 && move0 != null ) move0( "move", i, x, y, x - _mouseDelta[i].x, y - _mouseDelta[i].y );
				else if( i == 1 && move1 != null ) move1( "move", i, x, y, x - _mouseDelta[i].x, y - _mouseDelta[i].y );
				else if( i == 2 && move2 != null ) move2( "move", i, x, y, x - _mouseDelta[i].x, y - _mouseDelta[i].y );
			}
		}
	}
	static public void Update(){
		if( _touchStat ) touch();
		if( _mouseStat ) mouse();
	}
}
