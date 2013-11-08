using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class M{
	public class P{
		public int c, r;
	}
	static public bs.JSON data;
	static public int col, row;
	static public int[,] mapFill;
	static public int stg = 0, loc = 0;
	//stage별
	static public int score = 0;
	static public List<int> block = new List<int>();
	static public int[,] map;
	static private Dictionary<int,int> combo = new Dictionary<int,int>();
	
	
	static public void init( bs.JSON v ){
		data = v;
		col = v.ToInt( "col" );
		row = v.ToInt( "row" );
		mapFill = new int[col,row];
		map = new int[col,row];
	}
	static public void location( int v ){
		loc = v;
	}
	static public void game( int v ){
		int[] t0;
		int i, j, k;
		stg = v;
		block.Clear();
		t0 = data.ToArray<int>( "block" + v );
		for( i = 0 ; i < t0.Length ; ){
			block.Add( t0[i++] );
		}

		combo.Clear();
		t0 = data.ToArray<int>( "combo" + v );
		for( i = 0 ; i < t0.Length ; ){
			combo.Add( t0[i++], t0[i++] );
		}

		t0 = data.ToArray<int>( "map" + v );
		for( i = k = 0 ; i < row ; i++ ){
			for( j = 0 ; j < col ; j++ ){
				map[i,j] = t0[k++];
				if( map[i,j] == 0 ) map[i,j] = block[bs.rand( 0, block.Count - 1 )];
			}
		}
	}
	static public int selectCount( int c ){
		c /= 2; //10-5, 12-6
		c -= 1; //5-3, 6-4
		c /= 3; //3-1, 4-1
		return combo.ContainsKey( c ) ? combo[c] : -1 ;
	}
	static public List<int> unselected( List<int> l ){
		if( l.Count < 6 ) return null;
		for( int i = 0 ; i < l.Count ; ){
			int r = l[i++];
			int c = l[i++];
			map[r,c] = 0;
		}
		score += l.Count / 2;
		return l;
	}
	static public int[,] fall(){
		for( int c = 0 ; c < col ; c++ ){
			for( int r = row - 1 ; r > -1 ; r-- ){
				if( map[r,c] > 0 ){
					int i = r + 1, j = r;
					for(  ; i < row ; i++ ){
						if( map[i,c] == 0 ){
							j = i;
						}else if( map[i,c] > 0 ){
							break;
						}
					}
					if( j == r ){
						mapFill[r,c] = 0;
					}else{
						mapFill[r,c] = j - r;
						map[j,c] = map[r,c];
						map[r,c] = 0;
					}
				}else{
					mapFill[r,c] = 0;
				}
			}
		}
		return mapFill;
	}
	static public int[,] fill(){
		int count = block.Count - 2;
		for( int c = 0 ; c < col ; c++ ){
			for( int r = row - 1 ; r > -1 ; r-- ){
				if( map[r,c] == 0 ){
					mapFill[r,c] = map[r,c] = block[bs.rand( 0, count )];
				}else{
					mapFill[r,c] = 0;
				}
			}
		}
		return mapFill;
	}
}
