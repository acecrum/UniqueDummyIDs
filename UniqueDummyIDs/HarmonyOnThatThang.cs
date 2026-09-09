using CentralAuth;
using HarmonyLib;
using NetworkManagerUtils.Dummies;

namespace UniqueDummyIDs;

[HarmonyPatch(typeof(PlayerAuthenticationManager), nameof(PlayerAuthenticationManager.InstanceMode), MethodType.Setter)]
public static class InstanceModePatch
{
    private static readonly AccessTools.FieldRef<PlayerAuthenticationManager, ClientInstanceMode> InstanceMode = AccessTools.FieldRefAccess<PlayerAuthenticationManager,
        ClientInstanceMode>("_targetInstanceMode");

    [HarmonyPrefix]
    public static void Prefix(
        PlayerAuthenticationManager __instance,
        ref ClientInstanceMode value)
    {
        var hub = __instance.GetComponent<ReferenceHub>();

        if (value == ClientInstanceMode.Dummy)
        {
            __instance.UserId = $"{__instance.connectionToClient.connectionId}@Dummy";
        }

        if (hub.connectionToClient is DummyNetworkConnection && __instance.UserId.EndsWith("@Dummy"))
        {
            value = ClientInstanceMode.Dummy;
        }
    }
}