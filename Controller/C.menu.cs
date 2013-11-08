using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public partial class C : MonoBehaviour {
	private void menu(){
		_stat = stat.menu;
		V.menu( onMenu );
	}
	public void onMenu( string v ){
		bs.log("onMenu");
		V.menuOff();
		game( int.Parse( v.Substring( 1 ) ) );
	}
	
}
