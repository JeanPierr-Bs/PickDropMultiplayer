using Photon.Pun;
using UnityEngine;

public class PlayerPickup : MonoBehaviourPun
{
    public Transform holdPoint;
    private ArtifactController carriedArtifact;

    void OnTriggerEnter(Collider other)
    {
        if (!GameManager.Instance.IsPlaying()) return;
    if (!photonView.IsMine) return;

    ArtifactController artifact = other.GetComponent<ArtifactController>();
    if (artifact == null) return;

    PhotonView holdPV = holdPoint.GetComponent<PhotonView>();
    if (holdPV == null)
    {
        Debug.LogError("❌ ArtifactHoldPoint no tiene PhotonView");
        return;
    }

    artifact.PickUp(holdPV.ViewID);
    carriedArtifact = artifact;
    }

    public ArtifactController GetCarriedArtifact()
    {
        return carriedArtifact;
    }
    
    public void ClearArtifact()
    {
        carriedArtifact = null;
    }
}
