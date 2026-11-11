using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/// ORIGINAL ORIGINAL ORIGINAL ORIGINAL ORIGINAL ORIGINAL ORIGINAL ORIGINAL ORIGINAL ORIGINAL ORIGINAL ORIGINAL ORIGINAL ORIGINAL ORIGINAL
/// ORIGINAL CODE BY ASSET CREATOR
/// ORIGINAL ORIGINAL ORIGINAL ORIGINAL ORIGINAL ORIGINAL ORIGINAL ORIGINAL ORIGINAL ORIGINAL ORIGINAL ORIGINAL ORIGINAL ORIGINAL ORIGINAL

//namespace MoveBackground_asset_test {
//public class MoveBackground : MonoBehaviour {

//	public float speed;
//	private float x;
//	public float PontoDeDestino;
//	public float PontoOriginal;


//	// Use this for initialization
//	void Start () {
//		//PontoOriginal = transform.position.x;
//	}

//	// Update is called once per frame
//	void Update () {

//		x = transform.position.x;
//		x += speed * Time.deltaTime;
//		transform.position = new Vector3 (x, transform.position.y, transform.position.z);

//		if (PontoDeDestino <0){
//			if (x <= PontoDeDestino){

//			x = PontoOriginal;
//			transform.position = new Vector3 (x, transform.position.y, transform.position.z);
//		}
//		}else{
//			if (x >= PontoDeDestino){

//			x = PontoOriginal;
//			transform.position = new Vector3 (x, transform.position.y, transform.position.z);
//		}
//		}
//	}
//}
//}


/// UPDATED UPDATED UPDATED UPDATED UPDATED UPDATED UPDATED UPDATED UPDATED UPDATED UPDATED UPDATED UPDATED UPDATED 
/// updated for this project ; WIP
/// UPDATED UPDATED UPDATED UPDATED UPDATED UPDATED UPDATED UPDATED UPDATED UPDATED UPDATED UPDATED UPDATED UPDATED 

//namespace MoveBackground_asset_test
//{
//	public class MoveBackground : MonoBehaviour
//	{

//		public float InitalSpeed;
//		private float x;
//		public float PontoDeDestino;
//		public float PontoOriginal;


//		public float CurrentSpeed;
//		enum DirectionState { GoingRight, GoingLeft, StandStill};
//		DirectionState CurrentDirection;
//		public bool GoRight;
//		public bool StandStill;

//		// Use this for initialization
//		void Start()
//		{
//			PontoOriginal = transform.position.x;
//		}

//		// Update is called once per frame
//		void Update()
//		{

//			x = transform.position.x;
//			x += InitalSpeed * Time.deltaTime;
//			transform.position = new Vector3(x, transform.position.y, transform.position.z);

//		}

//		// Check if speed is in negative; if yes, is going right
//		void checkSpeed()
//		{

//			if (CurrentSpeed < 0){
//				CurrentDirection = DirectionState.GoingRight;
//			}
//			if (CurrentSpeed == 0)
//			{
//				CurrentDirection = DirectionState.StandStill;
//			}
//			if (CurrentSpeed > 0)
//			{
//				CurrentDirection = DirectionState.GoingLeft;
//			}
//		}

//		void UpdateDirectionState()
//		{

//			switch (CurrentDirection) {

//				case DirectionState.GoingRight:
//					{

//                        if (PontoDeDestino < 0)
//                        {
//                            if (x <= PontoDeDestino)
//                            {

//                                x = PontoOriginal;
//                                transform.position = new Vector3(x, transform.position.y, transform.position.z);
//                            }
//                        }
//                        else
//                        {
//                            if (x >= PontoDeDestino)
//                            {

//                                x = PontoOriginal;
//                                transform.position = new Vector3(x, transform.position.y, transform.position.z);
//                            }
//                        }

//                        break;
//					}
//				case DirectionState.StandStill: 
//					{

//					break;
//					}
//                case DirectionState.GoingLeft:
//                    {
//                        if (PontoDeDestino > 0)
//                        {
//                            if (x <= PontoDeDestino)
//                            {

//                                x = PontoOriginal;
//                                transform.position = new Vector3(x, transform.position.y, transform.position.z);
//                            }
//                        }
//                        else
//                        {
//                            if (x <= PontoDeDestino)
//                            {

//                                x = PontoOriginal;
//                                transform.position = new Vector3(x, transform.position.y, transform.position.z);
//                            }
//                        }
//                        break;
//                    }
//            }



//		}

//	}
//}