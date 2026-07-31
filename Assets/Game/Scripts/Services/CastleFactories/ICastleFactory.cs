using System.Threading;
using Assets.Game.Scripts.Shared;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.Game.Scripts.Services.CastleFactories
{
    public interface ICastleFactory
    {
        UniTask<(Health, Transform)> CreateCastleAsync(CancellationToken ct);
    }
}