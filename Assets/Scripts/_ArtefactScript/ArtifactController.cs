using UnityEngine;
using Photon.Pun;

public class ArtifactController : MonoBehaviourPun
{
    public Transform currentHolder;

    public void PickUp(Transform holder)
    {
        if (!PhotonNetwork.IsConnected || !PhotonNetwork.InRoom)
            return;

        if (holder == null)
            return;

        PhotonView holderPV = holder.GetComponentInParent<PhotonView>();
        if (holderPV == null)
            return;

        //Asegurar el OwnerShip
        if(!photonView.IsMine)
        {
            photonView.RequestOwnership();
        }

        photonView.RPC(
            nameof(RPC_PickUp),
            RpcTarget.All,
            holderPV.ViewID
        );
    }

    [PunRPC]
    void RPC_PickUp(int holderViewID)
    {
        PhotonView holderPV = PhotonView.Find(holderViewID);
        if (holderPV == null)
            return;

        currentHolder = holderPV.transform;

        transform.SetParent(currentHolder);
        transform.localPosition = new Vector3(0, 1.5f, 0);
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
