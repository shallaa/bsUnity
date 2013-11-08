using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public partial class V : MonoBehaviour {
	
	static private GameObject _select;
	
	static public void game(){
		for( int j = 0 ; j < M.row ; j++ ){
			for( int i = 0 ; i < M.col ; i++ ){
				int t0 = M.map[j, i];
				if( t0 > 0 ){
					bs.go( "R" + t0, 0, 0, _cell3 * .5f, "x", i * _cell3 + _x + ( j % 2 ) * _cell3 * .5f, "y", _y - j * _cell3, "s", _cellScale, "name", i + "_" + j  );
				}
			}
		}
		Vector3[] t1 = new Vector3[]{
			new Vector3(_bound3.x,_y,-10),
			new Vector3(-_bound3.x,_y,-10),
			new Vector3(-_bound3.x,_bound3.y - _bound3.height,-10),
			new Vector3(_bound3.x,_bound3.y - _bound3.height,-10),
			new Vector3(_bound3.x,_y,-10)
		};
		Vector3[] t2 = new Vector3[]{
			new Vector3(-_bound3.x,_bound3.y - _bound3.height,-10),
			new Vector3(-_bound3.x,_y,-10),
			new Vector3(_bound3.x,_y,-10),
			new Vector3(_bound3.x,_bound3.y - _bound3.height,-10),
			new Vector3(-_bound3.x,_bound3.y - _bound3.height,-10)
		};
		bs.go( bs.COM.Ldirect, "name", "Direct0", "rx", 50f, "ry", 330f, bs.L.intensity, .5f );
		bs.go( bs.COM.Lspot, "name", "Spotlight0", "p", t1[0], bs.L.range, 20f, bs.L.spotAngle, 40f,
			"t(", "easetype", iTween.EaseType.linear, "time", 7f, "path", t1, "looptype", iTween.LoopType.loop, ")" );
		bs.go( bs.COM.Lspot, "name", "Spotlight1", "p", t2[0], bs.L.range, 20f, bs.L.spotAngle, 40f,
			"t(", "easetype", iTween.EaseType.linear, "time", 5f, "path", t2, "looptype", iTween.LoopType.loop, ")" );
		bs.go( bs.COM.Gtext, "name", "Score", "x", -_bound3.x + _cell3, "y", _bound3.y - _cell3 );
		score();
	}
	static public void score(){
		bs.log ( M.score );
		GameObject.Find( "Score" ).guiText.text = M.score + "";
	}
	static private Color _selectColor = Color.clear;
	static private M.P _mapPos = new M.P();
	static public M.P mapPos( float x, float y ){
		_mapPos.r = M.row - (int)( y / _cell2 ) - 1;
		_mapPos.c = (int)( (x-(_mapPos.r%2)*_cell2*.5f) / _cell2 );
		return _mapPos;
	}
	
	static private int _selectCombo = -1;
	static private Color _comboColor;
	static private float _comboScale = 1f;
	
	static public void selected( string p){
		bs.go( p, "com", "rotater" );
		_comboScale = 1f;
		_comboColor = _selectColor = bs.go( p ).renderer.material.color;
	}
	static public void selected( M.P p ){
		string t0 = p.c+"_"+p.r;
		bs.go( t0, "com", "rotater" );
		if( _comboColor != _selectColor ) bs.go( t0, "C", _comboColor );
		if( _comboScale > 1f ) bs.go( t0, "s", _cellScale * _comboScale );
	}
	static public void selectCount( List<int> l, int t ){
		if( t < 0 || _selectCombo == t ) return;
		_selectCombo = t;
		_comboColor = _selectColor;
		switch( t ){
		case 100: _comboColor.r *= 1.1f; _comboColor.g *= 1.2f; _comboColor.b *= 1.2f; _comboScale = 1.1f; break;
		case 101: _comboColor.r *= 1.5f; _comboColor.g *= 1.5f; _comboColor.b *= 1.5f; _comboScale = 1.2f; break;
		case 102: _comboColor.r *= 2f; _comboColor.g *= 2f; _comboColor.b *= 2f; _comboScale = 1.3f; break;
		default: _comboColor.r *= 2f; _comboColor.g *= 2f; _comboColor.b *= 2f; _comboScale = 1.3f; break;
		}
		for( int i = 0 ; i < l.Count ; ){
			int r = l[i++];
			int c = l[i++];
			bs.go( c+"_"+r, "C", _comboColor, "s", _cellScale * _comboScale );
		}
	}
	static public void unselected( M.P p, bool isUp = false ){
		bs.go( p.c+"_"+p.r, "com-", "rotater", "r", bs.V3[0], "C", _selectColor, "s", _cellScale );
	}
	static public int unselected( List<int> l ){
		_selectColor = Color.clear;
		_selectCombo = -1;
		if( l == null ) return 0;
		for( int i = 0 ; i < l.Count ; ){
			int r = l[i++], c = l[i++];
			bs.go( c + "_" + r, "name", null, 
				"t(", "time", .3f, "x", bs.rand( _bound3.x + _cell3, _bound3.x + _cell3 * 4 ), "y", bs.rand( _bound3.y - _cell3, _bound3.y - _cell3 * 4 ), "z", bs.rand ( -1f, 1f ), "s", _cellScale * .3f, ")" );
		}
		return l.Count / 2;
	}
	static public void fall( int[,] m ){
		for( int c = 0 ; c < M.col ; c++ ){
			for( int r = M.row - 1 ; r > -1 ; r-- ){
				int t0 = m[r,c];
				if( t0 > 0 ){
					bs.go( c+"_"+r, "name", c+"_"+(r+t0), "t(", "time", .3f, "x", c * _cell3 + _x + ( (r+t0) % 2 ) * _cell3 * .5f, "y", _y - (r+t0) * _cell3, ")" );
				}
			}
		}
	}
	static public void fill( int[,] m ){
		for( int c = 0 ; c < M.col ; c++ ){
			for( int r = M.row - 1 ; r > -1 ; r-- ){
				int t0 = m[r,c];
				if( t0 > 0 ){
					bs.go(
						"R" + t0, 0, 0, _cell3 * .5f, "name", c + "_" + r, "x", c * _cell3 + _x, "y", _y + _cell3, "s", _cellScale, 
						"t(", "time", .3f, "x", c * _cell3 + _x + ( r % 2 ) * _cell3 * .5f, "y", _y - r * _cell3, ")"
					);
				}
			}
		}
	}
}
