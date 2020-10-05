using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
	public GameObject shootEffect;
	public GameObject shootPoint;

	public Reload reload;
    public float viewRadius;
	[Range(0,360)]
	public float viewAngle;

	public LayerMask targetMask;
	public LayerMask obstacleMask;

	public List<Transform> visibleTargets = new List<Transform>();

	public float meshResolution;
	public int edgeResolveIterations;
	public float edgeDstThreshold;

	public MeshFilter viewMeshFilter;
	Mesh viewMesh;
    private IEnumerator coroutine;

	string targetTag = "Player";

	public AudioClip shootAudio;
    [Range(0,1)]
    public float shootAudioVolume;


	void OnEnable()
    {
		//coroutine = FindTargetsWithDelay(.2f);
		//StartCoroutine(coroutine);
    }

	void Start() {
		viewMesh = new Mesh();
		viewMesh.name = "View Mesh";
		viewMeshFilter.mesh = viewMesh;
	}

	void LateUpdate() {
		//DrawFieldOfView();
	}

	void DrawFieldOfView() {
		int stepCount = Mathf.RoundToInt(viewAngle * meshResolution);
		float stepAngleSize = viewAngle / stepCount;
		List<Vector3> viewPoints = new List<Vector3> ();

		RaycastHit2D hit = new RaycastHit2D();
		for (int i = 0; i <= stepCount; i++) {
			float angle = transform.eulerAngles.y - viewAngle / 2 + stepAngleSize * i;
			Vector3 dir = DirFromAngle(angle, false);
			hit = Physics2D.Raycast(transform.position,dir,viewRadius,obstacleMask);

			if (hit.collider == null){
				viewPoints.Add (transform.position + dir.normalized * viewRadius);
			}
			else{
				viewPoints.Add (transform.position + dir.normalized * hit.distance);
			}
		}

		int vertexCount = viewPoints.Count + 1;
		Vector3[] vertices = new Vector3[vertexCount];
		int[] triangles = new int[(vertexCount-2) * 3];

		vertices [0] = Vector3.zero;
		for (int i = 0; i < vertexCount - 1; i++) {
			vertices [i + 1] = transform.InverseTransformPoint(viewPoints [i]);

			if (i < vertexCount - 2) {
				triangles [i * 3] = 0;
				triangles [i * 3 + 1] = i + 1;
				triangles [i * 3 + 2] = i + 2;
			}
		}

		viewMesh.Clear ();

		viewMesh.vertices = vertices;
		viewMesh.triangles = triangles;
		viewMesh.RecalculateNormals ();
	}

	void FixedUpdate()
	{
		FindVisibleTargets();
	}

	IEnumerator FindTargetsWithDelay(float delay) {
		while (true) {
			yield return new WaitForSeconds (delay);
			FindVisibleTargets ();
		}
	}

	void FindVisibleTargets() {
		visibleTargets.Clear ();
		Collider2D[] targetsInViewRadius = Physics2D.OverlapCircleAll(transform.position, viewRadius);

		for (int i = 0; i < targetsInViewRadius.Length; i++) {
			Transform target = targetsInViewRadius [i].transform;
			Vector2 dirToTarget = new Vector2(target.position.x - transform.position.x, target.position.y - transform.position.y);
			if (Vector2.Angle (transform.right, dirToTarget) < viewAngle / 2) {
				float dstToTarget = Vector2.Distance (transform.position, target.position);

				if (!Physics2D.Raycast (transform.position, dirToTarget, dstToTarget, obstacleMask)) {
					if(target.tag == targetTag){
						AudioManager.PlayClip(shootAudio,shootAudioVolume);
						Debug.Log("Got em");
						visibleTargets.Add(target);

						//Reload
						viewMesh.Clear ();
						reload.ReloadQueue();
						target.gameObject.GetComponent<CharController>().TriggerDeath();

						Vector3 dir = new Vector3(dirToTarget.x, 0,0).normalized;
						Vector3 rotatedDir = Quaternion.Euler(0, 0, 90) * -dir;

						Quaternion rotation = Quaternion.LookRotation(Vector3.forward, rotatedDir);
						Instantiate(shootEffect, shootPoint.transform.position, shootPoint.transform.rotation * rotation);
						//StopCoroutine(coroutine);
					}
				}
			}
		}
	}


	public Vector2 DirFromAngle(float angleInDegrees, bool angleIsGlobal) {
		if (!angleIsGlobal) {
			angleInDegrees += transform.eulerAngles.z;
		}
		return new Vector2(Mathf.Cos(angleInDegrees * Mathf.Deg2Rad),Mathf.Sin(angleInDegrees * Mathf.Deg2Rad));
	}
}
