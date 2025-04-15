using UnityEngine;

namespace Kdevaulo.MageDefend.Presentation
{
    public interface IPlayerContextProvider
    {
        Transform Target { get; }
        Vector3 GetDirection();
    }
}