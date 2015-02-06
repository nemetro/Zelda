using UnityEngine;
using System.Collections;

public class puzzle : MonoBehaviour {

	public static direction facing = direction.north;
	public GameObject raft, raftPre;
	public GameObject southW, eastW, raftBlock;
	public Material water, noWater;
	public GameObject hydrant;
	private direction lastfacing = direction.down;
	private bool floating = false;
	private GameObject eastWPre;
	private Vector3 blockPos, unblockPos;

	void Start(){
		arrowTiles.hydrant = hydrant;
		blockPos = new Vector3(raftBlock.transform.position.x, raftBlock.transform.position.y, raftBlock.transform.position.z);
		unblockPos = new Vector3(raftBlock.transform.position.x, raftBlock.transform.position.y, blockPos.z + 4);
	}

	void Update(){
		if(lastfacing != facing){
			lastfacing = facing;

			switch(facing){
			case direction.north:
				eastW.renderer.material = noWater;
				if(floating){
					floating = false;
					southW.renderer.material = noWater;
					Vector3 newpos = new Vector3(raft.transform.position.x, raft.transform.position.y, raft.transform.position.z);
					newpos.y = 28.76f;

					GameObject nR = Instantiate(raftPre, newpos, Quaternion.Euler(0, 0, 30f)) as GameObject;
					Destroy (raft);
					raft = nR;

					raftBlock.transform.position = blockPos;
				}
				break;
			case direction.east:
				eastW.renderer.material = water;
				if(floating){
					floating = false;
					southW.renderer.material = noWater;
					Vector3 newpos = new Vector3(raft.transform.position.x, raft.transform.position.y, raft.transform.position.z);
					newpos.y = 28.76f;

					GameObject nR = Instantiate(raftPre, newpos, Quaternion.Euler(0, 0, 30f)) as GameObject;
					Destroy (raft);
					raft = nR;

					raftBlock.transform.position = blockPos;
				}
				break;
			case direction.west:
				eastW.renderer.material = noWater;
				if(floating){
					floating = false;
					southW.renderer.material = noWater;
					Vector3 newpos = new Vector3(raft.transform.position.x, raft.transform.position.y, raft.transform.position.z);
					newpos.y = 28.76f;

					GameObject nR = Instantiate(raftPre, newpos, Quaternion.Euler(0, 0, 30f)) as GameObject;
					Destroy (raft);
					raft = nR;

					raftBlock.transform.position = blockPos;
				}
				break;
			case direction.south:
				eastW.renderer.material = noWater;
				if(!floating){
					floating = true;
					southW.renderer.material = water;
					Vector3 newpos = new Vector3(raft.transform.position.x, raft.transform.position.y, raft.transform.position.z);
					newpos.y = 29f;

					GameObject nR = Instantiate(raftPre, newpos, Quaternion.Euler(0, 0, 0f)) as GameObject;
					Destroy (raft);
					raft = nR;

					raftBlock.transform.position = unblockPos;
				}
				break;
			default: break;
			}
		}
	}
}
