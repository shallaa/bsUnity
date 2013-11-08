using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public partial class C : MonoBehaviour {
	private void map(){
		_stat = stat.map;
		V.map( onMap );
	}
	public void onMap( string v ){
		bs.log("onMap");
		V.mapOff();
		int t0 = int.Parse( v.Substring( 1 ) );
		M.location( t0 );
		bs.texturLoad( this, "menu"+t0,  URL+"menu"+t0+".png", (t)=>{
			menu();	
		} );
	}
}
