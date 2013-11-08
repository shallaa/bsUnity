using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public partial class bs{

	public delegate void A();
	public delegate void A<A>( A a );
	public delegate void A<A,B>( A a, B b );
	public delegate void A<A,B,C>( A a, B b, C c );
	public delegate void A<A,B,C,D>( A a, B b, C c, D d );
	public delegate void A<A,B,C,D,E>( A a, B b, C c, D d, E e );
	public delegate void A<A,B,C,D,E,F>( A a, B b, C c, D d, E e, F f );
	public delegate T F<T>();
	public delegate T F<T,A>( A a );
	public delegate T F<T,A,B>( A a, B b );
	public delegate T F<T,A,B,C>( A a, B b, C c );
	public delegate T F<T,A,B,C,D>( A a, B b, C c, D d );
	public delegate T F<T,A,B,C,D,E>( A a, B b, C c, D d, E e );
	
	static public Vector3[] V3 = new Vector3[]{
		new Vector3( 0f, 0f, 0f ),
		new Vector3( 1f, 1f, 1f ),
		new Vector3( 2f, 2f, 2f ),
		new Vector3( 3f, 3f, 3f )
	};
	
	static private bool _isBsRSCLoaded = false;
	static private void bsRSCLoad(){
		if( _isBsRSCLoaded == false ){
			_isBsRSCLoaded = true;
			goLoad( "bs" );
		}
	}
	static public Rect rect( float x, float y, float w, float h ){
		return new Rect( x, y, w, h );
	}
	static private GameObject _L;
	static public void log( object log ){
		Debug.Log( log );
	}
	static public void LOG( object v ){
		if( _L == null ){
			bsRSCLoad();
			_L = go( COM.Gtext, "x", 0f, "y", 1f );
		}
		go( _L, "text+", v + "\n" );
	}
	
	static public int rand( int str, int end ){
		return UnityEngine.Random.Range( str, end + 1 );
	}
	static public float rand( float str, float end ){
		return UnityEngine.Random.Range( str, end );
	}
	
	static private JSON _json = new JSON();
	static public void json( MonoBehaviour m, string v, A<JSON> end, A<float> prog = null, float t = 10f ){
		m.StartCoroutine( json_( v, end, prog, t ) );
	}
	static private IEnumerator json_( string v, A<JSON> end, A<float> prog, float t ){
		WWW www = new WWW( v );
		float elapsedTime = 0.0f;
		while( !www.isDone ){
			if( prog != null ) prog( www.progress );
			elapsedTime += Time.deltaTime;
			if( elapsedTime >= t) break;
			yield return null;
		}
		if( prog != null ) prog( 1f );
		if( !www.isDone || !string.IsNullOrEmpty(www.error) ){
			log( string.Format("Fail Whale!\n{0}", www.error) );
			end( null );
			yield break;
		}
		_json.serialized = www.text;
		end( _json );
	}
	
}
