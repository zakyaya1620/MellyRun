using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceSpawner : MonoBehaviour {

    public PieceType Type;
    private Piece currentPiece;

    public void Spawm()
    {

        int amtObj = 0;
        switch (Type)
        {
            case PieceType.jump:
                amtObj = levelmanager.Instance.jumpss.Count;
                break;
            case PieceType.slide:
                amtObj = levelmanager.Instance.slides.Count;
                break;
            case PieceType.longblock:
                amtObj = levelmanager.Instance.longblocks.Count;
                break;
            case PieceType.ramp:
                amtObj = levelmanager.Instance.ramps.Count;
                break;
        }
        currentPiece = levelmanager.Instance.GetPiece(Type, Random.Range(0,amtObj));
        currentPiece.gameObject.SetActive(true);
        currentPiece.transform.SetParent(transform, false);
    }

    public void Despawm()
    {
        currentPiece.gameObject.SetActive(false);
    }

}
