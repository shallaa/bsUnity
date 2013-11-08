using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public partial class C : MonoBehaviour {
	
	private void game( int s ){
		_stat = stat.game;
		M.game( s );
		V.game();
		bs.down0 += onDown;
		bs.move0 += onMove;
		bs.up0 += onUp;
	}
	
	private List<int> _select = new List<int>();
	private int _curr;
	
	private M.P _pos = new M.P();
	private bool _isSelect = false;
	private void onDown( string e, int id, float x, float y, float dx, float dy ){
		bs.log ("down");
		M.P p = V.mapPos( x, y );
		if( p.r < 0 || p.c < 0 || p.r >= M.row || p.c >= M.col || M.map[p.r, p.c] < 1 ){
			_isSelect = false;
		}else{
			_pos.r = p.r;
			_pos.c = p.c;
			_isSelect = true;
			_select.Clear();
			_select.Add( p.r );
			_select.Add( p.c );
			_curr = M.map[p.r, p.c];
			V.selected( _pos.c+"_"+_pos.r );
		}
	}
	private void onMove( string e, int id, float x, float y, float dx, float dy ){
		if( !_isSelect ) return;
		M.P p = V.mapPos( x, y );
		if( ( _pos.r == p.r && _pos.c == p.c ) || p.r < 0 || p.c < 0 || p.r >= M.row || p.c >= M.col || _curr != M.map[p.r, p.c] ) return;
		int c = _select[_select.Count - 1], r = _select[_select.Count - 2], odd = ( r % 2 ) - 1;
		if( ( p.c == c + odd && p.r == r - 1 ) || ( p.c == c + 1 + odd && p.r == r - 1 ) ||
			( p.c == c - 1 && p.r == r ) || ( p.c == c + 1 && p.r == r ) ||  
			( p.c == c + odd && p.r == r + 1 ) || ( p.c == c + 1 + odd && p.r == r + 1 )
		){
			if( _select.Count > 2 && p.c == _select[_select.Count - 3] && p.r == _select[_select.Count - 4] ){
				V.unselected( _pos );
				_pos.r = p.r;
				_pos.c = p.c;
				_select.RemoveAt( _select.Count - 1 );
				_select.RemoveAt( _select.Count - 1 );
			}else{
				for( int i = 0 ; i < _select.Count ; ){
					r = _select[i++];
					c = _select[i++];
					if( r == p.r && c == p.c ) return;
				}
				V.selectCount( _select, M.selectCount( _select.Count ) );
				_pos.r = p.r;
				_pos.c = p.c;
				_select.Add( p.r );
				_select.Add( p.c );
				V.selected( _pos );
			}
		}
	}
	private void onUp( string e, int id, float x, float y, float dx, float dy ){
		for( int i = 0 ; i < _select.Count ; ){
			_pos.r = _select[i++];
			_pos.c = _select[i++];
			V.unselected( _pos );
		}
		int t0 = V.unselected( M.unselected( _select ) );
		if( t0 > 0 ){
			V.fall( M.fall() );
			StartCoroutine( "fill" );
		}else{
			_isSelect = false;
		}
	}
	IEnumerator fill(){
		yield return new WaitForSeconds( .3f );
		V.fill( M.fill() );
		V.score();
		yield return new WaitForSeconds( .3f );
		_isSelect = false;
	}
}
