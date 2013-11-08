using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public partial class V : MonoBehaviour {
	
	static private float _mapX = 0f;
	static public void map( bs.A<string> s ){
		bs.go( bs.COM.Gtexture, "name", "s1", 
			bs.G.texture, "title", bs.G.uv, bs.rect(742f,0f,141f,94f), bs.G.screenSize,
			"x", 88f-70f*.5f,"y", 304f-47f*.5f, 
			bs.G.width, 70, bs.G.height, 47,
			bs.G.up0, s
		);
		bs.go( bs.COM.Gtexture, "name", "map", 
			bs.G.texture, "map", bs.G.uv, bs.rect(0f,0f,1024f,512f), bs.G.screenSize,
			"x", 0f, "y", 0f, 
			bs.G.width, 1400, bs.G.height, 700 
		);
		_mapX = 0f;
		bs.move0 += mapSlide;
		bs.up0 += mapSlideUp;
	}
	static private void mapSlide( string e, int id, float x, float y, float dx, float dy ){
		float t0 = _mapX;
		t0 += dx;
		if( t0 > 0 ){
			t0 = 0;
		}else if( t0 < 480f - 1400f  ){
			t0 = 480f - 1400f;
		}
		bs.go( "map", "x", t0 );
		bs.go( "s1", "x", t0 + 88f-70f*.5f );
	}
	static private void mapSlideUp( string e, int id, float x, float y, float dx, float dy ){
		_mapX += dx;
	}
	static public void mapOff(){
		bs.move0 -= mapSlide;
		bs.up0 -= mapSlideUp;
		bs.go( "map", null );
		bs.go( "s1", null );
	}
}
