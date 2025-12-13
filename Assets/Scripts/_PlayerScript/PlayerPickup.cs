using Photon.Pun;
using UnityEngine;

public class PlayerPickup : MonoBehaviourPun
{
    public Transform holdPoint;
    private ArtifactController carriedArtifact;

    void OnTriggerEnter(Collider other)
    {
        if (!GameManager.Instance.IsPlaying())
            return;

         if (!photonView.IsMine) return;

        ArtifactController artifact = other.GetComponent<ArtifactController>();

        if (artifact == null) return;

        artifact.PickUp(holdPoint);
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
