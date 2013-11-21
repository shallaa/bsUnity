using UnityEngine;
using System.Collections;

public partial class V : MonoBehaviour {

	static public void title( bs.A<string> s ){
		bs.go( bs.COM.Gtexture, "name", "logo", 
			bs.G.texture, "title", bs.G.uv, bs.rect(512f,0f,230f,87f), 
			bs.G.x, bs.G.center, "y", -87f,
			"t(", "y", 10f, "time", .5f, "delay", .5f, ")" 
		);
		/*
		bs.go( bs.COM.Gtexture, "name", "facebook", 
			bs.G.texture, "title", bs.G.uv, bs.rect(512f,87f,230f,62f), 
			bs.G.x, bs.G.center, bs.G.y, bs.G.middle, 
			bs.G.up0, f,
			"sx", 0f, "sy", 0f,
			"t(", "sx", 1f, "sy", 1f, "time", 1f, "delay", .5f, "easetype", iTween.EaseType.easeOutElastic, ")" 
		);
		*/
		bs.go( bs.COM.Gtexture, "name", "start", 
			bs.G.texture, "title", bs.G.uv, bs.rect(512f,149f,230f,62f), 
			bs.G.x, bs.G.center, "y", 700f, 
			bs.G.up0, s,
			"t(", "y", 700f-200f, "time", .5f, "delay", .5f, "easetype", iTween.EaseType.easeOutBack, ")" 
		);
		bs.go( bs.COM.Gtexture, "name", "title", 
			bs.G.texture, "title", bs.G.uv, bs.rect(0f,0f,512f,512f), bs.G.pixel, 
			"x", 0f, "y", 0f, 
			bs.G.width, Screen.width, bs.G.height, Screen.height 
		);
	}
	static public void titleOff(){
		bs.go( "logo", null );
		bs.go( "facebook", null );
		bs.go( "start", null );
		bs.go( "title", null );
	}
	
}
