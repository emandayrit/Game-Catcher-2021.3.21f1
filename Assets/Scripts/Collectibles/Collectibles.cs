using System;
using UnityEngine;

public class Collectibles : MonoBehaviour
{
    public static Action<int> coinCollect, bombCollect;
    [SerializeField] CollectibleValues collectible;

    void OnCollisionEnter(Collision _collider)
    {
        if (_collider.gameObject.CompareTag("Player"))
        {
            string _tag = gameObject.tag;
            Transform _playerTransform = _collider.gameObject.transform;

            StartCollectedActions(_tag, _collider);
            collectible.PlayVFX(_playerTransform);
        }

        Destroy(gameObject);
    }

    void StartCollectedActions(string _tag, Collision _collider)
    {
        switch (_tag)
        {
            case "Coin":
                SlowModifier(_collider);
                coinCollect?.Invoke(collectible.value);
                break;

            case "Bomb":
                bombCollect?.Invoke(collectible.value);
                break;

            default:
                break;
        }
    }

    void SlowModifier(Collision _collider)
    {
        IMoveable _move = _collider.gameObject.GetComponent<IMoveable>();
        _move.MoveSlow(collectible.value);
    }

}
