using Photon.Pun;
using UnityEngine;

public class DeliveryZone : MonoBehaviourPun
{
    private void OnTriggerEnter(Collider other)
    {
        if (!GameManager.Instance.IsPlaying())
            return;

        PhotonView pv = other.GetComponent<PhotonView>();
        if (pv == null || !pv.IsMine)
            return;

        PlayerPickup pickup = other.GetComponent<PlayerPickup>();
        if (pickup == null)
            return;

        ArtifactController artifact = pickup.GetCarriedArtifact();
        if (artifact == null)
            return;

        Debug.Log("[DeliveryZone] Entrega válida");

        pickup.ClearArtifact();
        artifact.Drop(transform.position);

        PlayerScore score = other.GetComponent<PlayerScore>();
        if (score != null)
            score.AddPoint();
    }
}
