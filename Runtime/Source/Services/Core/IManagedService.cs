using Services.Core;
using UnityEngine;

/// <summary>
/// Interface intended for Services that need to be located via Game Instance or other locator but
/// are constrained to the Game's world via GameObjects, these need to be created in a special way
/// </summary>
namespace Services.Core
{
    public interface IManagedService
    {
        bool IsPersistent {get;}
    }
}
