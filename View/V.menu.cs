using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public partial class V : MonoBehaviour{
	
	static private float _menuX = 0f;
	static public void menu( bs.A<string> s ){
		bs.go( bs.COM.Gtexture, "name", "s1", 
			bs.G.texture, "menu"+M.loc, bs.G.uv, bs.rect(806f,0f,215f,139f), bs.G.screenSize,
			"x", 83f-215f*.5f,"y", 370f-139f*.5f, 
			bs.G.up0, s
		);
		bs.go( bs.COM.Gtexture, "name", "s2", 
			bs.G.texture, "menu"+M.loc, bs.G.uv, bs.rect(806f,139f,178f,137f), bs.G.screenSize,
			"x", 560f-178f*.5f,"y", 225f-137f*.5f, 
			bs.G.up0, s
		);
		bs.go( bs.COM.Gtexture, "name", "menu", 
			bs.G.texture, "menu"+M.loc, bs.G.uv, bs.rect(0f,0f,806f,512f), bs.G.screenSize,
			"x", 0f, "y", 0f, 
			bs.G.width, 700*806/512, bs.G.height, 700 
		);
		_menuX = 0f;
		bs.move0 += menuSlide;
		bs.up0 += menuSlideUp;
	}
	static private void menuSlide( string e, int id, float x, float y, float dx, float dy ){
		float t0 = _menuX;
		t0 += dx;
		if( t0 > 0 ){
			t0 = 0;
		}else if( t0 < 480f - 700f*806f/512f  ){
			t0 = 480f - 700f*806f/512f;
		}
		bs.go( "menu", "x", t0 );
		bs.go( "s1", "x", t0 + 83f-215f*.5f );
		bs.go( "s2", "x", t0 + 560f-178f*.5f );
	}
	static private void menuSlideUp( string e, int id, float x, float y, float dx, float dy ){
		_menuX += dx;
	}
	static public void menuOff(){
		bs.move0 -= menuSlide;
		bs.up0 -= menuSlideUp;
		bs.go( "menu", null );
		bs.go( "s1", null );
		bs.go( "s2", null );
	}
}
