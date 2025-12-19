using UnityEngine;
using Photon.Pun;

public class ArtifactController : MonoBehaviourPun
{
    public Transform currentHolder;

    public void PickUp(int holdPointViewID)
    {
        if (!PhotonNetwork.IsConnected || !PhotonNetwork.InRoom)
            return;

        // if (holder == null)
        //     return;

        // PhotonView holderPV = holder.GetComponentInParent<PhotonView>();
        // if (holderPV == null)
        //     return;

        //Asegurar el OwnerShip
        // if(!photonView.IsMine)
        // {
        //     photonView.RequestOwnership();
        // }

        photonView.RPC(
            nameof(RPC_PickUp),
            RpcTarget.All,
            holdPointViewID
        );
    }

    [PunRPC]
    void RPC_PickUp(int holdPointViewID)
    {
        PhotonView holdPV = PhotonView.Find(holdPointViewID);
    if (holdPV == null)
    {
        Debug.LogError("❌ HoldPoint PhotonView no encontrado");
        return;
    }

    currentHolder = holdPV.transform;

    transform.SetParent(currentHolder);
    transform.localPosition = Vector3.zero;
    transform.localRotation = Quaternion.identity;
    }

    public void Drop(Vector3 dropPosition)
    {
        if(!photonView.IsMine)
        {
            photonView.RequestOwnership();
        }

        photonView.RPC(
            nameof(RPC_Drop), 
            RpcTarget.All, 
            dropPosition
        );
    }

    [PunRPC]
    void RPC_Drop(Vector3 pos)
    {
        currentHolder = null;

        transform.SetParent(null);
        transform.position = pos;
        transform.rotation = Quaternion.identity;
    }

}
