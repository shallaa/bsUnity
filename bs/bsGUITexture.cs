using UnityEngine;
using System.Collections;

public class bsGUITexture : MonoBehaviour {
	
	static private Vector2 mousePos = new Vector2();
	
	public Rect _pos = new Rect( 0, 0, 100, 100 ),
				_uv = new Rect( 0, 0, 1, 1 );
	public Vector2 _pivot = new Vector2( 50, 50 );
	public float _pivotX = .5f, _pivotY = .5f;
	public int _gL = 0, _gR = 0, _gT = 0, _gB = 0;
	public Texture _texture;
	public bool _isActive = true;
	
	public bool _isScreenPos = true,
				_isScreenSize = false;
	public float rx, ry;
	private bs.A _calX, _calY;
	
	private bool _isMouse = false, _isOver = false;
	private bool[] _mouseButton = new bool[]{false,false,false};
	private bs.A<string> _down0, _up0, _down1, _up1, _down2, _up2, _over, _out;
	
	public void init(){
		_calX = noneX;
		_calY = noneY;
	}
	public void ov( bs.A<string> v ){ _isMouse = v != null; _over = v; }
	public void ou( bs.A<string> v ){ _isMouse = v != null; _out = v; }
	public void down0( bs.A<string> v ){ _isMouse = v != null; _down0 = v; }
	public void down1( bs.A<string> v ){ _isMouse = v != null; _down1 = v; }
	public void down2( bs.A<string> v ){ _isMouse = v != null; _down2 = v; }
	public void up0( bs.A<string> v ){ _isMouse = v != null; _up0 = v; }
	public void up1( bs.A<string> v ){ _isMouse = v != null; _up1 = v; }
	public void up2( bs.A<string> v ){ _isMouse = v != null; _up2 = v; }
	
	public void screen(){ _isScreenPos = _isScreenSize = true; }
	public void pixel(){ _isScreenPos = _isScreenSize = false; }
	public void screenPos(){ _isScreenPos = true; }
	public void pixelPos(){ _isScreenPos = false; }
	public void screenSize(){ _isScreenSize = true; }
	public void pixelSize(){ _isScreenSize = false; }
	public void active( bool v ){ _isActive = v; }
	
	public void tex( Texture t ){ _texture = t; }
	public void uv( Rect r ){
		_uv.x = r.x / _texture.width;
		_uv.y = ( _texture.height - r.height - r.y ) / _texture.height;
		_uv.width = r.width / _texture.width;
		_uv.height = r.height / _texture.height;
		w( r.width );
		h( r.height );
	}
	public void pivot( bs.G v ){
		switch( v ){
		case bs.G.lt: _pivotX = 0f; _pivotY = 0f; break;
		case bs.G.lm: _pivotX = 0f; _pivotY = .5f; break;
		case bs.G.lb: _pivotX = 0f; _pivotY = 1f; break;
		case bs.G.rt: _pivotX = 1f; _pivotY = 0f; break;
		case bs.G.rm: _pivotX = 1f; _pivotY = .5f; break;
		case bs.G.rb: _pivotX = 1f; _pivotY = 1f; break;
		case bs.G.ct: _pivotX = .5f; _pivotY = 0f; break;
		case bs.G.cm: _pivotX = .5f; _pivotY = .5f; break;
		case bs.G.cb: _pivotX = .5f; _pivotY = 1f; break;
		}
	}
	public void w( float v ){ _pos.width = v; }
	public void h( float v ){ _pos.height = v; }
	public void x( object v ){
		if( v == null ) _calX = noneX;
		else if( v is bs.G ){
			switch( (bs.G)v ){
			case bs.G.left: _calX = leftX; break;
			case bs.G.right: _calX = rightX; break;
			case bs.G.center: _calX = centerX; break;
			}
		}else{
			rx = (float)v;
			_calX = perX;
		}
	}
	public void y( object v ){
		if( v == null ) _calY = noneY;
		else if( v is bs.G ){
			switch( (bs.G)v ){
			case bs.G.top: _calY = topY; break;
			case bs.G.bottom: _calY = bottomY; break;
			case bs.G.middle: _calY = middleY; break;
			}
		}else{
			ry = (float)v;
			_calY = perY;
		}
	}
	
	void OnGUI(){
		if( !_isActive || _texture == null ) return;
		
		float w = _pos.width, h = _pos.height;
		Matrix4x4 matrixBackup = GUI.matrix;
		pos();
		mouse();
		GUIUtility.RotateAroundPivot( transform.eulerAngles.x, _pivot );
		Graphics.DrawTexture( _pos, _texture, _uv, _gL, _gR, _gT, _gB );
		GUI.matrix = matrixBackup;
		_pos.width = w;
		_pos.height = h;
		
	}
	
	private void noneX(){ _pos.x = transform.position.x; if( _isScreenPos ) _pos.x = bs.screenX( _pos.x ); }
	private void noneY(){ _pos.y = transform.position.y; if( _isScreenPos ) _pos.y = bs.screenY( _pos.y ); }
	
	private void perX(){ _pos.x = Screen.width * (float)rx; }
	private void perY(){ _pos.y = Screen.height * (float)ry; }
	
	private void leftX(){ _pos.x = 0; }
	private void topY(){ _pos.y = 0; }
	
	private void rightX(){ _pos.x = Screen.width - _pos.width; }
	private void bottomY(){ _pos.y = Screen.height - _pos.height; }
	
	private void centerX(){ _pos.x = ( Screen.width - _pos.width ) * .5f; }
	private void middleY(){ _pos.y = ( Screen.height - _pos.height ) * .5f; }
	
	private void pos(){
		_pos.width *= transform.localScale.x;
		_pos.height *= transform.localScale.y;
		if( _isScreenSize ){
			_pos.width = bs.screenX( _pos.width );
			_pos.height = bs.screenY( _pos.height );
		}
		_calX();
		_calY();
		 _pivot.x = _pos.x + _pos.width * _pivotX;
		 _pivot.y = _pos.y + _pos.height * _pivotY;
	}
	private void mouse(){
		if( !_isMouse ) return;
		mousePos.x = Input.mousePosition.x;
		mousePos.y = Screen.height - Input.mousePosition.y;
		if( _pos.Contains( mousePos ) ){
			if( !_isOver ){
				_isOver = true;
				if( _over != null ) _over( name );
			}
			for( int i = 0 ; i < 3 ; i++ ){
				bool t0 = Input.GetMouseButton(i);
				if( t0 != _mouseButton[i] ){
					if( t0 ){
						if( i == 0 && _down0 != null ) _down0( name );
						else if( i == 1 && _down1 != null ) _down1( name );
						else if( i == 2 && _down2 != null ) _down2( name );
					}else{
						if( i == 0 && _up0 != null ) _up0( name );
						else if( i == 1 && _up1 != null ) _up1( name );
						else if( i == 2 && _up2 != null ) _up2( name );
					}
					_mouseButton[i] = t0;
				}
			}
		}else if( _isOver && _out != null ){
			_out( name );
		}
	}
}