using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public partial class C : MonoBehaviour {
	const string URL = "http://www.bsidesoft.com/unity/";
	private enum stat{
		start, title, map, menu, game
	}
	private stat _stat;
	
	void Start(){
		bs.mouseOn();
		_stat = stat.start;
		bs.json( this, URL + "0.js", (v)=>{
			M.init( v );
			V.init();
			title();
		} );
	}
	void Update(){
		if( _stat == stat.start || _stat == stat.title ) return;
		bs.Update();
	}
}