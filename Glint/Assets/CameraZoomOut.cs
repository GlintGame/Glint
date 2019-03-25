using Cinemachine;
using System.Collections;
using UnityEngine;

public class CameraZoomOut : MonoBehaviour
{
    public float SmoothTime = 2f;


    public float ZoomTo = 70f;
    private float OriginZoom;
    private float ZoomTarget;


    public Transform TargetTo;
    public Transform OriginTarget;
    private Transform Target;

    // smooth damp utils
    private float _dampVelocity;
    private Vector3 _dampVelocityPosition;

    // change values to this objects
    private CinemachineFramingTransposer FraminT;
    private Transform PlayerCamTarget;

    private void Awake()
    {
        this.FraminT = GameObject
                    .FindGameObjectsWithTag("CinemachineCam")[0]
                    .GetComponent<CinemachineVirtualCamera>()
                    .GetCinemachineComponent<CinemachineFramingTransposer>();

        this.PlayerCamTarget = GameObject
                    .FindGameObjectsWithTag("CamTarget")[0]
                    .GetComponent<Transform>();
        
        this.OriginZoom = this.ZoomTarget = this.FraminT.m_CameraDistance;
        this.Target = this.PlayerCamTarget;
    }

    private void Update()
    {
        this.FraminT.m_CameraDistance = Mathf.SmoothDamp(this.FraminT.m_CameraDistance, this.ZoomTarget, ref this._dampVelocity, this.SmoothTime);
        this.PlayerCamTarget.position = Vector3.SmoothDamp(this.PlayerCamTarget.position, this.Target.position, ref this._dampVelocityPosition, this.SmoothTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        this.ZoomTarget = this.ZoomTo;
        this.Target = this.TargetTo;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        this.ZoomTarget = this.OriginZoom;
        this.Target = this.OriginTarget;
    }
}
