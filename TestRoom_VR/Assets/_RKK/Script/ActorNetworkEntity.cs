using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ActorNetworkEntity : MonoBehaviour
{
    public SkinnedMeshRenderer[] meshRenderers;
    public Transform[] bodyBones;
    public Transform hip;
    public bool gateIsOpen = true;
    public float delaySeconds = 1f;
    
    private List<Vector3> hipPositions;
    private List<Quaternion[]> boneEulerAngles;
    private List<float> rpcTimestamps;
    private Vector3 prevTargetHipPosition, currTargetHipPosition;
    private Quaternion[] prevTargetEulerAngles, currTargetEulerAngles;
    private float prevRpcTimestamp, currRpcTimestamp;
    private int timestampFoundIndex;

    private int minTimestampCount = 3;
    private int maxTimestampCount = 100;

    private float DelayedTime => Time.time - delaySeconds;

    private void Start()
    {
        hipPositions = new List<Vector3>();
        boneEulerAngles = new List<Quaternion[]>();
        rpcTimestamps = new List<float>();
    }

    private void Update()
    {
        // Only start when there's enough data
        if (rpcTimestamps.Count > minTimestampCount && DelayedTime > rpcTimestamps[0])
        {
            timestampFoundIndex = -1;
            
            for (int i = 0; i < rpcTimestamps.Count; i++)
            {
                if (DelayedTime > rpcTimestamps[i]) continue;
                currTargetHipPosition = hipPositions[i];
                currTargetEulerAngles = boneEulerAngles[i];
                currRpcTimestamp = rpcTimestamps[i];
                prevTargetHipPosition = hipPositions[i - 1];
                prevTargetEulerAngles = boneEulerAngles[i - 1];
                prevRpcTimestamp = rpcTimestamps[i - 1];
                timestampFoundIndex = i;
                break;
            }

            if (timestampFoundIndex > -1)
            {
                float interpolationRatio = 1 / (currRpcTimestamp - prevRpcTimestamp) * (DelayedTime - prevRpcTimestamp);
                if (hip != null)
                {
                    hip.position =
                        Vector3.Lerp(prevTargetHipPosition, currTargetHipPosition, interpolationRatio);
                }
                for (int i = 0; i < bodyBones.Length; i++)
                {
                    bodyBones[i].rotation = Quaternion.Lerp(prevTargetEulerAngles[i], currTargetEulerAngles[i],
                        interpolationRatio);
                }
            }
        }
        
    }
    
    [PunRPC]
    void RPCBoneData(Quaternion[] objectArray)
    {
        if (!gateIsOpen) return;

        for (int i = 0; i < timestampFoundIndex - 1 && rpcTimestamps.Count > maxTimestampCount; i++)
        {
            hipPositions.RemoveAt(0);
            boneEulerAngles.RemoveAt(0);
            rpcTimestamps.RemoveAt(0);
        }

        if (delaySeconds == 0)
        {
            hip.position = QuaternionToVector3(objectArray[0]);
            for (int i = 0; i < bodyBones.Length; i++)
            {
                bodyBones[i].rotation = objectArray[i + 1];
            }
        }
        else
        {
            rpcTimestamps.Add(Time.time);
            Vector3 newPositions = QuaternionToVector3(objectArray[0]);
            Quaternion[] newEulerAngles = new Quaternion[bodyBones.Length];
            for (int i = 0; i < bodyBones.Length; i++)
            {
                newEulerAngles[i] = objectArray[i + 1];
            }
            hipPositions.Add(newPositions);
            boneEulerAngles.Add(newEulerAngles);
        }       
    }

    public void ManageMesh(bool nextState)
    {
        foreach (var skinnedMeshRenderer in meshRenderers)
            skinnedMeshRenderer.enabled = nextState;
    }

    private Quaternion Vector4ToQuaternion(Vector4 v)
    {
        return new Quaternion(v.x, v.y, v.z, v.w);
    }

    private Vector3 QuaternionToVector3(Quaternion q)
    {
        return new Vector3(q.x, q.y, q.z);
    }
}