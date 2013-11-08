using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Facebook;

public partial class C : MonoBehaviour {
	
	private void title(){
		_stat = stat.title;
		V.title( onTitleStart, onTitleFacebook );
	}
	public void onTitleStart( string v ){
		bs.log("onTitleStart");
		V.titleOff();
		map();
	}
	public void onTitleFacebook( string v ){
		bs.log("onTitleFacebook");
		FB.Init( ()=>{
			bs.LOG( "b" );
			if( FB.IsLoggedIn ){
				bs.LOG( "c" );
				bs.LOG( "fbstat:"+FB.UserId );
				Logined();
			}else{
				bs.LOG( "d" );
				FB.Login( "", (f)=>{
					bs.LOG( "e" );
					bs.LOG( "login:" + FB.UserId +":"+FB.IsLoggedIn+":"+FB.AccessToken);
					Logined();
				} );
			}
		} );
	}
}
