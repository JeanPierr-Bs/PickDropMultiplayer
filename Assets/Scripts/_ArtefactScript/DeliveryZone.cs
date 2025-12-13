using Photon.Pun;
using UnityEngine;

public class DeliveryZone : MonoBehaviourPun
{
    [Header("Configuracion Delivery")]
    public int allowedTeam;

    private void OnTriggerEnter(Collider other)
    {
        if (!GameManager.Instance.IsPlaying())
            return;

        PhotonView pv = other.GetComponent<PhotonView>();
        if (pv == null || !pv.IsMine)
            return;

        PlayerTeam team = other.GetComponent<PlayerTeam>();
        if (team == null || team.teamId != allowedTeam)
            return;

        PlayerPickup pickup = other.GetComponent<PlayerPickup>();
        if (pickup == null)
            return;

        ArtifactController artifact = pickup.GetCarriedArtifact();
        if (artifact == null)
            return;

        Debug.Log("🎯 Entrega final — fin del juego");

        pickup.ClearArtifact();
        artifact.Drop(transform.position);

        // 🧊 CONGELAR JUEGO
        GameManager.Instance.EndGame();
    }
}
