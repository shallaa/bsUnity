using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public partial class V{

	static private Rect _bound3, _bound2;
	static private float _cell2, _cell3, _top, _x, _y, to3D;
	static private Vector3 _cellScale;

	static public void init(){
		_bound3 = bs.cBound();
		_cell3 = _bound3.width / M.col;
		_cellScale = new Vector3( _cell3, _cell3, _cell3 );
		_x = _bound3.x + _cell3 * .5f;
		_y = _bound3.y - _bound3.height + ( M.row - .5f )* _cell3;
		
		_bound2 = new Rect( 0, 0, bs.cWidth(), bs.c2( 0, _y, 0 ).y );
		_cell2 = bs.cWidth() / M.col;
		to3D = _cell3/_cell2;
		bs.screen( 480, 700, bs.SCREEN.vertical, bs.SCREEN.left );
		bs.texturLoad( "texture" );
		bs.goLoad( "block" );
	}
	
	
	
}
